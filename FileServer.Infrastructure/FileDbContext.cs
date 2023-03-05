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
    /// 文件服务的数据库上下文
    /// </summary>
    public class FileDbContext:DbContext
    {
        public DbSet<FileItem> FileItems { get; set; }
        public FileDbContext(DbContextOptions<FileDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
