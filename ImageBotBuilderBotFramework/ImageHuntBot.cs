using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using ImageBotBuilderBotFramework.Dialogs;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text;

namespace ImageBotBuilderBotFramework
{
  public class ImageHuntBot : IBot
  {
    private readonly IContainer _container;
    private readonly DialogSet _dialogs;
    private InitDialog _initDialog;
    private readonly StartDialog _startDialog;

    public ImageHuntBot(InitDialog initDialog, StartDialog startDialog)
    {
      //_container = container;
      _initDialog = initDialog;
      _startDialog = startDialog;
      _dialogs = new DialogSet();
      _initDialog.FillDialog(_dialogs);
      _startDialog.FillDialog(_dialogs);
    }


    /// <summary>
    /// Every Conversation turn for our EchoBot will call this method. In here
    /// the bot checks the Activty type to verify it's a message, bumps the 
    /// turn conversation 'Turn' count, and then echoes the users typing
    /// back to them. 
    /// </summary>
    /// <param name="context">Turn scoped context containing all the data needed
    /// for processing this conversation turn. </param>        
    public async Task OnTurn(ITurnContext context)
    {
      var state = context.GetConversationState<ImageHuntState>();
      var dialogCtx = _dialogs.CreateContext(context, state);
      switch (context.Activity.Type)
      {
        case ActivityTypes.Message:
          await dialogCtx.Continue();
        
          if (!context.Responded)
          {
            switch (context.Activity.Text)
            {
              case var s when s.StartsWith("/init"):
                await dialogCtx.Begin(_startDialog.FirstStep);

                break;
              case var s when s.StartsWith("/startgame"):
                await dialogCtx.Begin(_startDialog.FirstStep);
                break;
            }
          }
          break;
      }
    }
  }
}
