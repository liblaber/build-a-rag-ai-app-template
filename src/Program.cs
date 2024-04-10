// This file contains the main program logic for the AI assistant application.
// The application uses the Semantic Kernel and OpenAI chat completion to interact with users.
// It creates a chat history, sends user input to the AI, and prints the AI's response.
//
// To use this app, you need to provide your OpenAI API key and chat model in the appsettings.json file.
// When running the app, you will see a 'User >' prompt. Enter your message and press Enter to start the conversation.
// To end the conversation, press Enter without typing anything.

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

using RagWithSDKs;

// Load the API key and selected model from the appsettings.json file
var appSettingsReader = new AppSettingsReader();
var openAISettings = appSettingsReader.ReadSection<OpenAISettings>("OpenAI");

// Create the kernel builder with OpenAI chat completion
var builder = Kernel.CreateBuilder().AddOpenAIChatCompletion(openAISettings.ChatModel, openAISettings.Key);

// Add plugins to the kernel builder

// Build the kernel
Kernel kernel = builder.Build();

// Create a chat history object, containing a system prompt to guide the LLM
// The history is passed to the LLM every time to keep track of the conversation. All new prmopts from the user
// are added to the history first, then the entire history is passed to the LLM to get the response.
// If the history size is too large then earlier entries wil be removed to keep within the token limit.
var history = new ChatHistory();
history.AddSystemMessage("You are Libby the liblab llama, a helpful chatbot that can use RAG from liblab generated SDKs to help answer questions using.");

// Get the chat completion service that will be used to interact with the AI
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Start the conversation by giving the user a prompt
Console.WriteLine("I am an AI assistant who also knows a load of cat facts!");
Console.Write("User > ");
string userInput;

// Get user input from the console and keep the conversation going until the user enters a blank line
while (!string.IsNullOrWhiteSpace(userInput = Console.ReadLine()))
{
    // Add the user input to the history
    history.AddUserMessage(userInput);

    // Enable auto function calling so that kernal functions from plugins are
    // automatically called when the LLM decides to do so
    OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
    {
        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
    };

    // Send the chat history and get the response from the AI
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // Print the response from the AI
    Console.WriteLine("Assistant > " + result);

    // Add the response message from the agent to the chat history
    history.AddMessage(result.Role, result.Content ?? string.Empty);

    // Ask for user input again
    Console.Write("User > ");
}