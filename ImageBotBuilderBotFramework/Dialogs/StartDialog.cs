using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts;
using Microsoft.Bot.Builder.Prompts.Choices;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text;
using ChoicePrompt = Microsoft.Bot.Builder.Dialogs.ChoicePrompt;

namespace ImageBotBuilderBotFramework.Dialogs
{
    public class StartDialog : IDialog
    {
      class PromptStep
      {
        public const string StartSummaryStep = "StartGame";
        public const string AskTeamCompleteStep = "AskTeamComplete";
      }
      public void FillDialog(DialogSet dialogSet)
      {
        dialogSet.Add(PromptStep.AskTeamCompleteStep, 
          new ChoicePrompt(Culture.French){Style = ListStyle.Inline});

        dialogSet.Add(PromptStep.StartSummaryStep, 
                      new WaterfallStep[] {ChoiceTeamCompleteStep, PromptTeamCompleteStep });
      }

      private async Task PromptTeamCompleteStep(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
      {
      var selectedCard = ((ChoiceResult) args).Value.Value;
        var activity = dc.Context.Activity;
        switch (selectedCard)
        {
        case "Yes":
          break;
        case "No":
          break;
        }
        await dc.End();
      }

      public string FirstStep => PromptStep.StartSummaryStep;

      private async Task ChoiceTeamCompleteStep(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
      {
      await dc.Prompt(PromptStep.AskTeamCompleteStep,
                      "Vous allez démarrer la chasse, toute votre équipe est prête?",
          new ChoicePromptOptions()
          {
            Choices = new List<Choice>()
            {
              new Choice(){Value="Yes", Synonyms = {"Yes", "Oui"}},
              new Choice(){Value = "No", Synonyms = {"No", "Non"}}
            }
          });
    }

    private Task Validator(ITurnContext context, ChoiceResult toValidate)
      {
        throw new NotImplementedException();
      }
    }
}
