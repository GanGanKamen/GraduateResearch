using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VFXController))]
public class VFXControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VFXController controller = (VFXController)target;
        if (GUILayout.Button("Simulate"))
        {
            controller.RunSimulation();
        }
    }
}