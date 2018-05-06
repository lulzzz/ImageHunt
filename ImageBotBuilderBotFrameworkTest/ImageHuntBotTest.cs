using System;
using System.Threading.Tasks;
using Autofac;
using FakeItEasy;
using ImageBotBuilderBotFramework;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using TestUtilities;
using Xunit;

namespace ImageBotBuilderBotFrameworkTest
{
    public class ImageHuntBotTest : BaseTest
    {
      private ContainerBuilder _containerBuilder;
      private IContainer _container;
      private ImageHuntBot _target;

      public ImageHuntBotTest()
      {
        _testContainerBuilder.RegisterType<ImageHuntBot>();


        _containerBuilder = new ContainerBuilder();



        _container = _containerBuilder.Build();
        _testContainerBuilder.RegisterInstance(_container);
        var testContainer = _testContainerBuilder.Build();

        _target = testContainer.Resolve<ImageHuntBot>();
      }
        [Fact]
        public async Task Turn_Init()
        {
          // Arrange
          var context = A.Fake<ITurnContext>();
          var activity = new Activity(type: ActivityTypes.Message, text:"/init");
          A.CallTo(() => context.Activity).Returns(activity);
          // Act
          await _target.OnTurn(context);
          // Assert
        }
    }
}
