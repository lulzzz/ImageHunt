﻿using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ImageHuntTelegramBot
{
  public class TelegramAdapter : IAdapter
  {
    private readonly ITelegramBotClient _client;

    public TelegramAdapter(ITelegramBotClient client)
    {
      _client = client;
    }
    public async Task SendActivity(IActivity activity)
    {
      switch (activity.ActivityType)
      {
        case ActivityType.Message:
          await SendMessage(activity);
          break;
        case ActivityType.UpdateMessage:
          break;
        case ActivityType.CallbackQuery:
          break;
        case ActivityType.None:
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public async Task<Activity> CreateActivityFromUpdate(Update update)
    {
      long chatId = 0;
      string text = null;
      ActivityType activityType = ActivityType.None;

      switch (update.Type)
      {
        case UpdateType.MessageUpdate:
          chatId = update.Message.Chat.Id;
          text = update.Message.Text;
          activityType = ActivityType.Message;
          break;
        case UpdateType.CallbackQueryUpdate:
          chatId = update.CallbackQuery.Message.Chat.Id;
          activityType = ActivityType.CallbackQuery;
          text = update.CallbackQuery.Message.Text;
          break;
      }
      var activity = new Activity();
      activity.ActivityType = activityType;
      activity.ChatId = chatId;
      activity.Text = text;
      return activity;
    }

    private async Task SendMessage(IActivity activity)
    {
      await _client.SendTextMessageAsync(activity.ChatId, activity.Text);
    }
  }
}