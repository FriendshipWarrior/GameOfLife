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
    }
}
