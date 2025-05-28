using UnityEngine;
using UnityEngine.EventSystems;

public class TowerGridClickHandler : MonoBehaviour
{
    public Camera mainCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return; // UI 클릭 무시

            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                TowerGrid grid = hit.collider.GetComponent<TowerGrid>();
                if (grid != null)
                {
                    grid.TrySpawnTower();
                }
            }
        }
    }
}