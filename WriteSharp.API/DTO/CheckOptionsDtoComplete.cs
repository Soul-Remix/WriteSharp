using System.ComponentModel.DataAnnotations;

namespace WriteSharp.API.DTO;

public class CheckOptionsDtoComplete : CheckOptionsDto
{
    [Required] [MinLength(1)] public string text { get; set; }
    public List<string> WhiteList { get; set; } = new List<string>();
}