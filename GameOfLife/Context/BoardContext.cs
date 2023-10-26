using GameOfLife.Domain;
using Microsoft.EntityFrameworkCore;

namespace GameOfLife.Context
{
    public class BoardContext : DbContext
    {
        public DbSet<GameBoard> GameBoards { get; set; }

        public BoardContext(DbContextOptions<BoardContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameBoard>().HasKey(b => b.BoardId);
            modelBuilder.Entity<GameBoard>().Property(b => b.BoardJson).HasColumnName("Board");
        }
    }
}
