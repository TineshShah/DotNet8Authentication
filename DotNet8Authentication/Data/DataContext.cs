using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNet8Authentication.Data {
    //DataContext is used to interact with the database
    //The options is dependency injected from the Program.cs
    public class DataContext:IdentityDbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }
    }
}
