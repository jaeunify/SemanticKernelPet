# Semantic Kernel Dating Sim

This project is a simple dating simulation prototype that uses Microsoft Semantic Kernel to converse with an AI character and generate an image based on the conversation. It is built as a Blazor Server web application, serving as both the server and the client.  

### Other Language  

- [KR](./README_KR.md)

## About the Project

This application is designed to allow users to experience a 'date' by having a free-form conversation with an AI character. The conversation is managed by Semantic Kernel, and a Text-to-Text AI model generates the character's responses.

After the 'date' concludes, the context of the entire conversation is summarized and passed to a Text-to-Image AI model. This process generates a 'cutscene' image of the two, which is then displayed to the user.

## Key Features

-   **AI-Powered Conversations**: Implements natural conversations with an AI character using Semantic Kernel.
-   **Dynamic Image Generation**: Creates a unique 'cutscene' image based on the content and mood of the conversation.
-   **Integrated Environment**: Manages both backend AI logic processing and frontend UI rendering within a single project using Blazor Server.

## Tech Stack

-   **Framework**: .NET, Blazor Server
-   **AI Orchestration**: Microsoft Semantic Kernel
-   **AI Models**:
    -   An AI model for text generation (e.g., GPT-4, GPT-3.5-Turbo from Azure OpenAI)
    -   An AI model for image generation (e.g., DALL-E 3 from Azure OpenAI)

## Getting Started

### Prerequisites

-   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
-   Access to Azure OpenAI or OpenAI services and an API Key
    -   A deployed model for text generation
    -   A deployed model for image generation

### Installation and Execution

1.  **Clone the repository**
    ```bash
    git clone https://github.com/your-username/SemanticKernelDatingSim.git
    cd SemanticKernelDatingSim
    ```

2.  **Configure AI Service**
    Add your AI service information to the `appsettings.json` or `appsettings.Development.json` file in the following format.

    ```json
    {
      "AzureOpenAI": {
        "Endpoint": "YOUR_AZURE_OPENAI_ENDPOINT",
        "ApiKey": "YOUR_AZURE_OPENAI_API_KEY",
        "TextCompletionDeploymentName": "YOUR_TEXT_MODEL_DEPLOYMENT_NAME",
        "ImageGenerationDeploymentName": "YOUR_IMAGE_MODEL_DEPLOYMENT_NAME"
      }
    }
    ```

3.  **Restore packages**
    ```bash
    dotnet restore
    ```

4.  **Run the application**
    ```bash
    dotnet run
    ```

5.  Open your browser and navigate to `https://localhost:xxxx` or `http://localhost:xxxx` to see the application.

## License

This project is licensed under the terms of the [LICENSE](LICENSE) file.
