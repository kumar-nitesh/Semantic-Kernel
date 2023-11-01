using Azure;
using Azure.AI.OpenAI;

string endPoint = "https://nikum-open-ai-studio.openai.azure.com/";
string key = "489a2ec503064952ae483d322e5e3f4e";
string deploymentName = "gpt-35-turbo";

OpenAIClient client = new OpenAIClient(new Uri(endPoint), new AzureKeyCredential(key));

string prompt = "What is Azure OpenAI?";
Console.WriteLine("Using Completion");
Console.WriteLine("------------------------------------------------------------------------------------");
Response<Completions> response = client.GetCompletions(deploymentName, prompt);
string completion = response.Value.Choices[0].Text;
Console.WriteLine($"Chatbot: {completion}");
Console.WriteLine("------------------------------------------------------------------------------------");
Console.WriteLine("Using Chat Completion");

ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions
{
    Messages =
    {
        new ChatMessage(ChatRole.System, "You are a helpful AI bot."),
        new ChatMessage(ChatRole.User, "What is Azure OpenAI?")
    }
};

ChatCompletions chatCompletionsResponse = client.GetChatCompletions(deploymentName, chatCompletionsOptions);
ChatMessage chatCompletion = chatCompletionsResponse.Choices[0].Message;
Console.WriteLine($"Chatbot: {chatCompletion.Content}");