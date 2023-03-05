using FileServer.Domain;
using FileServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Infrastructure
{
    /// <summary>
    /// 文件仓储
    /// </summary>
    public class FileRepository : IFileRepository
    {
        private readonly FileDbContext _dbContext;
        public FileRepository(FileDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<FileItem?> FindFileAsync(string? fileSha256)
        {
            return _dbContext.FileItems.FirstOrDefaultAsync(x=>x.FileSha256 == fileSha256);
        }
    }
}
