using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private LevelView levelView = null;
    [SerializeField] private GameObject level = null;
    #endregion

    #region PRIVATE_FIELDS
    private GameControllerActions gameControllerActions = null;
    #endregion

    #region INIT
    public void Init(GameControllerActions gameControllerActions)
    {
        this.gameControllerActions = gameControllerActions;

        gameControllerActions.onPlay += () => ToggleView(true);
        gameControllerActions.onExit += () => ToggleView(false);
    }
    #endregion

    #region PRIVATE_METHODS
    private void ToggleView(bool status)
    {
        levelView.gameObject.SetActive(status);
        level.SetActive(status);
    }
    #endregion
}
