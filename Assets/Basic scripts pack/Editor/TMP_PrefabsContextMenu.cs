#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Basic.Editor
{
    /// <summary>
    /// Menu items to call in canvas. (doesn't find canvas by itself)
    /// </summary>
    public class TMP_PrefabsContextMenu
    {

        [MenuItem("GameObject/UI/With TextMeshPro/RawImage (TMP)")]
        static void CreateRawImageTMP(MenuCommand menuCommand)
        {
            var path = "Assets/Basic scripts pack/Prefabs/TextMeshPro/RawImage (TMP).prefab";

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

                GameObjectUtility.SetParentAndAlign(instance, menuCommand.context as GameObject);

                Undo.RegisterCreatedObjectUndo(instance, "Create " + instance.name);
                Selection.activeObject = instance;
            }
            else
            {
                Debug.LogError($"{path} is not a valid path.");
            }
        }

        [MenuItem("GameObject/UI/With TextMeshPro/Image (TMP)")]
        static void CreateImageTMP(MenuCommand menuCommand)
        {
            var path = "Assets/Basic scripts pack/Prefabs/TextMeshPro/Image (TMP).prefab";

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

                GameObjectUtility.SetParentAndAlign(instance, menuCommand.context as GameObject);

                Undo.RegisterCreatedObjectUndo(instance, "Create " + instance.name);
                Selection.activeObject = instance;
            }
            else
            {
                Debug.LogError($"{path} is not a valid path.");
            }
        }

        [MenuItem("GameObject/UI/With TextMeshPro/Toggle (TMP)")]
        static void CreateToggleTMP(MenuCommand menuCommand)
        {
            var path = "Assets/Basic scripts pack/Prefabs/TextMeshPro/Toggle (TMP).prefab";

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

                GameObjectUtility.SetParentAndAlign(instance, menuCommand.context as GameObject);

                Undo.RegisterCreatedObjectUndo(instance, "Create " + instance.name);
                Selection.activeObject = instance;
            }
            else
            {
                Debug.LogError($"{path} is not a valid path.");
            }
        }

        [MenuItem("GameObject/UI/With TextMeshPro/Slider (TMP)")]
        static void CreateSliderTMP(MenuCommand menuCommand)
        {
            var path = "Assets/Basic scripts pack/Prefabs/TextMeshPro/Slider (TMP).prefab";

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

                GameObjectUtility.SetParentAndAlign(instance, menuCommand.context as GameObject);

                Undo.RegisterCreatedObjectUndo(instance, "Create " + instance.name);
                Selection.activeObject = instance;
            }
            else
            {
                Debug.LogError($"{path} is not a valid path.");
            }
        }

        [MenuItem("GameObject/UI/With TextMeshPro/Scrollbar (TMP)")]
        static void CreateScrollbarTMP(MenuCommand menuCommand)
        {
            var path = "Assets/Basic scripts pack/Prefabs/TextMeshPro/Scrollbar (TMP).prefab";

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

                GameObjectUtility.SetParentAndAlign(instance, menuCommand.context as GameObject);

                Undo.RegisterCreatedObjectUndo(instance, "Create " + instance.name);
                Selection.activeObject = instance;
            }
            else
            {
                Debug.LogError($"{path} is not a valid path.");
            }
        }

    }
}

#endif