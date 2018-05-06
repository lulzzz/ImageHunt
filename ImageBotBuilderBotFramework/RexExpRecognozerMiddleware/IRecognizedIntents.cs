using System.Collections.Generic;

namespace ImageBotBuilderBotFramework.RexExpRecognozerMiddleware
{
  public interface IRecognizedIntents
  {
    Intent TopIntent { get; set; }
    IList<Intent> Intents { get; set; }
  }
}