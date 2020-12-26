using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeReservation
{
    public class BotAccessor
    {
        public ConversationState ConversationState;

        public IStatePropertyAccessor<DialogState> DialogStateAccessor;

        public static String DialogStateAccessorName = $"{nameof(DialogStateAccessor)}.DialogState ";
  
        public BotAccessor(ConversationState conversationState)
        {
            ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        }
    }
}
