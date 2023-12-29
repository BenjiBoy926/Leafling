//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/GravityToy/Data/GravityToyActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GravityToyActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GravityToyActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GravityToyActions"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""e934f2e9-b7c9-423b-b88e-cc8ebfc59960"",
            ""actions"": [
                {
                    ""name"": ""IsGravityActive"",
                    ""type"": ""Value"",
                    ""id"": ""471d0f9f-1ba2-4b19-af3c-c1433a51d8c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GravityPosition"",
                    ""type"": ""Value"",
                    ""id"": ""392fe2db-07f9-463c-b68f-960c1a58b7c3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GravityMove"",
                    ""type"": ""Value"",
                    ""id"": ""5248b3a8-ad93-449f-ab24-819b7b647499"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b3a55255-197f-48c4-b977-3a7b7af48e07"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IsGravityActive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b56aad71-a0f1-46cd-9deb-a360f162758f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""6ad44771-d845-4194-b0e8-a74b299a8a3d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ae5f9299-47d4-46cd-82f1-5c4561f000f6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8f34278d-9486-4acd-b7e8-cb5d1453bc96"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""58af513b-5599-4741-99ca-a2246ed36670"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""df9a9896-58d0-4d4c-902d-3c33d96f6a96"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""ffe764ee-e4ec-4a39-b34e-3bc63375042f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e3d284ff-e537-4ed3-99fd-036584ba7f27"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3909c019-c088-4b9a-adf2-4b8d723e7112"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""66c871b7-7b69-40b2-b6d7-8061ee2621c1"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""11657fcb-8ce4-4cf6-90c0-6b4637b34feb"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GravityMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_IsGravityActive = m_Default.FindAction("IsGravityActive", throwIfNotFound: true);
        m_Default_GravityPosition = m_Default.FindAction("GravityPosition", throwIfNotFound: true);
        m_Default_GravityMove = m_Default.FindAction("GravityMove", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Default
    private readonly InputActionMap m_Default;
    private List<IDefaultActions> m_DefaultActionsCallbackInterfaces = new List<IDefaultActions>();
    private readonly InputAction m_Default_IsGravityActive;
    private readonly InputAction m_Default_GravityPosition;
    private readonly InputAction m_Default_GravityMove;
    public struct DefaultActions
    {
        private @GravityToyActions m_Wrapper;
        public DefaultActions(@GravityToyActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @IsGravityActive => m_Wrapper.m_Default_IsGravityActive;
        public InputAction @GravityPosition => m_Wrapper.m_Default_GravityPosition;
        public InputAction @GravityMove => m_Wrapper.m_Default_GravityMove;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void AddCallbacks(IDefaultActions instance)
        {
            if (instance == null || m_Wrapper.m_DefaultActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DefaultActionsCallbackInterfaces.Add(instance);
            @IsGravityActive.started += instance.OnIsGravityActive;
            @IsGravityActive.performed += instance.OnIsGravityActive;
            @IsGravityActive.canceled += instance.OnIsGravityActive;
            @GravityPosition.started += instance.OnGravityPosition;
            @GravityPosition.performed += instance.OnGravityPosition;
            @GravityPosition.canceled += instance.OnGravityPosition;
            @GravityMove.started += instance.OnGravityMove;
            @GravityMove.performed += instance.OnGravityMove;
            @GravityMove.canceled += instance.OnGravityMove;
        }

        private void UnregisterCallbacks(IDefaultActions instance)
        {
            @IsGravityActive.started -= instance.OnIsGravityActive;
            @IsGravityActive.performed -= instance.OnIsGravityActive;
            @IsGravityActive.canceled -= instance.OnIsGravityActive;
            @GravityPosition.started -= instance.OnGravityPosition;
            @GravityPosition.performed -= instance.OnGravityPosition;
            @GravityPosition.canceled -= instance.OnGravityPosition;
            @GravityMove.started -= instance.OnGravityMove;
            @GravityMove.performed -= instance.OnGravityMove;
            @GravityMove.canceled -= instance.OnGravityMove;
        }

        public void RemoveCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDefaultActions instance)
        {
            foreach (var item in m_Wrapper.m_DefaultActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DefaultActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    public interface IDefaultActions
    {
        void OnIsGravityActive(InputAction.CallbackContext context);
        void OnGravityPosition(InputAction.CallbackContext context);
        void OnGravityMove(InputAction.CallbackContext context);
    }
}