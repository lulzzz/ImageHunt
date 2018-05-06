using System.Collections.Generic;
using Microsoft.Bot.Schema;

namespace ImageBotBuilderBotFramework.RexExpRecognozerMiddleware
{
  public class Intent
  {
    public string Name { get; set; }
    public double Score { get; set; }

    public IList<Entity> Entities { get; } = new List<Entity>();
  }
}