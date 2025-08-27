# Semantic Kernel Pet

This project is a pet simulation prototype that uses Microsoft Semantic Kernel to converse with an AI pet and calls the Gemini API directly to generate images. It is built as a Blazor Server web application, serving as both the server and the client.

### Other Language  

- [KR](./README_KR.md)

## About the Project

This application is designed for users to interact with and create an AI pet.
-   **Conversation**: Managed via Semantic Kernel, using a Gemini text generation model to produce the pet's responses.
-   **Image Generation**: When a user describes a pet, the application makes a direct HTTP call to the Gemini image generation API to create an image of the pet, which is then displayed to the user.

## Key Features

-   **AI-Powered Conversations**: Implements natural conversations with an AI pet using Semantic Kernel.
-   **Dynamic Image Generation**: Creates a unique pet image based on user descriptions via a direct Gemini API call.
-   **Integrated Environment**: Manages both backend AI logic processing and frontend UI rendering within a single project using Blazor Server.

## Tech Stack

-   **Framework**: .NET, Blazor Server
-   **AI Interaction**:
    -   **Conversation**: Microsoft Semantic Kernel (AI Orchestration)
    -   **Image Generation**: Direct Gemini REST API call using `HttpClient`
-   **AI Models**:
    -   Google Gemini Pro (for text generation)
    -   Google Imagen (for image generation, model name needs to be specified based on API documentation)

## Getting Started

### Prerequisites

-   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
-   A Google AI Gemini API Key (with permissions for both text and image generation).

### Installation and Execution

1.  **Clone the repository**
    ```bash
    git clone https://github.com/your-username/SemanticKernelPet.git
    cd SemanticKernelPet
    ```

2.  **Configure AI Service**
    Add your AI service information to the `appsettings.json` or `appsettings.Development.json` file in the following format.

    ```json
    {
      "Gemini": {
        "ApiKey": "YOUR_GEMINI_API_KEY"
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
