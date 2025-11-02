using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIAula01.Models;

namespace APIAula01.Data
{
    public class APIAula01Context : DbContext
    {
        public APIAula01Context (DbContextOptions<APIAula01Context> options)
            : base(options)
        {
        }

        public DbSet<APIAula01.Models.TodoItem> TodoItem { get; set; } = default!;
    }
}
