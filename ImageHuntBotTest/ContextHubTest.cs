﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FakeItEasy;
using ImageHuntTelegramBot;
using ImageHuntTelegramBot.Controllers;
using NFluent;
using Telegram.Bot.Types;
using TestUtilities;
using Xunit;

namespace ImageHuntBotTest
{
    public class ContextHubTest : BaseTest
    {
      private ContextHub _target;
      private ITurnContext _turnContext;

      public ContextHubTest()
      {
        _turnContext = A.Fake<ITurnContext>();
        _testContainerBuilder.RegisterInstance(_turnContext);
        _container = _testContainerBuilder.Build();
        _target = new ContextHub(_container);
      }
      [Fact]
      public void GetContext_Message()
      {
        // Arrange
        var update = new Update() {Message = new Message() {Text = "toto", Chat = new Chat() {Id = 15}}};
        // Act
        var context = _target.GetContext(update);
        // Assert
        Check.That(context).Equals(_turnContext);
        Check.That(context.ChatId).Equals(15);
        Check.That(context.Activity).IsNotNull();
        Check.That(context.Activity.Text).Equals(update.Message.Text);
        Check.That(context.Activity.ActivityType).Equals(ActivityType.Message);
      }

      [Fact]
      public void GetContext_second_message()
      {
      // Arrange
        var update1 = new Update() { Message = new Message() { Text = "toto", Chat = new Chat() { Id = 15 } } };
        var update2 = new Update() { Message = new Message() { Text = "tata", Chat = new Chat() { Id = 15 } } };
        // Act
        var context1 = _target.GetContext(update1);
        var context2 = _target.GetContext(update2);
        // Assert
        Check.That(context1).Equals(context2);
        Check.That(context2.Activity.Text).Equals(update2.Message.Text);
      }
    [Fact]
      public void GetContext_CallbackQuery()
      {
        // Arrange
        var update = new Update() {CallbackQuery = new CallbackQuery(){Message = new Message(){Chat = new Chat(){Id = 15}}}};
        // Act
        var context = _target.GetContext(update);
        // Assert
        Check.That(context).Equals(_turnContext);
        Check.That(context.ChatId).Equals(15);
      }
    }
}
