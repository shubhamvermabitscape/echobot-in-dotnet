// Generated with EchoBot .NET Template version v4.22.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string userQuery = turnContext.Activity.Text;
            var gptResponse = await GPTResponseAsync(userQuery);
            await turnContext.SendActivityAsync(MessageFactory.Text(gptResponse, gptResponse), cancellationToken);
        }

        private async Task<string> GPTResponseAsync(string userQuery)
        {
            return userQuery;
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
