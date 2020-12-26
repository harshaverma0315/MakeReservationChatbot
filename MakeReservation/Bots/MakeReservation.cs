// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.10.3

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MakeReservation.Dialogs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace MakeReservation.Bots
{
    public class MakeReservationBot : ActivityHandler
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private DialogSet Dialogs;
        /// <summary>
        /// 
        /// </summary>
        private BotAccessor _botAccessor;
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="botAccessor"></param>
        /// <param name="introductionDialog"></param>
        /// <param name="makeReservationDialog"></param>
        public MakeReservationBot(BotAccessor botAccessor, IntroductionDialog introductionDialog, MakeReservationDialog makeReservationDialog)
        {
            _botAccessor = botAccessor;

            var dialogstate = _botAccessor.DialogStateAccessor;

            Dialogs = new DialogSet(dialogstate);
            Dialogs.Add(introductionDialog);
            Dialogs.Add(makeReservationDialog);
            Dialogs.Add(new TextPrompt("textPrompt"));
        }    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="turnContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var dialogContext = await Dialogs.CreateContextAsync(turnContext, cancellationToken);
            if (dialogContext.ActiveDialog == null)
            {
                await dialogContext.BeginDialogAsync(IntroductionDialog.DialogId, null, cancellationToken);
            }

            else if (dialogContext.ActiveDialog != null)
            {
                await dialogContext.ContinueDialogAsync(cancellationToken);
            }

         await _botAccessor.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);

            /*switch (turnContext.Activity.Text)
           {
               case "Make Reservation":

                   if (dialogContext.ActiveDialog == null)
                   {
                       await dialogContext.BeginDialogAsync(IntroductionDialog.DialogId, null, cancellationToken);
                   }

                   else if (dialogContext.ActiveDialog != null)
                   {
                       await dialogContext.ContinueDialogAsync(cancellationToken);
                   }

                   break; 

               case "Browse Menu":
                   break;
           }*/
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="membersAdded"></param>
        /// <param name="turnContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    var welcomeText = $"Hello and welcome, {member.Name}";
                    var welcomeText2 = "What would you like to do  today?";
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText), cancellationToken);
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText2), cancellationToken);
                }
            }
            await GetSuggestedActions(turnContext);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="turnContext"></param>
        /// <returns></returns>
        public static async Task<Activity> GetSuggestedActions(ITurnContext turnContext)
        {
            var messageActivity = turnContext.Activity.CreateReply();

            var suggestedActions = new SuggestedActions
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "Make Reservation", Type = ActionTypes.ImBack, Value = "Make Reservation" },
                    new CardAction() { Title = "Browse Menu", Type = ActionTypes.ImBack, Value = "Browse Menu" }
                }

            };

            messageActivity.SuggestedActions = suggestedActions;
            await turnContext.SendActivityAsync(messageActivity);
            return messageActivity;
        }

    }
}
