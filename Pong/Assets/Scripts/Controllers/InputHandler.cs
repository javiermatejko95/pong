using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerActions
{
    public Action<INPUT> onSetInput = null;
    public Func<INPUT> onGetSelectedInput = null;
    public Func<InputSO[]> onGetInputs = null;
}

public class InputHandler : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("SO"), Space]
    [SerializeField] private InputSO[] inputs = null;
    #endregion

    #region PRIVATE_FIELDS
    private InputHandlerActions inputHandlerActions = new();

    private INPUT selectedInput = default;
    #endregion

    #region PROPERTIES
    public InputHandlerActions InputHandlerActions { get => inputHandlerActions; }
    #endregion

    #region INIT
    public void Init()
    {
        inputHandlerActions.onSetInput += SetInput;
        inputHandlerActions.onGetInputs += GetInputs;
        inputHandlerActions.onGetSelectedInput += GetSelectedInput;
    }
    #endregion

    #region PRIVATE_METHODS
    private INPUT GetSelectedInput()
    {
        return selectedInput;
    }

    private InputSO[] GetInputs()
    {
        return inputs;
    }

    private void SetInput(INPUT selectedInput)
    {
        this.selectedInput = selectedInput;
    }    
    #endregion
}
