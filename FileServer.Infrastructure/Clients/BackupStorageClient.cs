using FileServer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Infrastructure.Clients
{
    /// <summary>
    /// 备份数据
    /// </summary>
    public class BackupStorageClient : IStorageClient
    {
        public StorageType StorageType => StorageType.Backup;

        public async Task<Uri?> SaveAsync(string key, Stream content, CancellationToken cancellationToken = default)
        {
            string path1 = @"E:\DDD2\FileServer\";
            string fullPath = Path.Combine(path1, key);
            string? dir = Path.GetDirectoryName(fullPath);
            if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir!);
            //不存在数据才保存,存在就不用在保存了
            if (!File.Exists(fullPath))
            {
                using Stream outStream = File.OpenWrite(fullPath);
                await content.CopyToAsync(outStream, cancellationToken);
            }
            return new Uri(fullPath);
        }
    }
}
