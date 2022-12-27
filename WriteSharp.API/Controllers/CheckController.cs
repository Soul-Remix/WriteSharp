using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriteSharp.API.DTO;
using WriteSharp.Types;

namespace WriteSharp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly WriteSharp _writeSharp;

        public CheckController(WriteSharp writeSharp)
        {
            _writeSharp = writeSharp;
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
        public ActionResult<List<CheckResult>> WithOptionsCheck(CheckOptionsDtoWithText options)
        {
            return _writeSharp.Check(options.text, MapOptions(options));
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
                WhiteList = options.WhiteList,
            };
        }
    }
}