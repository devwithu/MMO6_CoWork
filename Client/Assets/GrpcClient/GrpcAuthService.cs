using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Mmorpg2d.Auth;
using UnityEngine;
using Cysharp.Net.Http;


public class GrpcAuthService
{
    //private Channel channel;
    private Auth.AuthClient client;

    public GrpcAuthService(string ip, int port)
    {
        ConnectToServer($"{ip}:{port}");
    }

    private void ConnectToServer(string address)
    {
        //channel = new Channel(address, ChannelCredentials.Insecure);
        using GrpcChannel channel = GrpcChannel.ForAddress("http://b760m.jdj.kr:7777", new GrpcChannelOptions() { HttpHandler = new YetAnotherHttpHandler(), DisposeHttpClient = true });

        client = new Auth.AuthClient(channel);
    }

    public async Task<RegisterReply> Register(string userId, string userPw, string nickname)
    {
        RegisterRequest request = new RegisterRequest
        {
            Email = userId,
            Password = userPw,
        };

        try
        {
            RegisterReply response = await client.RegisterAsync(request);
            if (response.Success)
            {
                Debug.Log($"[System] Registration successful: {response.Detail}");
            }
            else
            {
                Debug.LogError($"[System] Registration failed: {response.Detail}");
            }

            return response;
        }
        catch (RpcException e)
        {
            Debug.LogError($"[System] RPC failed: {e.Status.Detail}");
            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"[System] An error occurred: {e.Message}");
            return null;
        }
    }

    public async Task<LoginReply> Login(string userId, string userPw)
    {
        LoginRequest request = new LoginRequest
        {
            Email = userId,
            Password = userPw
        };

        try
        {
            LoginReply response = await client.LoginAsync(request);
            if (response.Success)
            {
                Debug.Log($"[System] Login successful: {response.Detail}");
            }
            else
            {
                Debug.LogError($"[System] Login failed: {response.Detail}");
            }

            return response;
        }
        catch (RpcException e)
        {
            Debug.LogError($"[System] RPC failed: {e.Status.Detail}");
            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"[System] An error occurred: {e.Message}");
            return null;
        }
    }
}