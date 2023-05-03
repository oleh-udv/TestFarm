using BuilderGame.Gameplay.SeedFields;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BuilderGame.Editors
{
    [CustomEditor(typeof(SeedField))]
    public class SeedFieldEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var seedField = (SeedField)target;

            GUI.backgroundColor = Color.red;
            EditorGUILayout.HelpBox("Press when layers are filled to autofill lists", MessageType.Warning);
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Initialize", GUILayout.Height(50)))
            {
                seedField.FillLists();
            }
            GUI.backgroundColor = Color.gray;
        }
    }
}
