using UnityEngine;
using UnityEngine.EventSystems;

public class TowerMergeHandler : MonoBehaviour
{
    private Camera cam;
    private TowerObject draggingTower;
    private Vector3 offset;
    private Vector3 originalPos;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            int towerLayer = LayerMask.GetMask("Tower");
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 10f, towerLayer);

            if (hit.collider != null)
            {
                Debug.Log($"🎯 클릭됨: {hit.collider.name}, Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
                TowerObject tower = hit.collider.GetComponent<TowerObject>();
                if (tower != null)
                {
                    draggingTower = tower;
                    originalPos = tower.transform.position;
                    offset = tower.transform.position - (Vector3)mousePos;

                    // 시각적으로 위로 올리기
                    draggingTower.SetSortingOrder(100);
                }
                if (tower == null)
                {
                    Debug.LogWarning("⚠️ TowerObject 컴포넌트 없음");
                }
            }
            else
            {
                Debug.Log("❌ Raycast에 아무것도 안 걸림");
            }
        }

        if (Input.GetMouseButton(0) && draggingTower != null)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            draggingTower.transform.position = mousePos + (Vector2)offset;
        }

        if (Input.GetMouseButtonUp(0) && draggingTower != null)
        {
            draggingTower.OnRelease(originalPos);
            draggingTower.SetSortingOrder(5); // 원래대로
            draggingTower = null;
        }
    }
}
