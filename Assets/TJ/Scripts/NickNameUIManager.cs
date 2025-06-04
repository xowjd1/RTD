using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

public class NicknameUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text player1Text;
    [SerializeField] private TMP_Text player2Text;

    private Dictionary<PlayerRef, TMP_Text> playerTexts = new Dictionary<PlayerRef, TMP_Text>();

    private void Start()
    {
        UpdateNicknameUI();
    }

    public void UpdateNicknameUI()
    {
        var nicknames = FindObjectsOfType<PlayerNickname>();

        foreach (var nickname in nicknames)
        {
            if (nickname.OwnerRef == PlayerInfo.LocalPlayerRef)
            {
                player1Text.text = nickname.Nickname;
                playerTexts[nickname.OwnerRef] = player1Text;
            }
            else
            {
                player2Text.text = nickname.Nickname;
                playerTexts[nickname.OwnerRef] = player2Text;
            }
        }
    }
}