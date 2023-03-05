using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Domain
{
    /// <summary>
    /// 保存文件的客户端
    /// </summary>
    public interface IStorageClient
    {
        /// <summary>
        /// 保存文件的类型
        /// </summary>
        StorageType StorageType { get; }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="key">文件路径的一部分</param>
        /// <param name="content">文件内容</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Uri?> SaveAsync(string key, Stream content, CancellationToken cancellationToken = default);
    }
}
