using System.Collections.Generic;
using System.Threading.Tasks;
using ImageHuntTelegramBot.WebServices;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts;
using Microsoft.Recognizers.Text;

namespace ImageBotBuilderBotFramework.Dialogs
{
    public class InitDialog : IDialog
  {
      private readonly IGameWebService _gameWebService;
      private readonly ITeamWebService _teamWebService;

      public static class PromptStep
      {
        public const string GetGameId = "GetGameId";
        public const string GetTeamId = "GetTeamId";
        public const string InitSummary = "InitSummary";
      }

      public InitDialog(IGameWebService gameWebService, ITeamWebService teamWebService)
      {
        _gameWebService = gameWebService;
        _teamWebService = teamWebService;
      }

      public void FillDialog(DialogSet dialogSet)
      {
        dialogSet.Add(PromptStep.GetGameId,
          new Microsoft.Bot.Builder.Dialogs.NumberPrompt<int>(Culture.French, GameIdValidator));
        dialogSet.Add(PromptStep.GetTeamId,
          new Microsoft.Bot.Builder.Dialogs.NumberPrompt<int>(Culture.French, TeamIdValidator));
        dialogSet.Add(PromptStep.InitSummary,
          new WaterfallStep[] { GetGameIdStep, GetTeamIdStep, InitSummaryStep });

      }

    public string FirstStep => PromptStep.InitSummary;

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
        state.Game = await _gameWebService.GetGameById(state.GameId);
        await dc.Context.SendActivity($"Vous participez à la partie {state.Game.Name} qui débutera {state.Game.StartDate}.");
        await dc.Prompt(PromptStep.GetTeamId, "Merci de m'indiquer l'id de l'équipe");
      }

      private async Task InitSummaryStep(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
      {
        var state = dc.Context.GetConversationState<ImageHuntState>();
        state.TeamId = (args as NumberResult<int>).Value;
        state.Team = await _teamWebService.GetTeamById(state.TeamId);
        await dc.Context.SendActivity($"Ce chat est celui de l'équipe {state.Team.Name}. Merci, le chat est prêt, bonne partie!");
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

    }
}
