#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using StateLogic.Settings;
using StateLogic.Utils;
using UnityEditor;
using UnityEngine;

namespace StateLogic {
    /// <summary>
    /// Editor for <see cref="StateMachine"/>.
    /// </summary>
    [CustomEditor(typeof(StateMachine))]
    public sealed class StateManagerEditor : Editor {
        public void OnEnable() {
            ((StateMachine)target).LoadStates();
        }

        public override void OnInspectorGUI() {
            var stateManager = (StateMachine)target;

            if (string.IsNullOrEmpty(stateManager.ManagerName)) {
                stateManager.ManagerName =
                    $"{stateManager.gameObject.name} State Manager";
            }

            RenderManagerStateHeader(stateManager);
            RenderInitialTransitionEvent(stateManager);
            RenderEmptyStatesBox(stateManager);
            RenderStates(stateManager.States, stateManager.CurrentState);

            if (GUI.changed) {
                EditorUtility.SetDirty(target);
            }
        }

        private void RenderManagerStateHeader(StateMachine stateManager) {
            EditorGUILayout.BeginHorizontal();

            stateManager.ManagerName = EditorGUILayout.TextField(
                new GUIContent("Manager name", "Name of the state manager."),
                stateManager.ManagerName);

            if (GUILayout.Button(new GUIContent("Events", "Access event settings."), GUILayout.Width(50))) {
                AssetUtils.FocusOnProjectAsset(StateLogicEditorSettings.Instance);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void RenderInitialTransitionEvent(StateMachine stateManager) {
            var eventNames = StateLogicEditorSettings.Instance.EventNames;
            if (eventNames == null || eventNames.Count == 0) {
                EditorGUILayout.HelpBox(
                    "There are no transition events. Please create events to set the initial transition.",
                    MessageType.Error);
                return;
            }

            var popUpOptions = new string[eventNames.Count + 1];
            popUpOptions[0] = "---";
            Array.Copy(eventNames.ToArray(), 0, popUpOptions, 1, eventNames.Count);

            var index = Array.IndexOf(popUpOptions, stateManager.InitialTransitionEvent);
            if (index < 0) index = 0;
            index = EditorGUILayout.Popup(
                new GUIContent("Initial transition event", "First event to be triggered in this state machine."),
                index,
                popUpOptions);
            stateManager.InitialTransitionEvent = popUpOptions[index];
        }

        private void RenderEmptyStatesBox(StateMachine stateManager) {
            if (stateManager.States == null || stateManager.States.Count == 0) {
                EditorGUILayout.HelpBox(
                    "No states have been found.\nAdd components that inherit from StateLogic.State to set the available states.",
                    MessageType.Warning);
                return;
            }
        }

        private void RenderStates(Dictionary<string, State> states, State currentState) {
            EditorGUILayout.LabelField("States", EditorStyles.boldLabel);

            EditorGUI.indentLevel++;
            foreach (var entry in states) {
                var currentMark = entry.Value == currentState ? " [X}" : "";
                EditorGUILayout.LabelField($"{entry.Value.StateName}{currentMark}");
            }
            EditorGUI.indentLevel--;
        }
    }
}
#endif