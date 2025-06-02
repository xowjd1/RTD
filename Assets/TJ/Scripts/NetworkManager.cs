using UnityEngine;
using Fusion;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    private NetworkRunner runner;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    public async void StartCoopMode()
    {
        if (runner == null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }

        runner.ProvideInput = true;

        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "LRWD_CoopSession",
            Scene = SceneRef.FromIndex(2),
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            PlayerCount = 2
        });

        if (result.Ok)
        {
            Debug.Log("✅ 매칭 성공");
        }
        else
        {
            Debug.LogError($"❌ 매칭 실패: {result.ShutdownReason}");
        }
    }

}

