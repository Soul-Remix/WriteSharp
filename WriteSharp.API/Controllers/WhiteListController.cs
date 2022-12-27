using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WriteSharp.API.Data;
using WriteSharp.API.DTO;
using WriteSharp.API.Models;

namespace WriteSharp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WhiteListController : ControllerBase
{
    private readonly AppDbContext _context;

    public WhiteListController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable> GetMyWhiteList()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var whiteList = await _context.WhiteLists.Where(x => x.UserId.Equals(userId))
            .Select(x => new {Id = x.Id,Word = x.Word})
            .ToListAsync();

        return whiteList;
    }

    [HttpPost]
    public async Task<IActionResult> AddWordToList(WhiteListPostDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var whiteList = new WhiteList()
        {
            Word = dto.Word,
            UserId = userId
        };

        await _context.WhiteLists.AddAsync(whiteList);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMyWhiteList), new { Text = dto.Word },
            new { Id = whiteList.Id, Text = whiteList.Word });
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveWordFromList(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var isAdmin = User.IsInRole("Admin");

        var whitelistWord = await _context.WhiteLists.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (whitelistWord == null)
        {
            return BadRequest();
        }

        if (whitelistWord.UserId != userId && !isAdmin)
        {
            return BadRequest();
        }

        _context.WhiteLists.Remove(whitelistWord);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}