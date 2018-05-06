namespace ImageBotBuilderBotFramework.RexExpRecognozerMiddleware
{
  public class RegExpRecognizerSettings
  {
    /// <summary>
    /// Minimum score, on a scale from 0.0 to 1.0, that should be returned for a matched 
    /// expression.This defaults to a value of 0.0. 
    /// </summary>
    public double MinScore { get; set; } = 0.0;
  }
}