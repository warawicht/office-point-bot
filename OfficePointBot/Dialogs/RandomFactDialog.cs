using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace OfficePointBot.Dialogs
{
    public class RandomFactDialog
    {
        public static readonly IDialog<string> Dialog = Chain
            
            .PostToChain()
            .Select(m => m.Text)
            .Switch
            (
                Chain.Case
                (
                    new Regex("^tell me a fact"),
                    (context, text) =>

                Chain.Return("Grabbing a fact...")
                .PostToUser()
                .ContinueWith<string, string>(async (ctx, res) =>
                {
                    var response = await res;

                    var fact = await Helpers.GeneralHelper.GetRandomFactAsync();

                    return Chain.Return("**FACT:** *" + fact + "*");

                })

                ),
                Chain.Default<string, IDialog<string>>(

                    (context, text) =>

                        Chain.Return("Say 'tell me a fact'")
                )
            )
            .Unwrap().PostToUser();
             
    }
}