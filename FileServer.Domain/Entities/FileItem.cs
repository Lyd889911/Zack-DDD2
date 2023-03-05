using Lyd.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Domain.Entities
{
    public record FileItem :BaseEntity
    {
        public long FileSizeByte { get;set; }
        public string? FileName { get; set; }
        public string? FileSha256 { get; set; }
        public Uri? BackupUrl { get; set; }
        public Uri? RemoteUrl { get; set; }
        public FileItem(long fileSizeByte,string? fileName,string? fileSha256,Uri? backupUrl,Uri? remoteUrl):base()
        {
            this.BackupUrl = backupUrl;
            this.FileSizeByte = fileSizeByte;
            this.FileName = fileName;
            this.FileSha256 = fileSha256;
            this.RemoteUrl = remoteUrl;
        }

    }
}
