using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakeReservation.Dialogs
{
    public class MakeReservationDialog : BaseDialog
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static String DialogId => "MakeReservationDialog";
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        public MakeReservationDialog()
        {
            var waterfallsteps = new WaterfallStep[]
            {
                HandleIntroduction,
                HandleYesNo,
                NumberOfParticipants,
                DateAndTime,
                Summary,
                HandleEndOfDialog

            };
            AddDialog(new WaterfallDialog(DialogId, waterfallsteps));
            AddDialog(new TextPrompt("textPrompt"));

        }

        private async Task<DialogTurnResult> HandleIntroduction(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var promptOptions = new PromptOptions()
            {
                Prompt = MessageFactory.Text("Please enter your name to make a reservation")
            };
            var promptUser = stepContext.PromptAsync("textPrompt", promptOptions, cancellationToken);
            return await promptUser;

        }

        private Task<DialogTurnResult> HandleYesNo(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task<DialogTurnResult> NumberOfParticipants(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        private Task<DialogTurnResult> DateAndTime(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task<DialogTurnResult> Summary(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task<DialogTurnResult> HandleEndOfDialog(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

      
    }
}
