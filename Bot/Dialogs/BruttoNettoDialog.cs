using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace Bot.Dialogs
{
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
}

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
