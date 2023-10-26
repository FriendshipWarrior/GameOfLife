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
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest request)
        {
            if (request?.Board == null)
            {
                return BadRequest(request);
            }

            var boardId = await _gameBoardService.CreateGameBoard(request);

            var reply = new CreateBoardReply
            {
                BoardId = boardId,
            };

            return Ok(reply);
        }

        [ProducesResponseType(typeof(GetBoardReply), StatusCodes.Status200OK)]
        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetBoardNextState(int boardId, [FromQuery] int numberOfStates)
        {
            if (numberOfStates < 0) 
            {
                return BadRequest(numberOfStates);
            }

            var gameBoard = await _gameBoardService.GetBoardNextState(boardId, numberOfStates);

            if (gameBoard == null)
            {
                return NotFound();
            }

            var reply = new GetBoardReply
            {
                GameBoard = gameBoard,
            };

            return Ok(reply);
        }

        [ProducesResponseType(typeof(GetBoardReply), StatusCodes.Status200OK)]
        [HttpGet("{boardId}/final")]
        public async Task<IActionResult> GetBoardFinalState(int boardId, [FromQuery] int numberOfAttempts)
        {
            if (numberOfAttempts < 1)
            {
                return BadRequest(numberOfAttempts);
            }

            var gameBoard = await _gameBoardService.GetBoardFinalState(boardId, numberOfAttempts);

            var reply = new GetBoardReply
            {
                GameBoard = gameBoard,
            };

            return Ok(reply);
        }
    }
}