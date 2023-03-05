namespace FileServer.WebAPI.Dtos
{
    public record FileExistResponse(bool IsExist,Uri? url);
}
