using GameOfLife.Domain;

namespace GameOfLife.Services.Interfaces
{
    public interface IGameBoardService
    {
        Task<int> CreateGameBoard(CreateBoardRequest request);
    }
}
