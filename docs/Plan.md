# Blazor UI 개편 계획 (Plan.md)

`Spec.md`의 요구사항에 따라 현재 Blazor 프론트엔드를 개편하기 위한 실행 계획입니다.

## 1. 기본 구조 및 레이어 설정

- **`FullScreenLayout.razor` 컴포넌트 신규 생성**: 3개의 UI 레이어를 관리할 메인 레이아웃입니다. CSS의 `position`과 `z-index`를 사용하여 레이어를 겹겹이 쌓는 구조로 구현합니다.
- **기존 레이아웃 교체**: `App.razor`에서 기존 `MainLayout` 대신 `FullScreenLayout`을 사용하도록 변경합니다.

## 2. 레이어별 컴포넌트 개발

### Layer 1: 펫 이미지 표시 (`PetImageView.razor`)

- 화면 우측 상단에 위치할 컴포넌트입니다.
- 부모 컴포넌트로부터 선택된 펫의 `base64` 이미지 URL을 파라미터로 전달받습니다.
- 이미지 URL이 없으면, 명세에 따라 검은색 배경을 표시합니다.

### Layer 2: 중간 배경

- `FullScreenLayout.razor`의 중간 레이어 역할을 하는 `div`에 `background4.png`를 CSS 배경으로 지정합니다.
- 이미지 자체에 우측 상단이 투명하게 처리되어 있으므로, 별도 로직 없이 Layer 1의 펫 이미지가 보이게 됩니다.

### Layer 3: 상호작용 UI

가장 상위 레이어로, 실제 사용자와의 상호작용이 일어나는 부분입니다. 이 레이어는 다시 여러 개의 하위 컴포넌트로 분리하여 개발합니다.

- **`MainUI.razor`**:
    - Layer 3의 전체적인 컨테이너 역할을 하며, `background5.png`를 배경으로 가집니다.
    - 아래에 기술된 좌측 사이드바와 우측 컨텐츠 영역으로 구성됩니다.

- **`SideNav.razor` (좌측 사이드바)**:
    - **홈 버튼**: 최상단에 위치합니다.
    - **펫 목록 (`PetList.razor`)**: 홈 버튼 아래에 위치하며, `PetStorageService`와 연동하여 현재 보유 중인 펫 리스트를 세로로 표시합니다.

- **`MainContent.razor` (우측 컨텐츠 영역)**:
    - **아이템 바 (`ItemBar.razor`)**:
        - `ItemStorageService`와 연동하여 아이템 목록을 가로로 나열합니다.
        - 아이템 개수가 많아지면 `overflow-x: auto` CSS 속성을 이용해 가로 스크롤을 구현합니다.
    - **채팅 내역 (`ChatHistory.razor`)**:
        - `PetChatService`와 연동하여 채팅 기록을 리스트 형태로 표시합니다.
        - `overflow-y: auto`를 이용해 세로 스크롤을 구현하고, 항상 마지막 대화가 보이도록 설정합니다.
        - CSS 클래스를 동적으로 바인딩하여 사용자 채팅은 노란색(`.user-chat`), 펫 채팅은 분홍색(`.pet-chat`) 배경을 적용합니다.
    - **채팅 입력란 (`ChatInput.razor`)**:
        - 텍스트 입력 `input`과 `Enter` 버튼으로 구성됩니다.
        - 사용자가 메시지를 입력하고 전송하면, 부모 컴포넌트로 메시지 내용을 전달하는 이벤트를 발생시킵니다.

## 3. 컴포넌트 통합 및 상태 관리

- `FullScreenLayout.razor` 내부에 위에서 개발한 각 레이어와 UI 컴포넌트들을 배치합니다.
- `PetStorageService`를 중앙 상태 관리자로 활용하여, `PetList`에서 펫을 선택하면 해당 펫의 정보(이미지, 채팅 내역 등)가 `PetImageView`와 `ChatHistory`에 반영되도록 파라미터와 이벤트 콜백으로 연결합니다.

## 4. CSS 스타일링

- 각 컴포넌트별로 CSS Isolation (`.razor.css`)을 사용하여 독립적인 스타일을 적용합니다.
- `Plan.md`에 명시된 레이아웃, 색상, 폰트, 스크롤 방식 등을 CSS로 구현합니다.

## 5. 기존 컴포넌트 정리

- 개편이 완료되면, 더 이상 사용되지 않는 기존 페이지 및 컴포넌트(`Home.razor`, `Pet.razor`, `NavMenu.razor` 등)는 혼동을 방지하기 위해 프로젝트에서 제거하거나 보관 처리합니다.
