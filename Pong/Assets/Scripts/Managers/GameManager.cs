using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameManagerActions
{
    public Action onPlay = null;
}

public class GameManager : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("Managers"), Space]
    [SerializeField] private DifficultyManager difficultyManager = null;
    [SerializeField] private ControlManager controlManager = null;

    [Header("UI"), Space]
    [SerializeField] private Button btnPlay = null;
    #endregion

    #region PRIVATE_FIELDS

    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        difficultyManager.Init();
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
