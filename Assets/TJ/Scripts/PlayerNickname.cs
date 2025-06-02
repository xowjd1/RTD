using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNickname : NetworkBehaviour
{
    [Networked]
    public string Nickname { get; set; }

    public Text nicknameText;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Nickname = PlayerInfo.Nickname;
        }
        
        nicknameText.text = Nickname;
    }

    void Update()
    {
        nicknameText.text = Nickname;
    }
}