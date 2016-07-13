using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.FormFlow;

namespace WhatchaEarnBot.Activities
{
    public enum BotOptions { Gleich_berechnen, Infos_zum_Bot };

    [Serializable]
    public class BruttoNettoDialog
    {
        [Prompt("Welche {&} möchtest du? {||}")]
        public BotOptions? Option;
        public int AnnualSalary;

        public static IForm<BruttoNettoDialog> BuildForm()
        {
            String _welcomeMessage = "Willkommen bei WhatchaEarn! Wir berechnen für Sie Ihr Brutto-Jahres-Einkommen.";

            OnCompletionAsyncDelegate<BruttoNettoDialog> processOrder = async (context, state) =>
            {
                await context.PostAsync("Wir bearbeiten gerade Ihre Anfrage!");
            };

            return new FormBuilder<BruttoNettoDialog>()
                .Message(_welcomeMessage)
                .Field(nameof(Option))
                .Field(nameof(BruttoNettoDialog.AnnualSalary),
                    validate: async (state, value) =>
                    {
                        var result = new ValidateResult { IsValid = true, Value = value };
                        var salary = (value as Int32?);
                        if (salary.GetType() != typeof(int))
                        {
                            result.Feedback = "Wrong input";
                            result.IsValid = false;
                        }
                        return result;
                    })
                .Confirm("Sind {AnnualSalary} Ihr Brutto-Jahresgehalt?")
                .Message("Ihr Jahres-Netto-Gehalt beträgt: {AnnualSalary} - Netto Abzüge - LOL, YOLO DU SPASST")
                .Build();
        }
    }
}