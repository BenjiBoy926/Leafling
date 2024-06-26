using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace Leafling.Editor
{
    [CustomEditor(typeof(LeaflingStateMachine))]
    public class LeaflingStateMachineEditor : UnityEditor.Editor
    {
        private Type BaseStateType => typeof(LeaflingState);

        private LeaflingStateMachine _target;

        private void OnEnable()
        {
            _target = (LeaflingStateMachine)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Hard State Refresh"))
                {
                    HardStateRefresh();
                }
                if (GUILayout.Button("Soft State Refresh"))
                {
                    SoftStateRefresh();
                }
            }
        }
        private void HardStateRefresh()
        {
            if (!HardStateRefreshDialog())
            {
                return;
            }
            DestroyAllStates();
            SoftStateRefresh();
        }
        private void SoftStateRefresh()
        {
            AttachMissingStates();
            AlphabetizeChildTransforms();
            SetStates();
            DisableAllStates();
            serializedObject.ApplyModifiedProperties();
        }


        private bool HardStateRefreshDialog()
        {
            string title = "Hard State Refresh";
            string message = "This will delete and re-attach every child state in this object and " +
                "erase any custom inspector values set on any state";
            string ok = "Hard Refresh";
            string cancel = "Cancel";
            return EditorUtility.DisplayDialog(title, message, ok, cancel);
        }
        private void DestroyAllStates()
        {
            LeaflingState[] states = AllAttachedStateInstances();
            for (int i = 0; i < states.Length; i++)
            {
                DestroyState(states[i]);
            }
        }
        private void DestroyState(LeaflingState state)
        {
            if (StateHasOtherComponents(state))
            {
                DestroyImmediate(state);
            }
            else
            {
                DestroyImmediate(state.gameObject);
            }
        }
        private bool StateHasOtherComponents(LeaflingState state)
        {
            return state.GetComponents<Component>().Length > 2;
        }
        private void AttachMissingStates()
        {
            HashSet<Type> attachedStates = AllAttachedStateTypes();
            foreach (Type state in AllDefinedStates())
            {
                if (attachedStates.Contains(state))
                {
                    continue;
                }
                AttachState(state);
            }
        }
        private HashSet<Type> AllAttachedStateTypes()
        {
            return new(AllAttachedStateInstances().Select(StateType));
        }
        private IEnumerable<Type> AllDefinedStates()
        {
            return BaseStateType.Assembly.DefinedTypes.Where(IsState);
        }
        private void AttachState(Type stateType)
        {
            GameObject container = CreateStateContainer(stateType);
            container.AddComponent(stateType);
        }
        private void AlphabetizeChildTransforms()
        {
            Transform[] children = GetChildren().OrderBy(TransformName).ToArray();
            for (int i = 0; i < children.Length; i++)
            {
                children[i].SetSiblingIndex(i);
            }
        }
        private void SetStates()
        {
            LeaflingState[] states = AllAttachedStateInstances();
            SerializedProperty statesProperty = serializedObject.FindProperty(LeaflingStateMachine.StateListPropertyPath);
            statesProperty.arraySize = states.Length;
            for (int i = 0; i < states.Length; i++)
            {
                statesProperty.GetArrayElementAtIndex(i).objectReferenceValue = states[i];
            }
        }
        private void DisableAllStates()
        {
            LeaflingState[] states = AllAttachedStateInstances();
            for (int i = 0; i < states.Length; i++)
            {
                states[i].enabled = false;
            }
        }

        private LeaflingState[] AllAttachedStateInstances()
        {
            return _target.GetComponentsInChildren<LeaflingState>();
        }
        private bool IsState(Type type)
        {
            return type != BaseStateType && BaseStateType.IsAssignableFrom(type);
        }
        private Type StateType(LeaflingState state)
        {
            return state.GetType();
        }
        private GameObject CreateStateContainer(Type stateType)
        {
            GameObject container = new GameObject(stateType.Name);
            container.transform.parent = _target.transform;
            return container;
        }
        private Transform[] GetChildren()
        {
            Transform[] children = new Transform[_target.transform.childCount];
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = _target.transform.GetChild(i);
            }
            return children;
        }
        private string TransformName(Transform transform)
        {
            return transform.name;
        }
    }
}