using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/Category/[Action]")]
[ApiController]
public class CategoryController : Controller
{
    private Context db;
    public CategoryController(Context _db)
    {
        db = _db;
    }

    [HttpPost]
    [Authorize]
    public IActionResult NewCategory(NewCategory NewCat)
    {
        var check = db.Menus_tbl.Find(NewCat.ParentMenuId);
        if (check == null)
        {
            return BadRequest("منوی والد یافت نشد");
        }
        else if (!check.EndLine)
        {
            return BadRequest("منوی والد ، زیر منو دارد. عملیات امکان پذیر نیست");
        }

        Category category = new Category()
        {
            ParentMenuId = NewCat.ParentMenuId,
            Title = NewCat.Title
        };
        db.Categories_tbl.Add(category);
        db.SaveChanges();
        return Ok("انجام شد");
    }

    [HttpGet]
    [Authorize]
    public ActionResult<List<DCategoryAdmin>> GetCategoryAdmin()
    {
        List<DCategoryAdmin> result = db.Categories_tbl
            .Include(x => x.Posts)
            .Include(x => x.ParentMenu)
            .Select(DCategoryAdmin.FromCategory)
            .ToList();

        return result;
    }
    [HttpGet]
    [Authorize]
    public ActionResult<DCategoryAdmin> GetACategoryAdmin(int Id)
    {
        DCategoryAdmin result = DCategoryAdmin.FromCategory(
            db.Categories_tbl
                .Include(x => x.Posts)
                .Include(x => x.ParentMenu)
                .FirstOrDefault(x => x.Id == Id)
            );
        return result;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<DCategoryClient> GetCategory(int Id)
    {
        DCategoryClient result = DCategoryClient.FromCategory(db.Categories_tbl
            .Include(x => x.Posts)
            .Include(x => x.ParentMenu)
            .FirstOrDefault(x => x.Id == Id), false);

        return result;
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpdateCategory(PutCategory category)
    {
        Category? check = db.Categories_tbl.Find(category.Id);
        if (check == null)
        {
            return BadRequest("کتگوری یافت نشد");
        }

        Menu? menuCheck = db.Menus_tbl.Find(category.ParentMenuId);
        if (!menuCheck.EndLine)
        {
            return BadRequest("منوی والد ، زیر منو دارد. عملیات امکان پذیر نیست");
        }

        check.Title = category.Title;
        check.ParentMenuId = category.ParentMenuId;
        db.Categories_tbl.Update(check);
        db.SaveChanges();
        return Ok("انجام شد");
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DelCategory(int Id)
    {
        Category? check = db.Categories_tbl.Find(Id);
        if (check == null)
        {
            return BadRequest("کتگوری یافت نشد");
        }
        db.Categories_tbl.Remove(check);
        db.SaveChanges();
        return Ok("انجام شد");
    }
}