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
        public DbSet<APIAula01.Models.Category> Category { get; set; } = default!;

        // Configuração do relacionamento usando Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do relacionamento 1:N entre Category e TodoItem
            modelBuilder.Entity<TodoItem>()
                .HasOne(t => t.Category)                    // TodoItem tem uma Category
                .WithMany(c => c.TodoItems)                 // Category tem muitos TodoItems
                .HasForeignKey(t => t.CategoryId)           // Chave estrangeira é CategoryId
                .OnDelete(DeleteBehavior.SetNull);          // Se categoria for deletada, CategoryId vira null (opcional)

            // Configuração de índices para melhor performance
            modelBuilder.Entity<TodoItem>()
                .HasIndex(t => t.CategoryId);
        }
    }
}
