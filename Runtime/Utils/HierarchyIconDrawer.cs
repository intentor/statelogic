#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace StateLogic.Utils {
    /// <summary>
    /// Draw hierarchy icons for <see cref="StateMachine"/> components.
    /// </summary>
    [InitializeOnLoad]
    public static class HierarchyIconDrawer {
        private const string ICON_ASSET_NAME = "StateLogicIcon";

        private static Texture2D icon;

        static HierarchyIconDrawer() {
            var guids = AssetDatabase.FindAssets(ICON_ASSET_NAME);
            if (guids.Length == 0) return;

            var iconPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            icon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemOnGUI;
        }

        private static void HierarchyItemOnGUI(int instanceID, Rect selectionRect) {
            if (icon == null) return;

            var position = new Rect(selectionRect);
            position.x = position.width - 20;
            position.width = 20;

            var gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);
            if (gameObject != null && gameObject.GetComponent<StateMachine>())
                GUI.Label(position, icon);
        }
    }
}
#endif