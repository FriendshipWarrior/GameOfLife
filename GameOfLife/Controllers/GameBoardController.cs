using GameOfLife.Domain;
using GameOfLife.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameOfLife.Controllers
{
    [ApiController]
    public class GameBoardController : ControllerBase
    {
        private readonly IGameBoardService _gameBoardService;
        private readonly ILogger<GameBoardController> _logger;

        public GameBoardController(IGameBoardService gameBoardService, ILogger<GameBoardController> logger)
        {
            _gameBoardService = gameBoardService;
            _logger = logger;
        }

        [ProducesResponseType(typeof(CreateBoardReply), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("api/board")]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest request)
        {
            var boardId = await _gameBoardService.CreateGameBoard(request);

            var reply = new CreateBoardReply
            {
                BoardId = boardId,
            };

            return Ok(reply);
        }
    }
}