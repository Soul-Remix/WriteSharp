using System.ComponentModel.DataAnnotations;

namespace WriteSharp.API.DTO;

public class FreeCheckDto
{
    [Required] [MaxLength(400)] public string Text { get; set; } = "";
}