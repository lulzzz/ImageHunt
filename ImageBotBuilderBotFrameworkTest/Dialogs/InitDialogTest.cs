using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FakeItEasy;
using ImageBotBuilderBotFramework.Dialogs;
using ImageHuntTelegramBot.WebServices;
using Microsoft.Bot.Builder.Dialogs;
using TestUtilities;
using Xunit;

namespace ImageBotBuilderBotFrameworkTest.Dialogs
{
    public class InitDialogTest : BaseTest
    {
      private InitDialog _initDialog;
      private IGameWebService _gameService;
      private ITeamWebService _teamService;

      public InitDialogTest()
      {
        _gameService = A.Fake<IGameWebService>();
        _testContainerBuilder.RegisterInstance(_gameService);
        _teamService = A.Fake<ITeamWebService>();
        _testContainerBuilder.RegisterInstance(_teamService);
        _testContainerBuilder.RegisterType<InitDialog>();
        var container = _testContainerBuilder.Build();

        _initDialog = container.Resolve<InitDialog>();
      }

      [Fact]
      public void FillDialog()
      {
        // Arrange
        var dialogSet = new DialogSet();
        // Act
        _initDialog.FillDialog(dialogSet);
        // Assert
      }

    }
}
