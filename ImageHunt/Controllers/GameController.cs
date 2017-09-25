using System.Collections.Generic;
using ImageHunt.Model;
using ImageHunt.Model.Node;
using ImageHunt.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageHunt.Controllers
{
  [Route("api/[Controller]")]
  public class GameController : Controller
  {
    private readonly IGameService _gameService;
    private readonly IImageService _imageService;

    public GameController(IGameService gameService, IImageService imageService)
    {
      _gameService = gameService;
      _imageService = imageService;
    }

    [HttpGet("ById/{gameId}")]
    public IActionResult GetGameById(int gameId)
    {
      return Ok(_gameService.GetGameById(gameId));
    }
    [HttpGet("ByAdminId/{adminId}")]
    public IActionResult GetGames(int adminId)
    {
      return Ok(_gameService.GetGamesForAdmin(adminId));
    }
    [HttpPost("{adminId}")]
    public IActionResult CreateGame(int adminId, [FromBody] Game newGame)
    {
      return Ok(_gameService.CreateGame(adminId, newGame));
    }
    [HttpPut("{gameId}")]
    public IActionResult AddNode(int gameId, [FromBody] Node node)
    {
      _gameService.AddNode(gameId, node);
      return Ok();
    }
    [HttpPost("AddPictures/{gameId}")]
    public IEnumerable<Node> AddImageNodes(int gameId, [FromBody]List<Picture> images)
    {
      var nodes = new List<Node>();
      foreach (var picture in images)
      {
        _imageService.AddPicture(picture);
        var coordinates = _imageService.ExtractLocationFromImage(picture);
        var node = new PictureNode
        {
          Image = picture,
          Latitude = coordinates.Item1,
          Longitude = coordinates.Item2
        };
        _gameService.AddNode(gameId, node);
        nodes.Add(node);
      }
      return nodes;
    }
  }
}
