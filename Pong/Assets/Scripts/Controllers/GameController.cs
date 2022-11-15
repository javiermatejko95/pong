using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameControllerActions
{
    public Action onPlay = null;
}

public class GameController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("Handlers"), Space]
    [SerializeField] private DifficultyHandler difficultyHandler = null;
    [SerializeField] private ControlHandler controlHandler = null;

    [Header("Views"), Space]
    [SerializeField] private MenuView menuView = null;
    #endregion

    #region PRIVATE_FIELDS
    private GameControllerActions gameControllerActions = new();
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        difficultyHandler.Init();

        menuView.Init(gameControllerActions, difficultyHandler.DifficultyHandlerActions);
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
