using System.Collections;

using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private PowerUp powerUp = null;
    #endregion

    #region PRIVATE_FIELDS
    private Vector3 cameraBounds = new Vector3();

    private float powerUpSizeX = 0f;
    private float powerUpSizeY = 0f;
    #endregion

    #region INIT
    public void Init(GameControllerActions gameControllerActions, Vector3 cameraBounds)
    {
        gameControllerActions.onPowerUp += (poss) =>
        {
            TogglePowerUp(false);
            SetTimer();
        };

        gameControllerActions.onPlay += SetTimer;
        gameControllerActions.onExit += () =>
        {
            TogglePowerUp(false);
            StopAllCoroutines();
        };

        this.cameraBounds = cameraBounds;

        powerUpSizeX = powerUp.GetSpriteRenderer().bounds.size.x;
        powerUpSizeY = powerUp.GetSpriteRenderer().bounds.size.y;
    }
    #endregion

    #region PRIVATE_FIELDS
    private void TogglePowerUp(bool status)
    {
        powerUp.gameObject.SetActive(status);
    }

    private void SetTimer()
    {
        StartCoroutine(ISetTimer());

        IEnumerator ISetTimer()
        {
            float randomX = Random.Range(-cameraBounds.x, cameraBounds.x);
            float randomY = Random.Range(-cameraBounds.y + powerUpSizeY, cameraBounds.y - powerUpSizeY);

            transform.position = new Vector3(randomX, randomY, 1f);

            yield return new WaitForSeconds(Random.Range(2f, 4f));

            TogglePowerUp(true);
        }
    }
    #endregion
}
