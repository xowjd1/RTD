using UnityEngine;

public class ModeSelectUI : MonoBehaviour
{
    public void OnClickCoopMode()
    {
        NetworkManager.Instance.StartCoopMode();
    }
}
