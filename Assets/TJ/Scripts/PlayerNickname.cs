using Fusion;
using TMPro;
using UnityEngine;


public class PlayerNickname : NetworkBehaviour
{
    [Networked] public string Nickname { get; set; }
    [Networked] public PlayerRef OwnerRef { get; set; }

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Nickname = PlayerInfo.Nickname;
            OwnerRef = PlayerInfo.LocalPlayerRef;
        }
    }
}