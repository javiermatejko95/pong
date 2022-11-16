using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControllerActions
{
    public Action<int> onSetScorePlayerOne = null;
    public Action<int> onSetScorePlayerTwo = null;
}

public class ScoreController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private ScoreView scoreView = null;
    #endregion

    #region PRIVATE_FIELDS
    private int scorePlayerOne = 0;
    private int scorePlayerTwo = 0;

    private ScoreControllerActions scoreControllerActions = new();
    private GameControllerActions gameControllerActions = null;
    #endregion

    #region PROPERTIES
    public ScoreControllerActions ScoreControllerActions { get => scoreControllerActions; }
    #endregion

    #region INIT
    public void Init(GameControllerActions gameControllerActions)
    {
        this.gameControllerActions = gameControllerActions;

        scoreControllerActions.onSetScorePlayerOne += scoreView.SetScorePlayerOne;
        scoreControllerActions.onSetScorePlayerTwo += scoreView.SetScorePlayerTwo;
        
        gameControllerActions.onPlayerOneScoreGoal += () => {
            ScorePlayerOne();
            scoreControllerActions.onSetScorePlayerOne?.Invoke(scorePlayerOne); };

        gameControllerActions.onPlayerTwoScoreGoal += () => {
            ScorePlayerTwo();
            scoreControllerActions.onSetScorePlayerTwo?.Invoke(scorePlayerTwo); };

        gameControllerActions.onExit += ResetGame;

        ResetGame();
    }
    #endregion

    #region PRIVATE_METHODS
    private void ResetGame()
    {
        scorePlayerOne = 0;
        scorePlayerTwo = 0;

        scoreView.SetScorePlayerOne(scorePlayerOne);
        scoreView.SetScorePlayerTwo(scorePlayerTwo);
    }

    private void ScorePlayerOne()
    {
        scorePlayerOne++;
        gameControllerActions.onCheckScore?.Invoke(scorePlayerOne, true);
    }

    private void ScorePlayerTwo()
    {
        scorePlayerTwo++;
        gameControllerActions.onCheckScore?.Invoke(scorePlayerTwo, false);
    }
    #endregion
}
