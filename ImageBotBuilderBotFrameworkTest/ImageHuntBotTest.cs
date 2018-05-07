using System;
using System.Threading.Tasks;
using Autofac;
using FakeItEasy;
using ImageBotBuilderBotFramework;
using ImageBotBuilderBotFramework.Dialogs;
using ImageHuntTelegramBot.WebServices;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using TestUtilities;
using Xunit;

namespace ImageBotBuilderBotFrameworkTest
{
    public class ImageHuntBotTest : BaseTest
    {
      private ImageHuntBot _target;
      private IGameWebService _gameWebService;
      private ITeamWebService _teamWebService;

      public ImageHuntBotTest()
      {
        _testContainerBuilder.RegisterType<ImageHuntBot>();


        _gameWebService = A.Fake<IGameWebService>();
        _testContainerBuilder.RegisterInstance(_gameWebService);
        _teamWebService = A.Fake<ITeamWebService>();
        _testContainerBuilder.RegisterInstance(_teamWebService);
        _testContainerBuilder.RegisterType<InitDialog>();
        _testContainerBuilder.RegisterType<StartDialog>();

        var testContainer = _testContainerBuilder.Build();

        _target = testContainer.Resolve<ImageHuntBot>();
      }
        [Fact]
        public async Task Turn_Init()
        {
          // Arrange
          var context = A.Fake<ITurnContext>();
          var activity1 = new Activity(type: ActivityTypes.Message, text:"/init");
          var activity2 = new Activity(type: ActivityTypes.Message, text:"15");
          var activity3 = new Activity(type: ActivityTypes.Message, text:"96");
      // Act
          A.CallTo(() => context.Activity).Returns(activity1);
          await _target.OnTurn(context);
          A.CallTo(() => context.Activity).Returns(activity2);
          await _target.OnTurn(context);
          A.CallTo(() => context.Activity).Returns(activity3);
          await _target.OnTurn(context);
          // Assert
        }
    }
}
