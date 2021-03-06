﻿using System;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace ImageHuntTelegramBot
{
    public class Activity : IActivity
    {
      public ActivityType ActivityType { get; set; }
      public string Text { get; set; }
      public long ChatId { get; set; }
      public PhotoSize[] Pictures { get; set; }
      public Location Location { get; set; }

      public string Command
      {
        get
        {
          var regex = new Regex(@"\/\w*");
          return regex.Match(Text).Value;
        }
      }

      public Document Document { get; set; }
    }

  public enum ActivityType
  {
    None,
    Message,
    UpdateMessage,
    CallbackQuery

  }

  public interface IActivity
  {
    ActivityType ActivityType { get; set; }
    string Text { get; set; }
    long ChatId { get; set; }
    PhotoSize[] Pictures { get; set; }
    Location Location { get; set; }
    string Command { get; }
    Document Document { get; set; }
  }
}
