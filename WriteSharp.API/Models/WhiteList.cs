using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WriteSharp.API.Models;

public class WhiteList
{
    public int Id { get; set; }

    [Required] public string Word { get; set; }

    public string UserId { get; set; }
    public IdentityUser User { get; set; }
}