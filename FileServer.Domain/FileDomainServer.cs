using FileServer.Domain.Entities;
using Lyd.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Domain
{
    /// <summary>
    /// 文件的领域服务
    /// </summary>
    public class FileDomainServer
    {
        private readonly IFileRepository _fileRepository;
        private readonly IStorageClient _backupClient;
        private readonly IStorageClient _remoteClient;
        public FileDomainServer(IFileRepository fileRepository, IEnumerable<IStorageClient> storageClients)
        {
            this._fileRepository = fileRepository;
            this._backupClient = storageClients.First(s => s.StorageType == StorageType.Backup);
            this._remoteClient = storageClients.First(s => s.StorageType == StorageType.Public);
        }

        //领域服务只是一个完成事情的框架流程,并没有具体的方法
        //领域服务只有抽象的业务逻辑
        public async Task<FileItem> UploadAsync(Stream stream, string fileName,CancellationToken cancellationToken)
        {
            string hash = HashHelper.ComputeSha256Hash(stream);
            long length = stream.Length;
            DateTime today = DateTime.Now;
            //文件的目录,路径
            string key = $"{today.Year}/{today.Month}/{today.Day}/{hash}/{fileName}";
            //查找是否存在这个文件,如果存在就不用上传了,直接更改一个修改日期就行了
            var fileItem = await _fileRepository.FindFileAsync(hash);
            if (fileItem != null)
                return fileItem;
            //每次都要把指针的位置归零
            stream.Position = 0;
            //先备份
            Uri? backupUrl = await _backupClient.SaveAsync(key, stream, cancellationToken);
            stream.Position = 0;
            Uri? remoteUrl = await _remoteClient.SaveAsync(key, stream, cancellationToken);
            stream.Position = 0;
            //领域服务并不会真正的执行数据库插入，只是把实体对象生成，然后由应用服务和基础设施配合来真正的插入数据库！
            return new FileItem(length,fileName,hash,backupUrl,remoteUrl);
        }
    }
}
