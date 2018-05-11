﻿using System;
using System.Threading.Tasks;

namespace ImageHuntTelegramBot.Dialogs.Prompts
{
  public class NumberPrompt<T> : PromptDialog where T : struct 
  {
    private string _replyUser;

    public NumberPrompt(string promptMessage, PromptResult promptResult) 
      : base(promptMessage, promptResult)
    {
    }

    public override Task Continue(ITurnContext turnContext)
    {
      _replyUser = turnContext.Activity.Text;
      _prompResult?.Invoke(turnContext, Value);
      return base.Continue(turnContext);
    }


    public T Value
    {
      get
      {
        switch (Type.GetTypeCode(typeof(T)))
        {
          case TypeCode.Int32:
            return (T) (object) Convert.ToInt32(_replyUser);
          case TypeCode.Double:
            return (T) (object) Convert.ToDouble(_replyUser);
        }
        return default(T);
      }
    }
  }
}