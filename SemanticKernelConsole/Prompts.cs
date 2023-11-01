using Microsoft.SemanticKernel;

namespace SemanticKernelConsole
{
    internal class Prompts
    {
        public async Task Service()
        {
            var builder = new KernelBuilder();

            builder.WithAzureChatCompletionService(
                     "gpt-35-turbo",                  // Azure OpenAI Deployment Name
                     "https://nikum-open-ai-studio.openai.azure.com/", // Azure OpenAI Endpoint
                     "489a2ec503064952ae483d322e5e3f4e");      // Azure OpenAI Key

            //Alternative using OpenAI
            //builder.WithOpenAIChatCompletionService(
            //         "gpt-3.5-turbo",               // OpenAI Model name
            //         "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");     // OpenAI API Key

            var kernel = builder.Build();

            var prompt = @"{{$input}}

            One line TLDR with the fewest words.";

            string translationPrompt = @"{{$input}}

            Translate the text to math.";

            var translator = kernel.CreateSemanticFunction(translationPrompt);

            var summarize = kernel.CreateSemanticFunction(prompt);

            string text1 = @"
            1st Law of Thermodynamics - Energy cannot be created or destroyed.
            2nd Law of Thermodynamics - For a spontaneous process, the entropy of the universe increases.
            3rd Law of Thermodynamics - A perfect crystal at zero Kelvin has zero entropy.";

            //Console.WriteLine(await summarize.InvokeAsync(text1));
            //Console.WriteLine(await summarize.InvokeAsync(text2));

            // Run two prompts in sequence (prompt chaining)
            Console.WriteLine(await kernel.RunAsync(text1, translator, summarize));
        }

    }
}
