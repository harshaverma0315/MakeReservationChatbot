using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakeReservation.Dialogs
{
    public class IntroductionDialog : BaseDialog
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static String DialogId => "IntroductionDialog";

        public string userName;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public IntroductionDialog()
        {
            var waterfallsteps = new WaterfallStep[]
            {
                HandleIntroduction,
                PhoneNumber,
                DayConfirmation,
                HandleEndOfDialog
            };
           AddDialog(new WaterfallDialog(DialogId, waterfallsteps));
           AddDialog(new TextPrompt("textPrompt"));
           AddDialog(new ChoicePrompt("choicePrompt"));
           AddDialog(new ConfirmPrompt("confirmPrompt"));
           AddDialog(new DateTimePrompt("DateTimePrompt"));
           AddDialog(new NumberPrompt<int>("numberPrompt"));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<DialogTurnResult> HandleIntroduction(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var promptOptions = new PromptOptions()
            {
                Prompt = MessageFactory.Text("Please enter your name to make a reservation")
            };

            return await stepContext.PromptAsync("textPrompt", promptOptions, cancellationToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<DialogTurnResult> PhoneNumber(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
             userName = (string)stepContext.Result;
            var promptOptions = new PromptOptions()
            {
                Prompt = MessageFactory.Text("Thank you, " + userName + "! Please enter your mobile number")
            };

            var promptUser = stepContext.PromptAsync("textPrompt", promptOptions, cancellationToken);
            return await promptUser;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<DialogTurnResult> DayConfirmation(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var promptOptions = new PromptOptions()
            {
                Prompt = MessageFactory.Text("When would you like to come?"),
                Choices = new List<Choice> { new Choice("Breakfast"), new Choice("Lunch"), new Choice("Dinner")},
            };
        return await stepContext.PromptAsync("choicePrompt", promptOptions, cancellationToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<DialogTurnResult> HandleEndOfDialog(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

    }
}
