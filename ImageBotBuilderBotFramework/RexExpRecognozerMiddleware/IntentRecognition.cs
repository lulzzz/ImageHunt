using System.Collections.Generic;

namespace ImageBotBuilderBotFramework.RexExpRecognozerMiddleware
{
  public class IntentRecognition : IRecognizedIntents
  {
    public IntentRecognition()
    {
    }

    public Intent TopIntent { get; set; }
    public IList<Intent> Intents { get; set; } = new Intent[0];
  }
}