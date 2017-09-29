using System.Collections.Generic;
using System.Linq;
using ImageHunt.Model;
using ImageHunt.Model.Node;
using ImageHunt.Request;
using ImageHunt.Response;
using ImageHunt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
      var gameById = _gameService.GetGameById(gameId);
      return Ok(gameById);
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
    [HttpPost("AddNode/{gameId}")]
    public IActionResult AddNode(int gameId, [FromBody] AddNodeRequest nodeRequest)
    {
      if (nodeRequest == null)
        return BadRequest("Node is missing");
      var node = NodeFactory.CreateNode(nodeRequest.NodeType);
      node.Name = nodeRequest.Name;
      node.Latitude = nodeRequest.Latitude;
      node.Longitude = nodeRequest.Longitude;
      switch (nodeRequest.NodeType)
      {
        case "TimerNode":
          var timerNode = node as TimerNode;
          timerNode.Delay = nodeRequest.Duration;
          break;
         case "ObjectNode":
          var objectNode = node as ObjectNode;
          objectNode.Action = nodeRequest.Action;
          break;
        case "QuestionNode":
          var questionNode = node as QuestionNode;
          questionNode.Question = nodeRequest.Question;
          questionNode.Answers = nodeRequest.Answers.Select(a => new Answer() {Node = questionNode, Response = a}).ToList();
          break;
     }
      _gameService.AddNode(gameId, node);
      return Ok();
    }
    [HttpPut("AddPictures/{gameId}")]
    public IActionResult AddImageNodes(int gameId, List<IFormFile> files)
    {
      foreach (var file in files)
      {
        using (var fileStream = file.OpenReadStream())
        {
          byte[] bytes = new byte[fileStream.Length];
          fileStream.Read(bytes, 0, (int)fileStream.Length);
          var picture = new Picture(){Image = bytes};
          //_imageService.AddPicture(picture);
          var coordinates = _imageService.ExtractLocationFromImage(picture);
          var node = new PictureNode
          {
            Image = picture,
            Latitude = coordinates.Item1,
            Longitude = coordinates.Item2
          };
          _gameService.AddNode(gameId, node);
        }
      }
      return Ok();
    }
    [HttpPost("CenterGameByNodes/{gameId}")]
    public void SetCenterOfGameByNodes(int gameId)
    {
      _gameService.SetCenterOfGameByNodes(gameId);
    }
    [HttpGet("NodesRelations/{gameId}")]
    public IActionResult GetNodesRelations(int gameId)
    {
      var nodes = _gameService.GetNodes(gameId);
      var resNodes = new List<NodeResponse>();
      foreach (var node in nodes)
      {
        var resNode = new NodeResponse(){NodeId = node.Id, ChildNodeId = node.Children.Select(c=>c.Id).ToArray()};
        resNodes.Add(resNode);
      }
      return Ok(resNodes);
    }
  }
}
