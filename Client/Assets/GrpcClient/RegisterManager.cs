using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Mmorpg2d.Auth;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Mmorpg2d.Auth;
using UnityEngine;
using Cysharp.Net.Http;

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
        //authService = new GrpcAuthService("b760m.jdj.kr", 7777);

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

        if (password != nickname)
        {
            RegisterStatusText.text = "비밀번호 입력을 확인하세요.";
            return;
        }
        
        RegisterStatusText.text = "회원가입 요청 중...";

        try
        {
            string url = ConfigManager.Instance.config.grpc_url;
            Debug.Log(url);
            using GrpcChannel channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions() { HttpHandler = new YetAnotherHttpHandler(), DisposeHttpClient = true });

            Auth.AuthClient authClient = new Auth.AuthClient(channel);

            RegisterReply reply = await authClient.RegisterAsync(new RegisterRequest { Email = username, Password = password});

            Debug.Log("gRPC 응답: " + reply.Detail);
            RegisterStatusText.text = $"로그인 성공: {reply.Detail}";
        }
        catch (System.Exception ex)
        {
            Debug.LogError("gRPC 오류: " + ex.Message);
            RegisterStatusText.text = $"로그인 실패: {ex.Message ?? "서버 오류"}";
        }
        
        
        // RegisterReply reply = await authService.Register(username, password, nickname);
        //
        // if (reply != null && reply.Success)
        // {
        //     RegisterStatusText.text = $"회원가입 성공: {reply.Detail}";
        //     // TODO: 로그인 화면으로 전환하거나 자동 로그인 처리
        // }
        // else
        // {
        //     RegisterStatusText.text = $"회원가입 실패: {reply?.Detail ?? "서버 오류"}";
        // }
    }
}