using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Interface
{
    public class VibrationEditWindow : EditorWindow
    {
        string defultName = "New VibrationEffect";
        AnimationCurve leftCurveField = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        AnimationCurve rightCurveField = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        [CustomEditor(typeof(VibrationEffect))]
        [MenuItem("GanGanKamen/Vibration/Vibration Effect")]
        static void ShowWindow()
        {
            EditorWindow.GetWindow<VibrationEditWindow>("VibrationEffect");
        }

        void OnGUI()
        {
            GUILayout.Label("Setting", EditorStyles.boldLabel);
            EditorGUILayout.TextField("Name", defultName);            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("LeftMoter");
            EditorGUILayout.CurveField(leftCurveField);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("RightMoter");
            EditorGUILayout.CurveField(rightCurveField);

        }
    }
}


