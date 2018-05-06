using System.Collections.Generic;
using ImageHuntTelegramBot.Responses;

namespace ImageBotBuilderBotFramework
{
    /// <summary>
    /// Class for storing conversation state. 
    /// </summary>
    public class ImageHuntState : Dictionary<string, object>
    {
      private const string GameKey = "Game";
      private const string TeamKey = "Team";
      private const string GameIdKey = "GameId";
      private const string TeamIdKey = "TeamId";
        public int TurnCount { get; set; } = 0;

      public GameResponse Game
      {
        get => (GameResponse) this[GameKey];
        set => this[GameKey] = value;
      }

      public TeamResponse Team
      {
        get => (TeamResponse) this[TeamKey];
        set => this[TeamKey] = value;
      }
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
