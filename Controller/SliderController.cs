using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/Slider/[Action]")]
[ApiController]
public class SliderController : Controller
{
    private readonly Context db;
    public SliderController(Context _db)
    {
        db = _db;
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddSlider([FromBody] NewSlider NSlider )
    {
        var Slider = new Slider();
        Slider.SliderName = NSlider.SliderName;
        Slider.SliderType = NSlider.SliderType;
        Slider.active = NSlider.active;

        db.Slider_tbl.Add(Slider);
        db.SaveChanges();

        return Ok("ثبت با موفقیت انجام شد");
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<Slider>> GetSliders()
    {
        var Slider = db.Slider_tbl.ToList();
        return Slider;
    }
    
    [HttpGet]
    [Authorize]
    public ActionResult<Slider> GetSlider(int Id)
    {
        var Slider = db.Slider_tbl.Find(Id);
        return Slider != null ? Ok(Slider) : BadRequest("اسلایدر یافت نشد");
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DelSlider(int Id)
    {
        var Slider = db.Slider_tbl.Find(Id);
        db.Slider_tbl.Remove(Slider);
        db.SaveChanges();
        return Ok("با موفقیت پاک گردید");
    }
    
    [HttpPut]
    [Authorize]
    public IActionResult UpSlider([FromBody] UpdateSlider updateSlider)
    {
      var Slider=db.Slider_tbl.Find(updateSlider.Id);
      Slider.SliderName=updateSlider.SliderName;
      Slider.active=updateSlider.active;

      db.Slider_tbl.Update(Slider);
      db.SaveChanges();
        return Ok("با موفقثیت آپدیت شد");
    }
}