# 시맨틱 커널 펫키우기 시뮬레이션 (Semantic Kernel Pet)

본 프로젝트는 Microsoft Semantic Kernel을 활용하여 AI 캐릭터와 대화하고, 대화 내용에 기반한 이미지를 생성하는 간단한 데이팅 시뮬레이션 프로토타입입니다. Blazor Server 웹 애플리케이션으로 제작되어, 서버와 클라이언트의 역할을 동시에 수행합니다.

### Other Language  

- [ENG](./README.md)

## 프로젝트 소개

이 애플리케이션은 사용자가 AI 펫과 자유롭게 대화, 산책할 수 있도록 설계되었습니다. 대화는 Semantic Kernel을 통해 관리되며, 텍스트 생성 AI 모델(Text-to-Text)이 캐릭터의 응답을 생성합니다.

'산책'이 끝나면, 전체 대화의 맥락을 요약하여 이미지 생성 AI 모델(Text-to-Image)에 전달하고, 그 결과로 두 사람의 '컷씬' 이미지를 생성하여 사용자에게 보여줍니다.

## 주요 기능

-   **AI 기반 대화**: Semantic Kernel을 이용해 자연스러운 AI 캐릭터와의 대화를 구현합니다.
-   **동적 이미지 생성**: 대화의 내용과 분위기를 바탕으로 고유한 '컷씬' 이미지를 생성합니다.
-   **통합 환경**: Blazor Server를 사용하여 백엔드의 AI 로직 처리와 프론트엔드의 UI 렌더링을 하나의 프로젝트에서 관리합니다.

## 기술 스택

-   **프레임워크**: .NET, Blazor Server
-   **AI 오케스트레이션**: Microsoft Semantic Kernel
-   **AI 모델**:
    -   텍스트 생성을 위한 AI 모델 (예: Azure OpenAI의 GPT-4, GPT-3.5-Turbo)
    -   이미지 생성을 위한 AI 모델 (예: Azure OpenAI의 DALL-E 3)

## 시작하기

### 준비물

-   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 이상
-   Azure OpenAI 또는 OpenAI 서비스에 대한 액세스 권한 및 API 키
    -   텍스트 생성 모델 배포
    -   이미지 생성 모델 배포

### 설치 및 실행

1.  **리포지토리 클론**
    ```bash
    git clone https://github.com/your-username/SemanticKernelPet.git
    cd SemanticKernelPet
    ```

2.  **AI 서비스 구성**
    `appsettings.json` 또는 `appsettings.Development.json` 파일에 사용 중인 AI 서비스의 정보를 아래 형식에 맞게 추가합니다.

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

3.  **패키지 복원**
    ```bash
    dotnet restore
    ```

4.  **애플리케이션 실행**
    ```bash
    dotnet run
    ```

5.  브라우저에서 `https://localhost:xxxx` 또는 `http://localhost:xxxx` 로 접속하여 애플리케이션을 확인합니다.

## 라이선스

본 프로젝트는 [LICENSE](LICENSE) 파일에 명시된 조건에 따라 라이선스가 부여됩니다.
