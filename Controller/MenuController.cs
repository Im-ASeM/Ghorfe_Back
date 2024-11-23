using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/Menu/[Action]")]
[ApiController]
public class MenuController : Controller
{
    private readonly Context db;
    public MenuController(Context _db)
    {
        db = _db;
    }

    [HttpPost]
    [Authorize]
    public IActionResult NewMenu(NewMenu menu)
    {
        Menu NewMenu = new Menu()
        {
            EndLine = true,
            ParentId = menu.ParentId.HasValue ? menu.ParentId : null,
            Title = menu.Title
        };


        if (menu.ParentId.HasValue)
        {
            Menu? check = db.Menus_tbl.Include(x => x.Subs).FirstOrDefault(x => x.Id == menu.ParentId);
            if (check.Subs.Count != 0)
            {
                return BadRequest("منوی والد در دسترس نیست");
            }
            if (check.EndLine)
            {
                check.EndLine = false;
                db.Menus_tbl.Update(check);
                db.SaveChanges();
            }
        }
        db.Menus_tbl.Add(NewMenu);
        db.SaveChanges();
        return Ok("Succsesful !");
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<DMenu>> GetMenus()
    {
        List<DMenu> results = db.Menus_tbl
            .Include(x => x.Children)
                .ThenInclude(x => x.Children)
            .Where(x => x.ParentId == null)
            .Select(DMenu.FromMenu)
            .ToList();

        return results.Count == 0 ? null : results;
    }
    [HttpGet]
    [Authorize]
    public ActionResult<List<DMenu>> GetMenusList()
    {
        List<DMenu> results = db.Menus_tbl
            .Select(DMenu.FromMenu)
            .ToList();

        return results.Count == 0 ? null : results;
    }
    [HttpGet]
    [Authorize]
    public ActionResult<List<DMenuAdmin>> GetMenusAdmin()
    {
        List<DMenuAdmin> results = db.Menus_tbl
            .Include(x => x.Subs)
                .ThenInclude(x => x.Posts)
            .Include(x => x.Children)
                .ThenInclude(x => x.Children)
            .Include(x => x.Children)
                .ThenInclude(x => x.Subs)
                    .ThenInclude(x => x.Posts)
            .Where(x => x.ParentId == null)
            .Select(DMenuAdmin.FromMenu)
            .ToList();

        return results.Count == 0 ? null : results;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<DCategoryClient>> MenuCategory(int Id)
    {
        List<DCategoryClient> result = db.Categories_tbl
            .Where(x => x.ParentMenuId == Id)
            .Include(x => x.Posts)
            .Include(x => x.ParentMenu)
            .OrderByDescending(x => x.Id)
            .Select(x => DCategoryClient.FromCategory(x, true))
            .ToList();

        return result;
    }
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<string> GetMenu(int Id)
    {
        string? result = db.Menus_tbl.Find(Id)?.Title;
        return !String.IsNullOrEmpty(result) ? result : "";
    }

    [HttpGet]
    [Authorize]
    public ActionResult<DMenuAdmin?> GetMenuAdmin(int Id)
    {
        Menu? result = db.Menus_tbl
            .Include(x=>x.Children)
            .Include(x=>x.Subs)
            .FirstOrDefault(x=>x.Id == Id);
        return result == null ? null : DMenuAdmin.FromMenu(result); 
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DelMenu(int Id)
    {
        var check = db.Menus_tbl.Find(Id);
        if (check == null)
        {
            return BadRequest("منو مورد نظر یافت نشد");
        }
        db.Menus_tbl.Remove(check);
        db.SaveChanges();

        if (check.ParentId.HasValue)
        {
            Menu ParentCheck = db.Menus_tbl.Include(x => x.Children).FirstOrDefault(x => x.Id == check.ParentId);
            if (ParentCheck.Children.Count == 0)
            {
                ParentCheck.EndLine = true;
                db.Menus_tbl.Update(ParentCheck);
                db.SaveChanges();
            }

        }
        return Ok("انجام شد");
    }

    [HttpPut]
    [Authorize]
    public IActionResult RenameMenu(UpdateMenu menu)
    {
        Menu check = db.Menus_tbl.Find(menu.Id);
        if (check == null)
        {
            return BadRequest("منو مورد نظر یافت نشد");
        }
        check.Title = menu.Title;
        db.Menus_tbl.Update(check);
        db.SaveChanges();
        return Ok("انجام شد");
    }
}