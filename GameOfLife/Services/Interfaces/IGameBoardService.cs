using GameOfLife.Domain;

namespace GameOfLife.Services.Interfaces
{
    public interface IGameBoardService
    {
        Task<int> CreateGameBoardAsync(int[][] board);
        Task<GameBoard> GetGameBoardAsync(int boardId);
        Task<GameBoard> GetGameBoardNextStateAsync(int boardId, int numberOfStates);
        Task<GameBoard> GetGameBoardFinalStateAsync(int boardId, int numberOfAttempts);
    }
}
