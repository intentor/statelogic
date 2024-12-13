#if UNITY_EDITOR

using System.Collections.Generic;
using StateLogic.Utils;
using UnityEngine;

namespace StateLogic.Settings {
    /// <summary>
    /// Settings used by State Machines editor scripts.
    /// </summary>
    public class StateLogicEditorSettings : ScriptableObject {
        private const string SETTINGS_PATH = "Assets/Resources/Editor/StateLogic/StateLogicEditorSettings.asset";

        private static StateLogicEditorSettings instance;

        /// <summary>
        /// Stores foldout states for UI elements.
        /// </summary>
        public Dictionary<string, bool> FoldoutStates;

        /// <summary>
        /// Names of the available events.
        /// </summary>
        public List<string> EventNames;

        /// <summary>
        /// Reference to the asset generated from this object.
        /// </summary>
        public static StateLogicEditorSettings Instance {
            get {
                if (instance == null) {
                    instance = LoadOrCreate();
                }
                return instance;
            }
        }

        /// <summary>
        /// Load the settings.
        /// </summary>
        /// <returns>Loaded settings or <c>null</c> in case no settings were found.</returns>
        public static StateLogicEditorSettings Load() {
            Debug.Log("StateLogicEditorSettings | Load");
            return AssetUtils.LoadAsset<StateLogicEditorSettings>(SETTINGS_PATH);
        }

        /// <summary>
        /// Create the settings.
        /// </summary>
        /// <returns>Created settings.</returns>
        public static StateLogicEditorSettings Create() {
            Debug.Log("StateLogicEditorSettings | Create");
            var settings = ScriptableObject.CreateInstance<StateLogicEditorSettings>();
            AssetUtils.CreateAsset(settings, SETTINGS_PATH);
            return settings;
        }

        /// <summary>
        /// Load or create the settings.
        /// </summary>
        /// <returns>Created settings.</returns>
        public static StateLogicEditorSettings LoadOrCreate() {
            return Load() ?? Create();
        }
    }
}
#endif