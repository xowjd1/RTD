using TMPro;
using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    public GameObject NickNameUI;
    public GameObject MatchingUI;
    public GameObject MakingRoomUI;
    public GameObject JoinRoomUI;
    public TMP_InputField nicknameInputField;
    public void Start()
    {
        NickNameUI.SetActive(false);
        MatchingUI.SetActive(false);
        MakingRoomUI.SetActive(false);
        JoinRoomUI.SetActive(false);
        
    }
    
    public void OnClickCoopBtn()
    {
        NickNameUI.SetActive(true);
    }

    public void OnClickNickNameConfirmBtn()
    {
        string inputName = nicknameInputField.text.Trim();

        if (string.IsNullOrEmpty(inputName))
        {
            Debug.LogWarning("⚠️ 닉네임을 입력하세요.");
            return;
        }

        PlayerInfo.Nickname = inputName;

        Debug.Log($"✅ 닉네임 저장됨: {PlayerInfo.Nickname}");
        
        NickNameUI.SetActive(false);
        MatchingUI.SetActive(true);
    }
}
