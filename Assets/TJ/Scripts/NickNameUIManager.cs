using UnityEngine;
using UnityEngine.UI;

public class NicknameUIManager : MonoBehaviour
{
    [Header("Nickname 입력 UI")]
    public GameObject nicknamePanel;
    public InputField nicknameInputField;
    public Button confirmButton;

    [Header("매칭 UI")]
    public GameObject matchingPanel;

    void Start()
    {
        nicknamePanel.SetActive(false);
        matchingPanel.SetActive(false);

        confirmButton.onClick.AddListener(OnConfirmNickname);
    }

    public void OpenNicknamePanel()
    {
        nicknamePanel.SetActive(true);
    }

    private void OnConfirmNickname()
    {
        string inputName = nicknameInputField.text.Trim();

        if (string.IsNullOrEmpty(inputName))
        {
            Debug.LogWarning("⚠️ 닉네임을 입력하세요.");
            return;
        }

        PlayerInfo.Nickname = inputName;

        Debug.Log($"✅ 닉네임 저장됨: {PlayerInfo.Nickname}");
        
        nicknamePanel.SetActive(false);
        matchingPanel.SetActive(true);
    }
}