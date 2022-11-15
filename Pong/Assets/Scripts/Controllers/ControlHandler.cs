using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHandlerActions
{
    public Action onGetControl = null;
}

public class ControlHandler : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("SO"), Space]
    [SerializeField] private ControlsSO[] controls = null;
    #endregion

    #region PRIVATE_FIELDS

    #endregion

    #region INIT
    public void Init()
    {

    }
    #endregion

    #region PRIVATE_METHODS
    private void SetControl()
    {

    }
    #endregion
}
