using GameOfLife.Domain;
using GameOfLife.Services;
using GameOfLife.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeTests.ServiceTests
{
    public class GameBoardServiceTests
    {
        private readonly IGameBoardService _gameBoardService;

        public GameBoardServiceTests(IGameBoardService gameBoardService)
        {
            _gameBoardService = gameBoardService;
        }

        [Fact]
        public async Task CreateGameBoard()
        {
            var board = GenerateBoard(16, 16);

            var boardId = await _gameBoardService.CreateGameBoardAsync(board);

            var gameBoard = await _gameBoardService.GetGameBoardAsync(boardId);

            Assert.Equal(boardId, gameBoard.BoardId);
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
                    if (rng.Next(1, 101) < 70)
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
