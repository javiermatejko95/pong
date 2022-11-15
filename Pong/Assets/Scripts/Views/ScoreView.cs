using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreView : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private TextMeshProUGUI scorePlayerOne = null;
    [SerializeField] private TextMeshProUGUI scorePlayerTwo = null;
    #endregion

    #region PUBLIC_METHODS
    public void SetScorePlayerOne(int score)
    {
        scorePlayerOne.text = score.ToString();
    }

    public void SetScorePlayerTwo(int score)
    {
        scorePlayerTwo.text = score.ToString();
    }
    #endregion
}
