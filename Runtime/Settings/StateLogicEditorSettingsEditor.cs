#if UNITY_EDITOR

using System.Collections.Generic;
using StateLogic.Settings;
using UnityEditor;
using UnityEngine;

namespace StateLogic {
    /// <summary>
    /// Editor for <see cref="StateLogicEditorSettings"/>.
    /// </summary>
    [CustomEditor(typeof(StateLogicEditorSettings))]
    public sealed class StateLogicEditorSettingsEditor : Editor {
        private const string FOLDOUT_EVENTS = "Events";

        public override void OnInspectorGUI() {
            var settings = (StateLogicEditorSettings)target;
            if (settings.EventNames == null) {
                settings.EventNames = new List<string>();
            }

            EditorGUILayout.HelpBox(
                "Here you edit global settings that affects all state machines and their editors.",
                MessageType.Info);

            RenderEventsFoldout(settings);

            if (GUI.changed) {
                EditorUtility.SetDirty(target);
            }
        }

        private void RenderEventsFoldout(StateLogicEditorSettings settings) {
            var foldoutState = GetOrCreateFoldoutState(FOLDOUT_EVENTS, settings);
            var fouldoutTitle = $"{FOLDOUT_EVENTS} ({settings.EventNames.Count})";
            foldoutState = EditorGUILayout.BeginFoldoutHeaderGroup(
                foldoutState, fouldoutTitle, null, ShowEventsContextMenu);

            settings.FoldoutStates[FOLDOUT_EVENTS] = foldoutState;
            if (foldoutState) {
                RenderEventsList(settings);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void RenderEventsList(StateLogicEditorSettings settings) {
            var eventNames = settings.EventNames;
            for (var i = 0; i < eventNames.Count; i++) {
                var eventName = eventNames[i];

                EditorGUILayout.BeginHorizontal();

                eventNames[i] = EditorGUILayout.TextField(eventName);
                if (GUILayout.Button(new GUIContent("X", "Remove this event."), GUILayout.Width(50))) {
                    if (EditorUtility.DisplayDialog(
                        "Event removal",
                         $"Are you sure you want to remove the event '{eventName}'.\nThe event won't be available to state machines.",
                         "Yes",
                         "No")) {
                        settings.EventNames.Remove(eventName);
                        i--;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void ShowEventsContextMenu(Rect position) {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add event"), false, OnAddEventContextMenuClick);
            menu.AddItem(new GUIContent("Sort by name"), false, OnSortEventsMenuClick);
            menu.DropDown(position);
        }

        private void OnAddEventContextMenuClick() {
            var settings = (StateLogicEditorSettings)target;
            var index = settings.EventNames.Count + 1;
            settings.EventNames.Add($"Event {index}");
            settings.FoldoutStates[FOLDOUT_EVENTS] = true;
        }

        private void OnSortEventsMenuClick() {
            var settings = (StateLogicEditorSettings)target;
            settings.EventNames.Sort();
        }

        private bool GetOrCreateFoldoutState(string elementName, StateLogicEditorSettings settings) {
            if (settings.FoldoutStates == null) {
                settings.FoldoutStates = new Dictionary<string, bool>();
            }

            if (!settings.FoldoutStates.ContainsKey(FOLDOUT_EVENTS)) {
                settings.FoldoutStates[FOLDOUT_EVENTS] = false;
            }

            return settings.FoldoutStates[FOLDOUT_EVENTS];
        }
    }
}
#endif