using System;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using TMPro;


public class NetworkManager : MonoBehaviour,INetworkRunnerCallbacks
{
    
    public static NetworkManager Instance { get; private set; }
    private NetworkRunner runner;

    public NetworkObject playerNicknamePrefab;
    public TMP_Text player1Text;
    public TMP_Text player2Text;

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
            PlayerInfo.LocalPlayerRef = runner.LocalPlayer;

            runner.AddCallbacks(this); // ✅ 콜백 등록

            // Host라면, 기존 플레이어들도 스폰해주기
            if (runner.IsSharedModeMasterClient)
            {
                foreach (var player in runner.ActivePlayers)
                {
                    SpawnPlayerNickname(player);
                }
            }
        }
        else
        {
            Debug.LogError($"❌ 매칭 실패: {result.ShutdownReason}");
        }
    }

    private void SpawnPlayerNickname(PlayerRef playerRef)
    {
        runner.Spawn(playerNicknamePrefab, Vector3.zero, Quaternion.identity, playerRef);
    }


    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"플레이어 접속됨: {player}");

        // Host(SharedMode Master)만 스폰
        if (runner.IsSharedModeMasterClient)
        {
            SpawnPlayerNickname(player);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("씬 로드 완료됨");

        var nicknames = FindObjectsOfType<PlayerNickname>();

        foreach (var nickname in nicknames)
        {
            nickname.SetupUI(player1Text, player2Text);
        }
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }
}
