#if UNITY_EDITOR

using System;
using System.Linq;
using StateLogic.Settings;
using UnityEditor;
using UnityEngine;

namespace StateLogic {
    /// <summary>
    /// Editor for <see cref="State"/>.
    /// </summary>
    [CustomEditor(typeof(State), true)]
    public sealed class StateEditor : Editor {
        public override void OnInspectorGUI() {
            var state = (State)target;

            if (String.IsNullOrEmpty(state.StateName)) {
                state.StateName = GetStateNameFromType(state.GetType());
            }

            RenderTransitionEventSelector(state);

            if (GUI.changed) {
                EditorUtility.SetDirty(target);
            }

            base.OnInspectorGUI();
        }

        private void RenderTransitionEventSelector(State state) {
            var eventNames = StateLogicEditorSettings.Instance.EventNames;
            if (eventNames == null || eventNames.Count == 0) {
                EditorGUILayout.HelpBox(
                    "There are no transition events. Please create events to set the transition to this state.",
                    MessageType.Error);
                return;
            }

            var index = eventNames.IndexOf(state.TransitionEvent);
            if (index < 0) index = 0;
            index = EditorGUILayout.Popup(
                new GUIContent("Transition event", "Event which when sent transitions to this state."),
                index,
                eventNames.ToArray<string>());
            state.TransitionEvent = eventNames[index];
        }

        private string GetStateNameFromType(Type type) {
            var typeName = type.Name;
            return typeName.EndsWith("State") ? typeName[0..^5] : typeName;
        }
    }
}
#endif