using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private CustomButton[] btnsDifficulty = null;
    [SerializeField] private CustomButton[] btnsInput = null;
    [SerializeField] private CustomButton btnPlay = null;

    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color unselectedColor = Color.white;
    #endregion

    #region PRIVATE_FIELDS
    private DifficultyHandlerActions difficultyHandlerActions = null;
    private InputHandlerActions inputHandlerActions = null;
    #endregion

    #region INIT
    public void Init(GameControllerActions gameControllerActions, DifficultyHandlerActions difficultyHandlerActions, InputHandlerActions inputHandlerActions)
    {
        this.difficultyHandlerActions = difficultyHandlerActions;
        this.inputHandlerActions = inputHandlerActions;

        gameControllerActions.onPlay += () => ToggleView(false);
        gameControllerActions.onExit += () => ToggleView(true);

        SetPlayButton(gameControllerActions);
        SetDifficultyButtons(difficultyHandlerActions);
        SetInputButtons(inputHandlerActions);
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS
    private void SetPlayButton(GameControllerActions gameControllerActions)
    {
        btnPlay.SetOnClick(gameControllerActions.onPlay);
    }

    private void SetDifficultyButtons(DifficultyHandlerActions difficultyHandlerActions)
    {
        DifficultySO[] difficulties = difficultyHandlerActions.onGetDifficulties?.Invoke();
        DIFFICULTY difficultySelected = (DIFFICULTY)(difficultyHandlerActions.onGetDifficultySelected?.Invoke());

        for (int i = 0; i < difficulties.Length; i++)
        {
            DIFFICULTY diff = difficulties[i].Difficulty;
            btnsDifficulty[i].SetText(diff.ToString());
            btnsDifficulty[i].SetOnClick(() => {
                difficultyHandlerActions.onSetDifficulty?.Invoke(diff);
                SetDifficulty(difficulties);
                });
            btnsDifficulty[i].SetColor(selectedColor, unselectedColor);
            btnsDifficulty[i].ToggleSelected(difficultySelected == diff ? true : false);
        }
    }

    private void SetDifficulty(DifficultySO[] difficulties)
    {
        DIFFICULTY difficultySelected = (DIFFICULTY)difficultyHandlerActions.onGetDifficultySelected?.Invoke();

        for (int i = 0; i < difficulties.Length; i++)
        {
            btnsDifficulty[i].ToggleSelected(difficultySelected == difficulties[i].Difficulty ? true : false);
        }
    }

    private void SetInputButtons(InputHandlerActions inputHandlerActions)
    {
        InputSO[] inputs = inputHandlerActions.onGetInputs?.Invoke();
        INPUT selectedInput = (INPUT)inputHandlerActions.onGetSelectedInput?.Invoke();

        for(int i = 0; i < inputs.Length; i++)
        {
            INPUT input = inputs[i].Input;
            btnsInput[i].SetText(input.ToString());
            btnsInput[i].SetOnClick(() => {
                inputHandlerActions.onSetInput?.Invoke(input);
                SetInput(inputs);
            });
            btnsInput[i].SetColor(selectedColor, unselectedColor);
            btnsInput[i].ToggleSelected(selectedInput == input ? true : false);
        }
    }

    private void SetInput(InputSO[] inputs)
    {
        INPUT selectedInput = (INPUT)inputHandlerActions.onGetSelectedInput?.Invoke();

        for (int i = 0; i < inputs.Length; i++)
        {
            btnsInput[i].ToggleSelected(selectedInput == inputs[i].Input ? true : false);
        }
    }

    private void ToggleView(bool status)
    {
        this.gameObject.SetActive(status);
    }
    #endregion
}
