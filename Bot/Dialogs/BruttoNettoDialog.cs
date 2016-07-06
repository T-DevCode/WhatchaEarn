using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Bot.Dialogs
{
    [Serializable]
    public class BruttoNettoDialog : IDialog<object>
    {
        private String _respond = "";
        private int _annualSalary = 0;
        private int _taxClass = 0;
        private double _taxes = 0.00;
        private double _insurance = 0.00;
        private bool _showHello = true;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(ReceiveMessageAsync);
        }

        private async void SayHello(IDialogContext context)
        {
            if (_showHello)
            {
                _respond = "Wilkommen zu WhatchaEarn";
                PostAndWait(context, _respond);
                _showHello = false;
            }
        }

        public async Task ReceiveMessageAsync(IDialogContext context, IAwaitable<Message> argument)
        {
            /* _respond = "Wie können wir dir helfen?"
                            + "1. Brutto Netto berechnen\n"
                            + "2. Informationen zum Bot\n"; */
            //SayHello(context);

            _respond = "Bitte geben Sie Ihr Jahres-Brutto-Gehalt an";
            PostAndWait(context, _respond);

            var message = await argument;

            if (message.Text.Length > 0)
            {
                if (Int32.TryParse(message.Text, out _annualSalary))
                {
                    _taxClass = 0;
                    _annualSalary = Convert.ToInt32(message.Text);

                    if (_annualSalary <= 11000) _taxClass = 0;
                    else if (_annualSalary > 11000 && _annualSalary <= 18000) _taxClass = 1;
                    else if (_annualSalary > 18000 && _annualSalary <= 31000) _taxClass = 2;
                    else if (_annualSalary > 31000 && _annualSalary <= 60000) _taxClass = 3;
                    else if (_annualSalary > 60000 && _annualSalary <= 90000) _taxClass = 4;
                    else if (_annualSalary > 90000 && _annualSalary <= 999999) _taxClass = 5;
                    else if (_annualSalary > 1000000) _taxClass = 6;

                    _taxes = CalculateTax(_annualSalary, _taxClass);
                    _insurance = 0;

                    _respond = String.Format("Ihr Monats-Netto-Gehalt beträgt: {0}€", ((_annualSalary - _taxes - _insurance)/14));
                    PostAndWait(context, _respond);
                }
            }
            else
            {
                _respond = "Bitte geben Sie eine Zahl ein. (25000)";
                PostAndWait(context, _respond);
            }
            //PostAndWait(context, _respond);
            //PostAndWait(context, respond);
        }

        private double CalculateTax(int salary, int taxclass)
        {
            double value = 0;

            switch (taxclass)
            {
                case 0:
                    value = salary;
                    break;
                case 1:
                    value = ((salary - 11000) * 1750) / 7000;
                    break;
                case 2:
                    value = (((salary - 18000) * 4550) / 13000) + 1750;
                    break;
                case 3:
                    value = (((salary - 31000) * 12180) / 29000) + 6300;
                    break;
                case 4:
                    value = (((salary - 60000) * 14400) / 30000) + 18480;
                    break;
                case 5:
                    value = (((salary - 90000) * 455000) / 910000) + 32880;
                    break;
                case 6:
                    value = ((salary - 999999) * 0.55) + 487880;
                    break;
            }

            return value;
        }

        private async void PostAndWait(IDialogContext context, string respond)
        {
            try
            {
                await context.PostAsync(_respond);
                context.Wait(ReceiveMessageAsync);
            }
            catch(AggregateException e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}

/*
public enum Information
{
    Informationen_zu_dem_Bot = 1,
    Brutto_Netto_berechnen
};

[Serializable]
public class BruttoNettoDialog
{
    //Display prompt message before allowing the user to select one of the choices
    [Prompt("Wie können wir Ihnen helfen? {||}")]
    public Information Info;

    public double annualSalary;

    public static IForm<BruttoNettoDialog> BuildForm()
    {
        //Return message
        String responseMessage = "";

        return new FormBuilder<BruttoNettoDialog>()
            .Message("Willkommen bei WhatchaEarn!")
            .Field(nameof(Info))
            .Field(nameof(BruttoNettoDialog.annualSalary), state => {


                return false;
            })
            .Build();
    }

    private bool Calculate()
    {
        throw new NotImplementedException();
    }
}
*/



/*
    Pattern Element 	            Description
    {<format>}                      Value of the current field.
    {&} 	                        Description of the current field.
    {<field><format>} 	            Value of a particular field.
    {&<field>} 	                    Description of a particular field.
    {||} 	                        Show the current choices which can be the current value, no preference or the possible values for enumerated fields.
    {[{<field><format>} ...]} 	    Create a list with all field values together utilizing Separator and LastSeparator to separate the individual values.
    {*} 	                        Show one line for each active field with the description and current value.
    {*filled} 	                    Show one line for each active field that has an actual value with the description and current value.
    {<nth><format>} 	            A regular C# format specifier that refers to the nth arg. See TemplateUsage to see what args are available.
    {?<textOrPatternElement>...} 	Conditional substitution. If all referred to pattern elements have values, the values are substituted and the whole expression is used. 
*/

/*
                .Field(new FieldReflector<BruttoNettoDialog>(nameof(annualSalary))
                    .SetType(null)
                    .SetActive((state) => state.Info == Information.Brutto_Netto_berechnen)
                    .SetDefine((state, response) =>
                    {
                        var result = new ValidateResult { IsValid = true, Value = response };
                        var salary = response.Values;
                        if (salary == typeof(double))
                        {
                            result.Feedback = " Ihr Brutto Jahresgehalt: GEH SCHEISSEN";
                            result.IsValid = true;
                        }
                        else
                        {
                            result.Feedback = "Bitte geben Sie eine Zahl ein! (24000.13)";
                            result.IsValid = false;
                        }
                        return null;
                    }))
*/
