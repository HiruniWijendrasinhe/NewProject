using Microsoft.EntityFrameworkCore;

namespace NewProject
{

    public class Datacontextnew:DbContext
{protected override  void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        optionBuilder.UseSqlite(@"DataSource=C:\Newdestop\TaskDatabase\UserData.db");
    }
    public DbSet<Tasks>NewTask{get;set;}
}
}
