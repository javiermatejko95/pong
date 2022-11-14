using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("SO"), Space]
    [SerializeField] private DifficultySO[] difficulties = null;

    [Header("UI"), Space]
    [SerializeField] private CustomButton[] btnsDifficulty = null;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color unselectedColor = Color.white;
    #endregion

    #region PRIVATE_FIELDS
    private DIFFICULTIES difficultySelected = default;
    #endregion

    #region INIT
    public void Init()
    {
        for(int i = 0; i < difficulties.Length; i++)
        {
            DIFFICULTIES diff = difficulties[i].Difficulty;
            btnsDifficulty[i].SetText(diff.ToString());
            btnsDifficulty[i].SetOnClick(() => SetDifficulty(diff));
            btnsDifficulty[i].SetColor(selectedColor, unselectedColor);
            btnsDifficulty[i].ToggleSelected(difficultySelected == diff ? true : false);
        }
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS
    private void SetDifficulty(DIFFICULTIES difficulty)
    {
        difficultySelected = difficulty;

        for(int i = 0; i < difficulties.Length; i++)
        {
            btnsDifficulty[i].ToggleSelected(difficultySelected == difficulties[i].Difficulty ? true : false);
        }
    }
    #endregion
}
