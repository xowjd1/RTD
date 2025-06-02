using UnityEngine;
using UnityEngine.UI;

public class NicknameDisplay : MonoBehaviour
{
    public Text nicknameText;

    void Start()
    {
        if (!string.IsNullOrEmpty(PlayerInfo.Nickname))
        {
            nicknameText.text = PlayerInfo.Nickname;
        }
        else
        {
            nicknameText.text = "플레이어";
        }
    }
}