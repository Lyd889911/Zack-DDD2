using FileServer.Domain;
using FileServer.Domain.Entities;
using FileServer.Infrastructure;
using FileServer.WebAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileServer.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly FileDbContext _dbContext;
        private readonly FileDomainServer _fileDomainServer;
        public FileController(IFileRepository fileRepository, FileDbContext dbContext, FileDomainServer fileDomainServer)
        {
            _fileRepository = fileRepository;
            _dbContext = dbContext;
            _fileDomainServer = fileDomainServer;
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        [HttpGet("{hash}")]
        public async Task<FileExistResponse> FileExist([FromRoute]string hash)
        {
            var fileItem = await _fileRepository.FindFileAsync(hash);
            if (fileItem == null)
                return new FileExistResponse(false, null);
            else
                return new FileExistResponse(true, fileItem.RemoteUrl);
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Uri?> Upload([FromForm]FileRequest request, CancellationToken cancellationToken = default)
        {
            string fileName = request.File.FileName;
            Stream content = request.File.OpenReadStream();
            var fileItem  = await _fileDomainServer.UploadAsync(content, fileName, cancellationToken);
            _dbContext.Add(fileItem);
            await _dbContext.SaveChangesAsync();
            return fileItem.RemoteUrl;
        }
    }
}
