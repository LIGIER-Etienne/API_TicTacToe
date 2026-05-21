using Microsoft.AspNetCore.Mvc;
using TicTacToe.Dtos;
using TicTacToe.Services;

namespace TicTacToe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicTacToeController(GameService gameService) : ControllerBase
{
    private readonly GameService _gameService = gameService;

    /// <summary>
    /// Récupère l'état actuel du jeu
    /// </summary>
    [HttpGet("state")]
    public ActionResult<GameStateDto> GetState()
    {
        GameStateDto gameStateDto = _gameService.GetCurrentState();
        return Ok(gameStateDto);
    }

    /// <summary>
    /// Effectue un coup du joueur humain
    /// </summary>
    [HttpPost("move")]
    public ActionResult<MoveResponseDto> Move([FromBody] MoveRequestDto request)
    {
        var response = _gameService.MakeHumanMove(request.Row, request.Column);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Réinitialise une nouvelle partie
    /// </summary>
    [HttpPost("reset")]
    public ActionResult<GameStateDto> Reset()
    {
        _gameService.Reset();
        return Ok(_gameService.GetCurrentState());
    }
}
