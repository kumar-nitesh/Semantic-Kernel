using Azure;
using Azure.AI.OpenAI;


namespace SemanticKernelConsole
{
    internal class Chats
    {
        readonly string endpoint = "https://aoais-guardian-apim.azure-api.net";
        readonly string key = "28be25d33b584743a04acf91e4352b11";

        public void WithoutResponseStreaming()
        {
            OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatMessage(ChatRole.System, "You are a helpful assistant."),
                    new ChatMessage(ChatRole.User, "Do you know about Azure Serverless computing?")
                },
                MaxTokens = 100
            };

            Response<ChatCompletions> response = client.GetChatCompletions(deploymentOrModelName: "lvovan-chatgpt-deployment", chatCompletionsOptions);

            Console.WriteLine(response.Value.Choices[0].Message.Content);

            Console.WriteLine();
        }

        public async Task WithResponseStreaming()
        {
            OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatMessage(ChatRole.System, "You are a helpful assistant."),
                    new ChatMessage(ChatRole.User, "Does Azure OpenAI support customer managed keys?"),
                    new ChatMessage(ChatRole.Assistant, "Yes, customer managed keys are supported by Azure OpenAI."),
                    new ChatMessage(ChatRole.User, "Do other Azure AI services support this too?"),
                },
                MaxTokens = 100
            };

            Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(deploymentOrModelName: "gpt-35-turbo", chatCompletionsOptions);
            using StreamingChatCompletions streamingChatCompletions = response.Value;

            await foreach (StreamingChatChoice choice in streamingChatCompletions.GetChoicesStreaming())
            {
                await foreach (ChatMessage message in choice.GetMessageStreaming())
                {
                    Console.Write(message.Content);
                }
                Console.WriteLine();
            }
        }

    }
}
