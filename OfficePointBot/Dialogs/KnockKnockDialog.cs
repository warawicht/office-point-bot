using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace OfficePointBot.Dialogs
{
    public class KnockKnockDialog
    {
        public static readonly IDialog<string> Dialog = Chain

                .PostToChain()
                .Select(m => m.Text)
                .Switch
                (
                    Chain.Case
                    (
                        new Regex("^tell me a knock knock joke"),
                        (context, text) =>

                            Chain.Return("Knock, knock.")
                            .PostToUser()
                            .WaitToBot()
                            .Select(ignoreUser => "Interrupting Cow")
                            .PostToUser()
                            .Do(async (ctx, res) =>
                            {
                                await Task.Delay(2000);
                            })
                            .Select(ignoreUser => "MOOOOO!")
                              
                    ),
                    Chain.Default<string, IDialog<string>>(

                        (context, text) =>

                            Chain.Return("Say 'tell me a knock knock joke'")
                    )
                )
                .Unwrap().PostToUser();
    }
}