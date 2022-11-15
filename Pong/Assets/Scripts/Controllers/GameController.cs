using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameControllerActions
{
    public Action onPlay = null;
    public Action onPlayerOneScoreGoal = null;
    public Action onPlayerTwoScoreGoal = null;
}

public class GameController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("Handlers"), Space]
    [SerializeField] private LevelController levelController = null;
    [SerializeField] private DifficultyHandler difficultyHandler = null;
    [SerializeField] private ScoreController scoreController = null;
    [SerializeField] private InputHandler inputHandler = null;

    [Header("Views"), Space]
    [SerializeField] private MenuView menuView = null;

    [Header("Players"), Space]
    [SerializeField] private Paddle playerOne = null;
    [SerializeField] private Paddle playerTwo = null;
    [SerializeField] private Ball ball = null;
    #endregion

    #region PRIVATE_FIELDS
    private GameControllerActions gameControllerActions = new();
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        gameControllerActions.onPlay += Play;        

        levelController.Init(gameControllerActions);
        scoreController.Init(gameControllerActions);

        difficultyHandler.Init();
        inputHandler.Init();

        menuView.Init(gameControllerActions, difficultyHandler.DifficultyHandlerActions, inputHandler.InputHandlerActions);
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS
    private void Play()
    {
        StartCoroutine(IStart());

        IEnumerator IStart()
        {
            yield return new WaitForSeconds(2f);

            playerOne.TogglePlaying(true);
            ball.TogglePlaying(true);
        }
    }
    #endregion
}
