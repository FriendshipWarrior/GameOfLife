using GameOfLife.Context;
using GameOfLife.Services;
using GameOfLife.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameOfLifeTests
{
    public class TestFixture : IDisposable
    {
        public IGameBoardService GameBoardService { get; set; }

        public TestFixture() 
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<GameBoardService>>();
            var dbContextOptions = new DbContextOptionsBuilder<BoardContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var boardContext = new BoardContext(dbContextOptions);

            GameBoardService = new GameBoardService(logger, boardContext);
        }

        public void Dispose()
        {
        }
    }
}
