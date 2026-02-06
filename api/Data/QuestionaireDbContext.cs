using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class QuestionaireDbContext(DbContextOptions<QuestionaireDbContext> options) : DbContext(options)
    {
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<QuestionOption> Options => Set<QuestionOption>();
        public DbSet<Respondent> Respondents => Set<Respondent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, Text = "ข้อใดต่างจากข้ออื่น", AnswerId = 4 },
                new Question { Id = 2, Text = "X + 2 = 4 จงหาค่า X", AnswerId = 6 }
            );

            modelBuilder.Entity<QuestionOption>().HasData(
                new QuestionOption { Id = 1, QuestionId = 1, Text = "3" },
                new QuestionOption { Id = 2, QuestionId = 1, Text = "5" },
                new QuestionOption { Id = 3, QuestionId = 1, Text = "9" },
                new QuestionOption { Id = 4, QuestionId = 1, Text = "11" },
                new QuestionOption { Id = 5, QuestionId = 2, Text = "1" },
                new QuestionOption { Id = 6, QuestionId = 2, Text = "2" },
                new QuestionOption { Id = 7, QuestionId = 2, Text = "3" },
                new QuestionOption { Id = 8, QuestionId = 2, Text = "4" }
               );
        }
    }
}
