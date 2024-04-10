namespace  RagWithSDKs;

/// <summary>
/// Represents the settings for OpenAI.
/// This includes your API key - https://platform.openai.com/api-keys
/// And the model you want to use for chat completion -https://platform.openai.com/docs/models
/// </summary>
public class OpenAISettings
{
    /// <summary>
    /// Gets or sets the API key for OpenAI.
    /// </summary>
    public required string Key { get; set; }

    /// <summary>
    /// Gets or sets the chat model for OpenAI.
    /// </summary>
    public required string ChatModel { get; set; }
}