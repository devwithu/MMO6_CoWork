using Grpc.Core;
using GrpcService;
using Mmorpg2d.Auth;

namespace GrpcService.Services
{
    public class AuthService : Auth.AuthBase
    {

        public override async Task<RegisterReply> Register(RegisterRequest request, ServerCallContext context)
        {
            //return base.Register(request, context);
            return new RegisterReply
            {
                Success = true,
                Detail = "ȸ������ �����Դϴ�."

            };
        }

        public override async Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
        {
            //return base.Login(request, context);
            return new LoginReply
            {
                Success = true,
                Detail = "ȸ������ �����Դϴ�."

            };
        }
    }
}
