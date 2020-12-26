using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakeReservation.Dialogs
{
    public class BrowseMenuDialog : BaseDialog
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static String DialogId => "BrowseMenuDialog";
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public BrowseMenuDialog()
        {
            var waterfallsteps = new WaterfallStep[]
            {
               HandleIntro,
               HandleEndOfDialog

            };
            AddDialog(new WaterfallDialog(DialogId, waterfallsteps));
            AddDialog(new TextPrompt("textPrompt"));

        }

        private Task<DialogTurnResult> HandleEndOfDialog(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task<DialogTurnResult> HandleIntro(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
