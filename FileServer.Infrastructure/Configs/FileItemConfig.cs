using FileServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileServer.Infrastructure.Configs
{
    public class FileItemConfig : IEntityTypeConfiguration<FileItem>
    {
        public void Configure(EntityTypeBuilder<FileItem> builder)
        {
            builder.ToTable("file_items");
            builder.Property(p => p.FileName).IsUnicode().HasMaxLength(1024);
            builder.Property(p=>p.FileSha256).IsUnicode(false).HasMaxLength(64);
            builder.HasIndex(p => p.FileSha256);
        }
    }
}