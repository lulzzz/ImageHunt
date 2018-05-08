﻿using System.Collections.Generic;
using Autofac;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ImageHuntTelegramBot.Controllers
{
  public class ContextHub
  {
    private readonly IContainer _container;
    private Dictionary<long, ITurnContext> _turnContexts = new Dictionary<long, ITurnContext>();

    public ContextHub(IContainer container)
    {
      _container = container;
    }
    public virtual ITurnContext GetContext(Update update)
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
          break;
      }

      if (!_turnContexts.ContainsKey(chatId))
      {
        var turnContext = _container.Resolve<ITurnContext>();
        turnContext.ChatId = chatId;
        turnContext.Activity = new Activity(){Text = text, ActivityType = activityType};
        _turnContexts.Add(chatId, turnContext);
      }

      return _turnContexts[chatId];
    }
  }
}