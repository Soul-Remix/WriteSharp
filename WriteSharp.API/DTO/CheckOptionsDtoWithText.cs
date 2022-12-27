using System.ComponentModel.DataAnnotations;

namespace WriteSharp.API.DTO;

public class CheckOptionsDtoWithText : CheckOptionsDto
{
    [Required] [MinLength(1)] public string text { get; set; }
}