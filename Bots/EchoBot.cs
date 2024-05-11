// Generated with EchoBot .NET Template version v4.22.0

using Azure.AI.OpenAI;
using Azure;
using iText.Forms.Xfdf;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using DotNetEnv;

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
            string _endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
            string _key = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY");
            string _model = Environment.GetEnvironmentVariable("AZURE_OPENAI_MODEL");

            OpenAIClient client = new OpenAIClient(new Uri(_endpoint), new AzureKeyCredential(_key));
            var chatCompletionOptions = new ChatCompletionsOptions()
            {
                Messages = {
                    new ChatMessage(ChatRole.System, "You are a helpful AI assistant"),
                    new ChatMessage(ChatRole.User, "Does Azure support GPT-4 ?"),
                    new ChatMessage(ChatRole.Assistant, "Yes, it does"),
                    new ChatMessage(ChatRole.User, userQuery)
                },
                MaxTokens = 1000
            };

            Response<ChatCompletions> response = await client.GetChatCompletionsAsync(deploymentOrModelName: _model, chatCompletionOptions);
            var botResponse = response.Value.Choices.First().Message.Content;
            return botResponse.ToString();
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
