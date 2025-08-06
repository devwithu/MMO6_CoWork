using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Mmorpg2d.Auth;

public class RegisterManager : MonoBehaviour
{
    public InputField RegisterUsernameInput;
    public InputField RegisterPasswordInput;
    public InputField RegisterNicknameInput;
    public Button RegisterButton;
    public Text RegisterStatusText;

    private GrpcAuthService authService;

    private void Start()
    {
        // gRPC 서버 주소 설정
        authService = new GrpcAuthService("b760m.jdj.kr", 7777);

        // 버튼 클릭 이벤트 등록
        RegisterButton.onClick.AddListener(OnRegisterButtonClicked);
    }

    private async void OnRegisterButtonClicked()
    {
        string username = RegisterUsernameInput.text;
        string password = RegisterPasswordInput.text;
        string nickname = RegisterNicknameInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nickname))
        {
            RegisterStatusText.text = "모든 항목을 입력하세요.";
            return;
        }

        RegisterStatusText.text = "회원가입 요청 중...";

        RegisterReply reply = await authService.Register(username, password, nickname);

        if (reply != null && reply.Success)
        {
            RegisterStatusText.text = $"회원가입 성공: {reply.Detail}";
            // TODO: 로그인 화면으로 전환하거나 자동 로그인 처리
        }
        else
        {
            RegisterStatusText.text = $"회원가입 실패: {reply?.Detail ?? "서버 오류"}";
        }
    }
}