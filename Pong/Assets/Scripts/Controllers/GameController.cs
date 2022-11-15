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
    [SerializeField] private LevelController levelController = null;
    [SerializeField] private DifficultyHandler difficultyHandler = null;
    [SerializeField] private InputHandler inputHandler = null;

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
        inputHandler.Init();

        menuView.Init(gameControllerActions, difficultyHandler.DifficultyHandlerActions, inputHandler.InputHandlerActions);
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
