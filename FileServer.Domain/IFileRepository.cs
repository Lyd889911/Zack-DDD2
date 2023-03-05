using FileServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Domain
{
    /// <summary>
    /// 文件服务仓储接口
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// 通过sha256值查找文件
        /// </summary>
        /// <param name="fileSizeByte"></param>
        /// <param name="fileSha256"></param>
        /// <returns></returns>
        Task<FileItem?> FindFileAsync(string? fileSha256);
    }
}
