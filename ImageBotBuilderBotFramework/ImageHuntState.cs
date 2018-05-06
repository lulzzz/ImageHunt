using System.Collections.Generic;

namespace ImageBotBuilderBotFramework
{
    /// <summary>
    /// Class for storing conversation state. 
    /// </summary>
    public class ImageHuntState : Dictionary<string, object>
    {
      private int _gameId;
      private int _teamId;
      private const string GameIdKey = "GameId";
      private const string TeamIdKey = "TeamId";
        public int TurnCount { get; set; } = 0;

      public int GameId
      {
        get => (int) this[GameIdKey];
        set => this[GameIdKey] = value;
      }

      public int TeamId
      {
        get => (int) this[TeamIdKey];
        set => this[TeamIdKey] = value;
      }
    }
}
