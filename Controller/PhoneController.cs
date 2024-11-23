using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/PhoneNumber/[Action]")]
[ApiController]
public class PhoneController : Controller
{
    private readonly Context db;
    public PhoneController(Context _db)
    {
        db = _db;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<PhoneNumber>> GetPhones()
    {
        var result = db.Phones_tbl.ToList();
        if(result.Count == 0){
            db.Phones_tbl.Add(new PhoneNumber{
                Phone = "تلفن 1"
            });
            db.SaveChanges();
            db.Phones_tbl.Add(new PhoneNumber{
                Phone = "تلفن 2"
            });
            db.SaveChanges();

            result = db.Phones_tbl.ToList();
        }
        return result;
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpPhone([FromBody] updatePhone updatePhone)
    {
        var PhoneNumber = db.Phones_tbl.Find(updatePhone.Id);
        PhoneNumber.Phone = updatePhone.Phone;

        db.Phones_tbl.Update(PhoneNumber);
        db.SaveChanges();
        return Ok("با موفقثیت آپدیت شد");
    }
}