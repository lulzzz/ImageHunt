﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using FakeItEasy;
using ImageHuntBot.Dialogs;
using ImageHuntTelegramBot;
using ImageHuntTelegramBot.Dialogs;
using ImageHuntWebServiceClient.Request;
using ImageHuntWebServiceClient.WebServices;
using Microsoft.Extensions.Logging;
using NFluent;
using Telegram.Bot.Types;
using TestUtilities;
using Xunit;

namespace ImageHuntBotTest.Dialog
{
    public class ResetDialogTest : BaseTest
    {
      private IResetDialog _target;
      private ILogger _logger;
        private IActionWebService _actionWebService;

        public ResetDialogTest()
      {
        _testContainerBuilder.RegisterType<ResetDialog>();
          _actionWebService = A.Fake<IActionWebService>();
          _testContainerBuilder.RegisterInstance(_actionWebService).As<IActionWebService>();
        _logger = A.Fake<ILogger<ResetDialog>>();
        _testContainerBuilder.RegisterInstance(_logger).As<ILogger<ResetDialog>>();

        _container = _testContainerBuilder.Build();
        _target = _container.Resolve<ResetDialog>();
      }

      [Fact]
      public async Task Begin()
      {
        // Arrange
        var activity = new Activity()
        {
          ActivityType = ActivityType.Message,
          ChatId = 15,
        };
        var turnContext = A.Fake<ITurnContext>();
        A.CallTo(() => turnContext.Activity).Returns(activity);
        var imageHuntState = new ImageHuntState();
        A.CallTo(() => turnContext.GetConversationState<ImageHuntState>()).Returns(imageHuntState);
      // Act
      await _target.Begin(turnContext);
        // Assert
        A.CallTo(() => turnContext.ResetConversationStates<ImageHuntState>()).MustHaveHappened();
          A.CallTo(() => turnContext.ReplyActivity(A<string>._)).MustHaveHappened();
          A.CallTo(() => turnContext.End()).MustHaveHappened();
        A.CallTo(() => _logger.Log(A<LogLevel>._, A<EventId>._, A<object>._, A<Exception>._,
            A<Func<object, Exception, string>>._))
          .WithAnyArguments()
          .MustHaveHappened();
      }
  }
}
