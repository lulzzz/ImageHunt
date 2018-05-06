using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts;
using Microsoft.Bot.Schema;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Recognizers.Text;

namespace ImageBotBuilderBotFramework
{
  public static class PromptStep
  {
    public const string GetGameId = "GetGameId";
    public const string GetTeamId = "GetTeamId";
    public const string InitSummary = "InitSummary";
  }
  public class ImageHuntBot : IBot
  {
    private readonly IContainer _container;
    private readonly DialogSet _dialogs;

    public ImageHuntBot()
    {
      //_container = container;
      _dialogs = new DialogSet();
      _dialogs.Add(PromptStep.GetGameId,
        new Microsoft.Bot.Builder.Dialogs.NumberPrompt<int>(Culture.French, GameIdValidator));
      _dialogs.Add(PromptStep.GetTeamId, 
        new Microsoft.Bot.Builder.Dialogs.NumberPrompt<int>(Culture.French, TeamIdValidator));
      _dialogs.Add(PromptStep.InitSummary, 
        new WaterfallStep[] { GetGameIdStep, GetTeamIdStep, InitSummaryStep });
    }


    private async Task GetGameIdStep(DialogContext dc, 
                                     IDictionary<string, object> args, 
                                     SkipStepFunction next)
    {
      await dc.Prompt(PromptStep.GetGameId, "Merci de m'indiquer l'id de la partie");
    }
    private async Task GetTeamIdStep(DialogContext dc, 
                                     IDictionary<string, object> args, 
                                     SkipStepFunction next)
    {
      var state = dc.Context.GetConversationState<ImageHuntState>();
      state.GameId = (args as NumberResult<int>).Value;
      await dc.Prompt(PromptStep.GetTeamId, "Merci de m'indiquer l'id de l'équipe");
    }

    private async Task InitSummaryStep(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
    {
      var state = dc.Context.GetConversationState<ImageHuntState>();
      state.TeamId = (args as NumberResult<int>).Value;
      await dc.Context.SendActivity($"Ce chat est celui de l'équipe {state.TeamId}. Merci, le chat est prêt, bonne partie!");
      await dc.End();
    }

    private async Task TeamIdValidator(ITurnContext context, NumberResult<int> tovalidate)
    {
      if (tovalidate.Value == 0)
      {
        tovalidate.Status = PromptStatus.OutOfRange;
        await context.SendActivity("L'Id doit être différent de 0");
      }
    }

    private async Task GameIdValidator(ITurnContext context, NumberResult<int> tovalidate)
    {
      if (tovalidate.Value == 0)
      {
        tovalidate.Status = PromptStatus.OutOfRange;
        await context.SendActivity("L'Id doit être différent de 0");
      }
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
                await dialogCtx.Begin(PromptStep.InitSummary);

                break;
            }
          }
          break;
      }
    }
  }
}
