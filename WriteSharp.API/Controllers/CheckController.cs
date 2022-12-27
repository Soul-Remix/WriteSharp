using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WriteSharp.API.Data;
using WriteSharp.API.DTO;
using WriteSharp.Types;

namespace WriteSharp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckController : ControllerBase
{
    private readonly WriteSharp _writeSharp;
    private readonly AppDbContext _context;

    public CheckController(WriteSharp writeSharp, AppDbContext context)
    {
        _writeSharp = writeSharp;
        _context = context;
    }

    [HttpGet("free")]
    public ActionResult<List<CheckResult>> FreeCheckGet(string text)
    {
        if (text.Length > 400)
        {
            return BadRequest();
        }

        return _writeSharp.Check(text);
    }

    [HttpPost("free")]
    public ActionResult<List<CheckResult>> FreeCheckPost(FreeCheckDto text)
    {
        return _writeSharp.Check(text.Text);
    }

    [Authorize]
    [HttpPost("withOptions")]
    public ActionResult<List<CheckResult>> WithOptionsCheck(CheckOptionsDtoComplete options)
    {
        var opt = MapOptions(options);
        opt.WhiteList = options.WhiteList;
        return _writeSharp.Check(options.text, opt);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<List<CheckResult>>> DefaultCheck(CheckOptionsDtoWithText options)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var whiteList = await _context.WhiteLists.Where(x => x.UserId.Equals(userId))
            .Select(x => x.Word)
            .ToListAsync();

        var opt = MapOptions(options);
        opt.WhiteList = whiteList;
        return _writeSharp.Check(options.text, opt);
    }

    private WriteSharpOptions MapOptions(CheckOptionsDto options)
    {
        return new WriteSharpOptions()
        {
            AdverbWhere = options.AdverbWhere,
            Duplicates = options.Duplicates,
            EPrime = options.EPrime,
            NoCliches = options.NoCliches,
            PassiveVoice = options.PassiveVoice,
            StartWithSo = options.StartWithSo,
            ThereIs = options.ThereIs,
            TooWordy = options.TooWordy,
            WeaselWords = options.WeaselWords,
        };
    }
}