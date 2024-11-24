using Microsoft.EntityFrameworkCore;
public class Context : DbContext
{
    public DbSet<Slider> Slider_tbl { get; set; }
    public DbSet<Logo> Logo_tbl { get; set; }
    public DbSet<PhoneNumber> Phones_tbl { get; set; }
    public DbSet<Posts> Posts_tbl { get; set; }
    public DbSet<Category> Categories_tbl { get; set; }
    public DbSet<Menu> Menus_tbl { get; set; }
    public DbSet<Users> Users_tbl { get; set; }

    

    // protected override void OnConfiguring(DbContextOptionsBuilder db)
    //      {
    //          db.UseSqlServer("server=.;database=gorfe;trusted_connection=true;MultipleActiveResultSets=True;TrustServerCertificate=True");
    //      }

    protected override void OnConfiguring(DbContextOptionsBuilder db)
    {
        db.UseSqlServer("server=.\\SQL2019;database=Gorfe;user ID=sa;password=1234;MultipleActiveResultSets=True;TrustServerCertificate=True");
    }

}