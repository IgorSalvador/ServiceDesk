namespace ServiceDesk.Web.DTOs;

public class Result<T>
{
    public bool Succeeded { get; set; }
    public string[] Errors { get; set; } = [];
    public T? Data { get; set; }
}
