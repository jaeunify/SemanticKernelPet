# Semantic Kernel Pet

This project is a simple pet simulation prototype that uses Microsoft Semantic Kernel to converse with an AI pet and generate a rich description of it. It is built as a Blazor Server web application, serving as both the server and the client.  

### Other Language  

- [KR](./README_KR.md)

## About the Project

This application is designed to allow users to interact with an AI pet through free-form conversation. Users can also describe a pet they want to create, and the application will generate a detailed and imaginative description of that pet using a powerful AI model. The conversation and text generation are managed by Semantic Kernel.

## Key Features

-   **AI-Powered Conversations**: Implements natural conversations with an AI pet using Semantic Kernel.
-   **Dynamic Text Generation**: Creates a unique and rich description of a pet based on user input.
-   **Integrated Environment**: Manages both backend AI logic processing and frontend UI rendering within a single project using Blazor Server.

## Tech Stack

-   **Framework**: .NET, Blazor Server
-   **AI Orchestration**: Microsoft Semantic Kernel
-   **AI Models**:
    -   Google Gemini model for text generation (e.g., `gemini-1.5-pro`)

## Getting Started

### Prerequisites

-   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
-   A Google AI Gemini API Key.

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
