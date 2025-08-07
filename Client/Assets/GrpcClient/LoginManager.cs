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


public class LoginManager : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Text StatusText;

    private GrpcAuthService authService;

    private void Start()
    {
        // gRPC 서버 주소 설정 (예: localhost:5000)
        //authService = new GrpcAuthService("b760m.jdj.kr", 7777);

        // 버튼 클릭 이벤트 등록
        LoginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    private async void OnLoginButtonClicked()
    {
        string username = UsernameInput.text;
        string password = PasswordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            StatusText.text = "아이디와 비밀번호를 입력하세요.";
            return;
        }

        StatusText.text = "로그인 중...";

        try
        {
            string url = ConfigManager.Instance.config.grpc_url;
            Debug.Log(url);
            using GrpcChannel channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions() { HttpHandler = new YetAnotherHttpHandler(), DisposeHttpClient = true });

            Auth.AuthClient authClient = new Auth.AuthClient(channel);
            
            LoginReply reply = await authClient.LoginAsync(new LoginRequest { Email = username, Password = password });

            Debug.Log("gRPC 응답: " + reply);
            StatusText.text = $"로그인 성공: {reply.Detail}";
        }
        catch (System.Exception ex)
        {
            Debug.LogError("gRPC 오류: " + ex.Message);
            StatusText.text = $"로그인 실패: {ex.Message ?? "서버 오류"}";
        }
        
        //LoginReply reply = await authService.Login(username, password);

        // if (reply != null && reply.Success)
        // {
        //     StatusText.text = $"로그인 성공: {reply.Detail}";
        //     // TODO: 게임 씬으로 전환하거나 사용자 정보 저장
        // }
        // else
        // {
        //     StatusText.text = $"로그인 실패: {reply?.Detail ?? "서버 오류"}";
        // }
    }
}