using FileServer.Domain;
using FileServer.Infrastructure;
using FileServer.Infrastructure.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FileServerExpansions
    {
        /// <summary>
        /// 文件服务的一些注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileServer(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IStorageClient, BackupStorageClient>();
            services.AddScoped<IStorageClient, MockStorageClient>();
            services.AddScoped<IFileRepository,FileRepository>();
            services.AddScoped<FileDomainServer>();
            services.AddDbContext<FileDbContext>(opt =>
            {
                string connStr = "Server=localhost;User ID=root;Password=123456;DataBase=ddd2;";
                opt.UseMySql(connStr, new MySqlServerVersion("8.0.30"));
            });
            return services;
        }
    }
}
