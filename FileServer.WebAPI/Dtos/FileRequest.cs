using FluentValidation;
using System;

namespace FileServer.WebAPI.Dtos
{
    public class FileRequest
    {
        public IFormFile File { get; set; }
    }
    public class FileRequestValidator : AbstractValidator<FileRequest>
    {
        public FileRequestValidator()
        {
            long maxFileSize = 50 * 1024 * 1024;//最大文件大小
            RuleFor(x => x.File).NotNull().Must(x => x.Length > 0 && x.Length < maxFileSize).WithMessage("文件格式不对");
        }
    }
}
