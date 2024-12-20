//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts-CONTROLADOR/XRControls.inputactions
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

public partial class @XRControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @XRControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""XRControls"",
    ""maps"": [
        {
            ""name"": ""Controllers"",
            ""id"": ""40d6d9bd-7e28-40f0-abfa-6d3c6808c83e"",
            ""actions"": [
                {
                    ""name"": ""AdvanceStep"",
                    ""type"": ""Button"",
                    ""id"": ""f4c159da-cfe8-4b40-8ead-97a717088ceb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c5632115-b132-4623-8ca7-f3360c41be84"",
                    ""path"": ""<XRController>{RightHand}/primaryButton "",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AdvanceStep"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controllers
        m_Controllers = asset.FindActionMap("Controllers", throwIfNotFound: true);
        m_Controllers_AdvanceStep = m_Controllers.FindAction("AdvanceStep", throwIfNotFound: true);
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

    // Controllers
    private readonly InputActionMap m_Controllers;
    private List<IControllersActions> m_ControllersActionsCallbackInterfaces = new List<IControllersActions>();
    private readonly InputAction m_Controllers_AdvanceStep;
    public struct ControllersActions
    {
        private @XRControls m_Wrapper;
        public ControllersActions(@XRControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AdvanceStep => m_Wrapper.m_Controllers_AdvanceStep;
        public InputActionMap Get() { return m_Wrapper.m_Controllers; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllersActions set) { return set.Get(); }
        public void AddCallbacks(IControllersActions instance)
        {
            if (instance == null || m_Wrapper.m_ControllersActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ControllersActionsCallbackInterfaces.Add(instance);
            @AdvanceStep.started += instance.OnAdvanceStep;
            @AdvanceStep.performed += instance.OnAdvanceStep;
            @AdvanceStep.canceled += instance.OnAdvanceStep;
        }

        private void UnregisterCallbacks(IControllersActions instance)
        {
            @AdvanceStep.started -= instance.OnAdvanceStep;
            @AdvanceStep.performed -= instance.OnAdvanceStep;
            @AdvanceStep.canceled -= instance.OnAdvanceStep;
        }

        public void RemoveCallbacks(IControllersActions instance)
        {
            if (m_Wrapper.m_ControllersActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IControllersActions instance)
        {
            foreach (var item in m_Wrapper.m_ControllersActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ControllersActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ControllersActions @Controllers => new ControllersActions(this);
    public interface IControllersActions
    {
        void OnAdvanceStep(InputAction.CallbackContext context);
    }
}
