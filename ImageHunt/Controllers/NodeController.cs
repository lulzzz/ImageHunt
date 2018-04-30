using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageHunt.Request;
using ImageHunt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageHunt.Controllers
{
  [Route("api/[controller]")]
  #if !DEBUG
  [Authorize]
  #endif
  public class NodeController : Controller
  {
    private INodeService _nodeService;

    public NodeController(INodeService nodeService)
    {
      _nodeService = nodeService;
    }

    [HttpPut("AddRelationToNode")]
    public IActionResult AddRelationToNode([FromBody]NodeRelationRequest relationRequest)
    {
      _nodeService.AddChildren(relationRequest.NodeId, relationRequest.ChildrenId);
      if (relationRequest.AnswerId != 0)
        _nodeService.LinkAnswerToNode(relationRequest.AnswerId, relationRequest.ChildrenId);
      return Ok();
    }
    [HttpPut("RemoveRelationToNode")]
    public IActionResult RemoveRelationToNode([FromBody]NodeRelationRequest relationRequest)
    {
      _nodeService.RemoveChildren(relationRequest.NodeId, relationRequest.ChildrenId);
      if (relationRequest.AnswerId != 0)
        _nodeService.UnlinkAnswerToNode(relationRequest.AnswerId);
      return Ok();
    }
    [HttpPut("AddRelationsWithAnswers")]
    public void AddRelationsWithAnswers([FromBody]IEnumerable<NodeRelationRequest> relationsRequest)
    {
      var groupsNode = relationsRequest.GroupBy(n => n.NodeId);
      foreach (var gNode in groupsNode)
      {
        var questionNode = _nodeService.GetNode(gNode.Key);
        _nodeService.RemoveAllChildren(questionNode);
        foreach (var nodeRelationRequest in gNode)
        {
          _nodeService.AddChildren(questionNode.Id, nodeRelationRequest.ChildrenId);
          _nodeService.LinkAnswerToNode(nodeRelationRequest.AnswerId, nodeRelationRequest.ChildrenId);
        }
      }
    }
    [HttpDelete("RemoveNode/{nodeId}")]
    public IActionResult RemoveNode(int nodeId)
    {
      var node = _nodeService.GetNode(nodeId);
      _nodeService.RemoveNode(node);
      return Ok();
    }
  }
}
