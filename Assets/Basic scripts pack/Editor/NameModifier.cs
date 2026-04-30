#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Basic.Editor
{
    public class NameModifier : EditorWindow
    {
        private string _prefix = "";
        private string _suffix = "";
        private string _removeText = "";
        private bool _previewMode = true;

        [MenuItem("Tools/Game Objects/Name Modifier")]
        private static void ShowWindow()
        {
            GetWindow<NameModifier>("Name Modifier");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Modify Object Names", EditorStyles.boldLabel);

            // Add section
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Add Text", EditorStyles.boldLabel);
            _prefix = EditorGUILayout.TextField("Add Before (Prefix)", _prefix);
            _suffix = EditorGUILayout.TextField("Add After (Suffix)", _suffix);

            // Remove section
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Remove Text", EditorStyles.boldLabel);
            _removeText = EditorGUILayout.TextField("Text to Remove", _removeText);

            EditorGUILayout.Space();
            _previewMode = EditorGUILayout.Toggle("Preview Changes", _previewMode);

            // Preview section
            if (Selection.gameObjects.Length > 0 && _previewMode)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Preview:", EditorStyles.boldLabel);
                foreach (GameObject obj in Selection.gameObjects)
                {
                    string newName = GetModifiedName(obj.name);
                    EditorGUILayout.LabelField($"{obj.name} → {newName}");
                }
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Apply Changes"))
            {
                Undo.RecordObjects(Selection.gameObjects, "Modify Object Names");

                foreach (GameObject obj in Selection.gameObjects)
                {
                    obj.name = GetModifiedName(obj.name);
                    EditorUtility.SetDirty(obj);
                }
            }
        }

        private string GetModifiedName(string originalName)
        {
            string newName = originalName;

            // Remove specified text if any
            if (!string.IsNullOrEmpty(_removeText))
            {
                newName = newName.Replace(_removeText, "");
            }

            // Add prefix and suffix
            newName = $"{_prefix}{newName}{_suffix}";

            return newName;
        }

    }

}

#endif