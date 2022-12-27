namespace WriteSharp.API.DTO;

public class CheckOptionsWithList : CheckOptionsDto
{
    public List<string> WhiteList { get; set; } = new List<string>();
}