using System.Data.Entity;
using CrudApp_Core;

namespace CrudApp.Infrastructure
{
    public class NoteContext : DbContext
    {
        public NoteContext()
            : base("name=CrudAppContextConnectionString")
        {
            //var a = Database.Connection.ConnectionString;
        }
        public DbSet<Note> Note { get; set; }
    }
}
