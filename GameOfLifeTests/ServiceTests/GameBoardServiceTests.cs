using GameOfLife.Services.Interfaces;

namespace GameOfLifeTests.ServiceTests
{
    public class GameBoardServiceTests : IClassFixture<TestFixture>
    {
        private readonly IGameBoardService _gameBoardService;

        public GameBoardServiceTests(TestFixture fixture)
        {
            _gameBoardService = fixture.GameBoardService;
        }

        [Fact]
        public async Task CreateGameBoard()
        {
            var board = GenerateBoard(16, 16);

            var boardId = await _gameBoardService.CreateGameBoardAsync(board);

            var gameBoard = await _gameBoardService.GetGameBoardAsync(boardId);

            Assert.Equal(boardId, gameBoard.BoardId);
        }

        [Fact]
        public async Task GetGameBoardNextState_1_Generation()
        {
            var board = GenerateBoard(16, 16);

            var boardId = await _gameBoardService.CreateGameBoardAsync(board);

            var gameBoard = await _gameBoardService.GetGameBoardNextStateAsync(boardId, 1);

            Assert.NotEqual(board, gameBoard.Board);
        }

        [Fact]
        public async Task GetGameBoardNextState_Many_Generations()
        {
            var board = GenerateBoard(16, 16);

            var boardId = await _gameBoardService.CreateGameBoardAsync(board);

            var gameBoard = await _gameBoardService.GetGameBoardNextStateAsync(boardId, 8);

            Assert.NotEqual(board, gameBoard.Board);
        }

        [Fact]
        public async Task GetGameBoardFinalState_Failure()
        {
            var board = GenerateBoard(24, 24);

            var boardId = await _gameBoardService.CreateGameBoardAsync(board);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _gameBoardService.GetGameBoardFinalStateAsync(boardId, 1));

            Assert.Equal("Game board did not reach conlusion within the specific number of generations.", exception.Message);
        }

        [Fact]
        public async Task GetGameBoardFinalState_Success()
        {
            var board = GenerateBoard(3, 3);

            var boardId = await _gameBoardService.CreateGameBoardAsync(board);

            var gameBoard = await _gameBoardService.GetGameBoardFinalStateAsync(boardId, 10);

            Assert.NotNull(gameBoard);
        }

        private int[][] GenerateBoard(int x, int y)
        {
            var board = new int[x][];

            var rng = new Random();
            for (var i = 0; i < x; i++)
            {
                board[i] = new int[y];
                for (var j = 0; j < y; j++)
                {
                    if (rng.Next(1, 101) < 75)
                    {
                        board[i][j] = 0;
                    }
                    else
                    {
                        board[i][j] = 1;
                    }
                }
            }

            return board;
        }
    }
}
