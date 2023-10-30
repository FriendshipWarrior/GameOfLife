using GameOfLife.Domain;
using GameOfLife.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameOfLife.Controllers
{
    [ApiController]
    [Route("api/boards")]
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
        public async Task<IActionResult> CreateGameBoard([FromBody] CreateBoardRequest request)
        {
            if (request?.Board == null)
            {
                return BadRequest(request);
            }

            var boardId = await _gameBoardService.CreateGameBoardAsync(request.Board);

            var reply = new CreateBoardReply
            {
                BoardId = boardId,
            };

            return Ok(reply);
        }

        [ProducesResponseType(typeof(GetGameBoardReply), StatusCodes.Status200OK)]
        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetGameBoard(int boardId)
        {
            var gameBoard = await _gameBoardService.GetGameBoardAsync(boardId);

            if (gameBoard == null)
            {
                return NotFound();
            }

            var reply = new GetGameBoardReply
            {
                GameBoard = gameBoard,
            };

            return Ok(reply);
        }

        [ProducesResponseType(typeof(GetGameBoardReply), StatusCodes.Status200OK)]
        [HttpPut("{boardId}/next")]
        public async Task<IActionResult> GetGameBoardNextState(int boardId, [FromBody] UpdateNextGameBoardStateRequest request)
        {
            var gameBoard = await _gameBoardService.GetGameBoardNextStateAsync(boardId, request.NumberOfGenerations);

            if (gameBoard == null)
            {
                return NotFound();
            }

            var reply = new GetGameBoardReply
            {
                GameBoard = gameBoard,
            };

            return Ok(reply);
        }

        [ProducesResponseType(typeof(GetGameBoardReply), StatusCodes.Status200OK)]
        [HttpPut("{boardId}/final")]
        public async Task<IActionResult> GetGameBoardFinalState(int boardId, UpdateNextGameBoardStateRequest request)
        {
            var gameBoard = await _gameBoardService.GetGameBoardFinalStateAsync(boardId, request.NumberOfGenerations);

            if (gameBoard == null)
            {
                return NotFound();
            }

            var reply = new GetGameBoardReply
            {
                GameBoard = gameBoard,
            };

            return Ok(reply);
        }
    }
}