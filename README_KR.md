# 시맨틱 커널 펫키우기 시뮬레이션 (Semantic Kernel Pet)

본 프로젝트는 Microsoft Semantic Kernel을 활용하여 AI 펫과 대화하고, AI가 생성해주는 풍부한 설명을 통해 새로운 펫을 만나는 시뮬레이션 프로토타입입니다. Blazor Server 웹 애플리케이션으로 제작되어, 서버와 클라이언트의 역할을 동시에 수행합니다.

### Other Language  

- [ENG](./README.md)

## 프로젝트 소개

이 애플리케이션은 사용자가 AI 펫과 자유롭게 대화할 수 있도록 설계되었습니다. 또한, 사용자가 원하는 펫을 묘사하면 강력한 AI 모델이 그에 대한 상세하고 상상력 풍부한 설명을 생성해줍니다. 대화와 텍스트 생성은 모두 Semantic Kernel을 통해 관리됩니다.

## 주요 기능

-   **AI 기반 대화**: Semantic Kernel을 이용해 자연스러운 AI 펫과의 대화를 구현합니다.
-   **동적 텍스트 생성**: 사용자 입력을 바탕으로 고유하고 풍부한 펫 관련 설명을 생성합니다.
-   **통합 환경**: Blazor Server를 사용하여 백엔드의 AI 로직 처리와 프론트엔드의 UI 렌더링을 하나의 프로젝트에서 관리합니다.

## 기술 스택

-   **프레임워크**: .NET, Blazor Server
-   **AI 오케스트레이션**: Microsoft Semantic Kernel
-   **AI 모델**:
    -   텍스트 생성을 위한 Google Gemini 모델 (예: `gemini-1.5-pro`)

## 시작하기

### 준비물

-   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 이상
-   Google AI Gemini API 키

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
      "Gemini": {
        "ApiKey": "YOUR_GEMINI_API_KEY"
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
