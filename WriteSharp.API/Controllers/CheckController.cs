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

        [HttpGet]
        public ActionResult<List<CheckResult>> FreeCheckGet(string text)
        {
            if (text.Length > 400)
            {
                return BadRequest();
            }

            return _writeSharp.Check(text);
        }

        [HttpPost]
        public ActionResult<List<CheckResult>> FreeCheckPost(FreeCheckDto text)
        {
            return _writeSharp.Check(text.Text);
        }
    }
}