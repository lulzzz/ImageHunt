using System;
using ImageHunt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageHunt.Controllers
{
    [Route("api/[controller]")]
  [Authorize]
    public class PlayerController : Controller
    {
      private readonly IPlayerService _playerService;

      public PlayerController(IPlayerService playerService)
      {
        _playerService = playerService;
      }
      [HttpPost("CreatePlayer/{name}/{chatLogin}")]
      public IActionResult CreatePlayer(string name, string chatLogin)
      {
        return Ok(_playerService.CreatePlayer(name, chatLogin));
      }
    [HttpPut("JoinTeam/{teamId}/{playerId}")]
      public IActionResult JoinTeam(int teamId, int playerId)
      {
        return Ok(_playerService.JoinTeam(teamId, playerId ));
      }

      public IActionResult StartPlayer(string playername)
      {
        _playerService.StartPlayer(playername);
        var nextNode = _playerService.NextNodeForPlayer(playername, 0,0);
        return Ok(nextNode);
      }

      public IActionResult NextNodeForPlayer(string playername)
      {
        return Ok(_playerService.NextNodeForPlayer(playername,0,0));
      }

      public IActionResult UploadImage(string playername, int latitude, int longitude, byte[] image)
      {
        _playerService.UploadImage(playername, latitude, longitude, image);
        return Ok();
      }
      [HttpGet("PlayerByChatId/{chatId}")]
      public IActionResult PlayerByChatId(string chatId)
      {
        return Ok(_playerService.GetPlayerByChatId(chatId));
      }
    }
}
