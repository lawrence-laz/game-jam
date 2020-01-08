using UnityEngine;

public class HiddenItemBehaviour : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Color colour = Color.white;

        UnityEditor.Handles.BeginGUI();
        GUI.color = colour;
        var view = UnityEditor.SceneView.currentDrawingSceneView;
        Vector3 screenPos = view.camera.WorldToScreenPoint(transform.position);

        if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
        {
            UnityEditor.Handles.EndGUI();
            return;
        }

        Vector2 size = GUI.skin.label.CalcSize(new GUIContent("Hidden"));
        GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), "Hidden");
        UnityEditor.Handles.EndGUI();
    }
}
