using UnityEditor;
using UnityEngine;

public class DropdownMenu : MonoBehaviour
{
    [MenuItem("Dropdown", menuItem = "GameObject/UI/Dropdown")]
    private static void CreatePrefab(MenuCommand menuCommand)
    {
        var go = Instantiate(AssetDatabase.
            LoadAssetAtPath<GameObject>("Assets/Plugins/Dropdown/Prefab/Dropdown.prefab"));

        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}
