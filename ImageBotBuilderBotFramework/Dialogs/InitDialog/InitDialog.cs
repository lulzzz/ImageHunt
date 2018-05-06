using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts;

namespace ImageBotBuilderBotFramework.Dialogs.InitDialog
{
    public class InitDialog
    {
      public async Task AskGameIdStep(DialogContext dialogContext, object result, SkipStepFunction next)
      {
        var state = dialogContext.Context.GetConversationState<ImageHuntState>();
        state.GameId = (result as NumberResult<int>).Value;
        //await dialogContext.Prompt("Merci de m'indiquer l'id de la partie")
      }
    }
}
