using Microsoft.EntityFrameworkCore;
using RMSystem.Models;


namespace RMSystem.Services
{
    public class RMSystemContext : DbContext
    {
        // Наследуем настройки от базового DbContext.
        public RMSystemContext(DbContextOptions<RMSystemContext> options) : base(options)
        {
        }

        // Создаем таблицу рисков в базе.
        public DbSet<Risk> Risks { get; set; }


    }
}
