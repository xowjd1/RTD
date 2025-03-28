using UnityEditor;
using UnityEngine;


//waypoint를 Scene 뷰에서 시각적으로 편집할 수 있도록 커스텀 에디터를 구현
[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
    // target은 Editor 클래스에서 제공하는 프로퍼티
    // 해당 오브젝트를 WayPoint 타입으로 형변환(cast)하는 것을 의미
    // 코드 내에서 간편하게 현재 편집 중인 WayPoint 오브젝트에 접근
    WayPoint Waypoint => target as WayPoint;

    
    // Scene 뷰에서 커스텀 그리기와 상호작용을 처리하는 함수
    // 여기서 waypoint를 시각적으로 나타내고, 마우스를 이용해 waypoint의 위치를 변경할 수 있도록 Handles 기능을 사용함.
    private void OnSceneGUI()
    {
        Handles.color = Color.red;

        for (int i = 0; i < Waypoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            
            Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
            
            // waypoint를 Scene 뷰에서 마우스로 드래그해서 이동할 수 있도록 하는 핸들
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, 0.7f,new Vector3(0.3f,0.3f,0.3f), Handles.SphereHandleCap);
            
            // 핸들 순서를 알기 위해 텍스트 입력
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 20;
            textStyle.normal.textColor = Color.yellow;
            Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(currentWaypointPoint + textAlligment, $"{i + 1}", textStyle);
            
            EditorGUI.EndChangeCheck();

            //EditorGUI.BeginChangeCheck() 와 EditorGUI.EndChangeCheck()
            //핸들을 움직였을 때 웨이포인트 위치가 변경되었는지 감지하는 기능
            
            if (EditorGUI.EndChangeCheck())
            {
                // 핸들을 이동시키면 Undo.RecordObject() 가 호출되어 변경 사항이 기록되고, Waypoint.Points[i]에 새로운 위치가 반영
                Undo.RecordObject(target, "Change WayPoint");
                Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPosition;
            }
        }
        
    }

}
