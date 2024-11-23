using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/Logo/[Action]")]
[ApiController]
public class LogoController : Controller
{
    private readonly Context db;
    public LogoController(Context _db)
    {
        db = _db;
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddLogo([FromBody] NewLogo NLogo)
    {
        var Logo = new Logo();
        Logo.ImageLogo = NLogo.ImageLogo;

        Logo.active = NLogo.active;

        db.Logo_tbl.Add(Logo);
        db.SaveChanges();

        return Ok("ثبت با موفقیت انجام شد");
    }

    [HttpGet]
    [Authorize]
    public ActionResult<List<Logo>> GetLogos()
    {
        var Logo = db.Logo_tbl.ToList();
        return Logo;
    }
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<string> GetLogo()
    {
        var Logo = db.Logo_tbl.FirstOrDefault(x => x.active);
        return Logo != null ? Ok(Logo.ImageLogo) : "";
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DelLogo(int Id)
    {
        var Logo = db.Logo_tbl.Find(Id);
        if (Logo == null)
        {
            return BadRequest("لوگو یافت نشد");
        }
        db.Logo_tbl.Remove(Logo);
        db.SaveChanges();
        return Ok("با موفقیت پاک گردید");
    }
}