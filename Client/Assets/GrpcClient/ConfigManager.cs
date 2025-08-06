using System.IO;
using UnityEngine;
using Newtonsoft.Json;


public class ConfigData
{
    public string grpc_url { get; set; }
    public string server_ip { get; set; }
}

public class ConfigManager : MonoSingleton<ConfigManager>
{
    public ConfigData config;
    
    void Start()
    {
        string configPath = GetConfigPath();

        if (File.Exists(configPath))
        {
            string json = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<ConfigData>(json);
            Debug.Log("gRPC URL: " + config.grpc_url);
            Debug.Log("Server IP: " + config.server_ip);
        }
        else
        {
            Debug.LogWarning("Config file not found. Creating default config.");
            config = new ConfigData
            {
                grpc_url = "https://b760m.jdj.kr:7777",
                server_ip = "127.0.0.1"
            };
            SaveConfig(configPath);
        }
    }

    void SaveConfig(string path)
    {
        string json = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(path, json);
        Debug.Log("Config saved to: " + path);
    }
    
    string GetConfigPath()
    {
        string rootPath;

#if UNITY_STANDALONE_OSX
        // macOS: .app 내부의 Contents 폴더에서 두 단계 위로
        rootPath = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName;
#elif UNITY_STANDALONE_WIN
        // Windows: MyProject_Data 폴더에서 한 단계 위로
        rootPath = Directory.GetParent(Application.dataPath).FullName;
#else
        // 기타 플랫폼: 기본값으로 실행 파일 위치 추정
        rootPath = Directory.GetParent(Application.dataPath).FullName;
#endif

        return Path.Combine(rootPath, "config.json");
    }
}