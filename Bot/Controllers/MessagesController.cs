using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Bot.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            return await Conversation.SendAsync(message, () => new BruttoNettoDialog());
            context.Wait(MessageReceivedAsync);
        }

        /*internal static IDialog<BruttoNettoDialog> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(BruttoNettoDialog.BuildForm));
        }*/

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}