using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace Leafling.Editor
{
    [CustomEditor(typeof(StateMachine<>), true)]
    public class StateMachineEditor : UnityEditor.Editor
    {
        private Type BaseStateType => _target.BaseStateType;

        private IStateMachine _target;

        private void OnEnable()
        {
            _target = target as IStateMachine;
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
            RefreshTargets();
            DisableAllStates();
            serializedObject.ApplyModifiedProperties();
        }


        private bool HardStateRefreshDialog()
        {
            string title = "Hard State Refresh";
            string message = "This will delete and re-attach every child state in this object and " +
                "reset any custom inspector values set on any state";
            string ok = "Hard Refresh";
            string cancel = "Cancel";
            return EditorUtility.DisplayDialog(title, message, ok, cancel);
        }
        private void DestroyAllStates()
        {
            IState[] states = AllAttachedStateInstances();
            for (int i = 0; i < states.Length; i++)
            {
                DestroyState(states[i]);
            }
        }
        private void DestroyState(IState state)
        {
            if (StateHasOtherComponents(state))
            {
                DestroyImmediate(state.Behaviour);
            }
            else
            {
                DestroyImmediate(state.Behaviour.gameObject);
            }
        }
        private bool StateHasOtherComponents(IState state)
        {
            return state.Behaviour.GetComponents<Component>().Length > 2;
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
            IState[] states = AllAttachedStateInstances();
            SerializedProperty statesProperty = serializedObject.FindProperty(_target.StateListPropertyPath);
            statesProperty.arraySize = states.Length;
            for (int i = 0; i < states.Length; i++)
            {
                statesProperty.GetArrayElementAtIndex(i).objectReferenceValue = states[i].Behaviour;
            }
        }
        private void RefreshTargets()
        {
            IState[] states = AllAttachedStateInstances();
            for (int i = 0; i < states.Length; i++)
            {
                states[i].RefreshTarget();
                EditorUtility.SetDirty(states[i].Behaviour);
            }
        }
        private void DisableAllStates()
        {
            IState[] states = AllAttachedStateInstances();
            for (int i = 0; i < states.Length; i++)
            {
                states[i].Behaviour.enabled = false;
            }
        }

        private IState[] AllAttachedStateInstances()
        {
            return _target.Behaviour.GetComponentsInChildren<IState>();
        }
        private bool IsState(Type type)
        {
            return type != BaseStateType && BaseStateType.IsAssignableFrom(type);
        }
        private Type StateType(IState state)
        {
            return state.GetType();
        }
        private GameObject CreateStateContainer(Type stateType)
        {
            GameObject container = new GameObject(stateType.Name);
            container.transform.parent = _target.Behaviour.transform;
            return container;
        }
        private Transform[] GetChildren()
        {
            Transform[] children = new Transform[_target.Behaviour.transform.childCount];
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = _target.Behaviour.transform.GetChild(i);
            }
            return children;
        }
        private string TransformName(Transform transform)
        {
            return transform.name;
        }
    }
}