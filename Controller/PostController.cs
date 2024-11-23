using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/Post/[Action]")]
[ApiController]
public class PostController : Controller
{
    private Context db;
    public PostController(Context _db)
    {
        db = _db;
    }

    [HttpPost]
    [Authorize]
    public IActionResult NewPost(NewPosts post)
    {
        Category? check = db.Categories_tbl.Find(post.CategoryId);
        if (check == null) return BadRequest("کتگوری در دسترس نیست");

        Posts result = new Posts
        {
            Body = post.Body,
            Caption = post.Caption,
            CategoryId = post.CategoryId,
            MainImage = post.MainImage,
            Title = post.Title,
            WordKey = post.WordKey
        };
        db.Posts_tbl.Add(result);
        db.SaveChanges();
        return Ok("انجام شد");
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<DPost> GetPost(int Id)
    {
        Posts? check = db.Posts_tbl
            .Include(x => x.Category)
            .FirstOrDefault(x => x.Id == Id);
        return check == null ? BadRequest("پست در دسترس نیست") : DPost.FromPost(check);
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DelPost(int Id)
    {
        Posts? check = db.Posts_tbl.Find(Id);
        if (check == null) return BadRequest("پست یافت نشد");
        db.Posts_tbl.Remove(check);
        db.SaveChanges();
        return Ok("انجام شد");
    }

    [HttpPut]
    [Authorize]
    public IActionResult EditPost(UpdatePost post){
        Posts? check = db.Posts_tbl.Find(post.Id);
        if (check == null) return BadRequest("پست یافت نشد");
        check.Body = post.Body;
        check.Caption = post.Caption;
        check.MainImage = post.MainImage;
        check.Title = post.Title;
        check.WordKey = post.WordKey;
        db.Posts_tbl.Update(check);
        db.SaveChanges();
        return Ok("انجام شد");
    }
}