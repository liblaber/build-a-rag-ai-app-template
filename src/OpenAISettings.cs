namespace  RagWithSDKs;

/// <summary>
/// Represents the settings for OpenAI.
/// This includes your API key - https://platform.openai.com/api-keys
/// And the models you want to use for chat and image completion -https://platform.openai.com/docs/models
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

    /// <summary>
    /// Gets or sets the image model for OpenAI.
    /// </summary>
    public required string ImageModel { get; set; }
}