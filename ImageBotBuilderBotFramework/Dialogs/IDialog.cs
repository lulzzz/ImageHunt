using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace ImageBotBuilderBotFramework.Dialogs
{
    public interface IDialog
    {
      void FillDialog(DialogSet dialogSet);
      string FirstStep { get; }
    }
}
