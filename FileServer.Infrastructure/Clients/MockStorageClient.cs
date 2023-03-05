using FileServer.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Infrastructure.Clients
{
    /// <summary>
    /// 假数据上传
    /// </summary>
    public class MockStorageClient : IStorageClient
    {
        public StorageType StorageType => StorageType.Public;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MockStorageClient(IWebHostEnvironment hostEnv, IHttpContextAccessor httpContextAccessor)
        {
            this._hostEnv = hostEnv;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<Uri?> SaveAsync(string key, Stream content, CancellationToken cancellationToken = default)
        {
            string path1 = Path.Combine(_hostEnv.ContentRootPath, "wwwroot");
            string fullPath = Path.Combine(path1, key);
            string? fullDir = Path.GetDirectoryName(fullPath);//得到目录名
            if (!Directory.Exists(fullDir))
                Directory.CreateDirectory(fullDir!);
            //不存在数据才保存,存在就不用在保存了
            if(!File.Exists(fullPath))
            {
                using Stream outStream = File.OpenWrite(fullPath);
                await content.CopyToAsync(outStream, cancellationToken);
            }
            var req = _httpContextAccessor.HttpContext!.Request;
            string url = req.Scheme + "://" + req.Host + "/" + key;
            return new Uri(url);
        }
    }
}
