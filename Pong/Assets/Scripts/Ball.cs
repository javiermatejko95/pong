using UnityEngine;

public enum POSSESSION
{
    NONE,
    PLAYER1,
    PLAYER2
}

public class Ball : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Paddle playerOne = null;
    [SerializeField] private Paddle playerTwo = null;
    #endregion

    #region PRIVATE_FIELDS
    private float speed = 7f;
    private float originalSpeed = 0f;
    private float modifiedSpeed = 0f;

    private Camera camera = null;

    private float height = 0f;
    private float width = 0f;

    private float yBound = 0f;
    private float xBound = 0f;

    private float topBound = 0f;
    private float bottomBound = 0f;

    private float leftBound = 0f;
    private float rightBound = 0;

    private float movementX = 1f;
    private float movementY = 1f;    

    private bool isPlaying = false;

    private float playerOneSizeX = 0f;
    private float playerOneSizeY = 0f;

    private float playerTwoSizeX = 0f;
    private float playerTwoSizeY = 0f;

    private float powerUpSizeX = 0f;
    private float powerUpSizeY = 0f;

    private POSSESSION currentPossession = default;

    private PowerUp powerUp = null;

    private GameControllerActions gameControllerActions = null;
    private AudioHandlerActions audioHandlerActions = null;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        if(isPlaying)
        {
            Movement();
        }        
    }
    #endregion

    #region PUBLIC_METHODS
    public void Init(GameControllerActions gameControllerActions, AudioHandlerActions audioHandlerActions, Camera camera,Vector3 cameraBounds, PowerUp powerUp)
    {
        this.gameControllerActions = gameControllerActions;
        this.audioHandlerActions = audioHandlerActions;

        this.gameControllerActions.onPowerUp += (poss) =>
        {
            SetModifiedSpeed();
            UpdatePlayersSize();
        };
        this.gameControllerActions.onPowerUpFinish += () =>
        {
            SetOriginalSpeed();
            UpdatePlayersSize();
        };
        this.gameControllerActions.onPlay += () =>
        {
            SetOriginalSpeed();
            UpdatePlayersSize();
        };
        this.gameControllerActions.onPlayerOneScoreGoal += SetOriginalSpeed;
        this.gameControllerActions.onPlayerTwoScoreGoal += SetOriginalSpeed;

        this.camera = camera;

        this.powerUp = powerUp;

        xBound = cameraBounds.x;
        yBound = cameraBounds.y;

        height = spriteRenderer.bounds.size.y / 2;
        width = spriteRenderer.bounds.size.x / 2;

        topBound = yBound - height;
        bottomBound = -yBound + height;

        leftBound = -xBound + width;
        rightBound = xBound - width;

        UpdatePlayersSize();

        powerUpSizeX = powerUp.GetSpriteRenderer().bounds.size.x / 2;
        powerUpSizeY = powerUp.GetSpriteRenderer().bounds.size.y / 2;

        speed = (xBound + yBound) / 2;
        originalSpeed = speed;
        modifiedSpeed = speed + (speed / 2);
    }

    public void TogglePlaying(bool status)
    {
        isPlaying = status;
    }
    #endregion

    #region PRIVATE_METHODS
    private void Movement()
    {
        Vector2 pos = transform.position;

        float newPosX = Mathf.Clamp(pos.x + movementX * speed * Time.deltaTime, leftBound, rightBound);
        float newPosY = Mathf.Clamp(pos.y + movementY * speed * Time.deltaTime, bottomBound, topBound);    

        pos.x = newPosX;
        pos.y = newPosY;

        CheckCollision(newPosX, newPosY, ref pos);

        transform.position = pos;
    }

    private void CheckCollision(float newPosX, float newPosY, ref Vector2 pos)
    {
        CheckCollisionWithLeftBound(newPosX, ref pos);

        CheckCollisionWithRightBound(newPosX, ref pos);

        CheckCollisionWithBottomBound(newPosY);

        CheckCollsionWithTopBound(newPosY);

        CheckCollsionWithPlayerOne(playerOne.transform, newPosX, newPosY);

        CheckCollsionWithPlayerTwo(playerTwo.transform, newPosX, newPosY);

        CheckCollisionWithPowerUp(powerUp.transform, newPosX, newPosY);
    }

    private void CheckCollisionWithLeftBound(float newPosX, ref Vector2 pos)
    {
        if (newPosX == leftBound)
        {
            movementX = 1f;
            pos = GetRandomPos();
            gameControllerActions.onPlayerTwoScoreGoal?.Invoke();
            audioHandlerActions.onPongHit?.Invoke();
            SetPossession(POSSESSION.NONE);
        }
    }

    private void CheckCollisionWithRightBound(float newPosX, ref Vector2 pos)
    {
        if (newPosX == rightBound)
        {
            movementX = -1f;
            pos = GetRandomPos();
            gameControllerActions.onPlayerOneScoreGoal?.Invoke();
            audioHandlerActions.onPongHit?.Invoke();
            SetPossession(POSSESSION.NONE);
        }
    }

    private void CheckCollisionWithBottomBound(float newPosY)
    {
        if (newPosY == bottomBound)
        {
            movementY = 1f;
            audioHandlerActions.onPongHit?.Invoke();
        }
    }

    private void CheckCollsionWithTopBound(float newPosY)
    {
        if (newPosY == topBound)
        {
            movementY = -1f;
            audioHandlerActions.onPongHit?.Invoke();
        }
    }

    private void CheckCollsionWithPlayerOne(Transform player, float newPosX, float newPosY)
    {
        if (newPosX > player.transform.position.x - playerOneSizeX &&
            newPosX < player.transform.position.x + playerOneSizeX &&
            newPosY > player.transform.position.y - playerOneSizeY &&
            newPosY < player.transform.position.y + playerOneSizeY)
        {
            movementX = 1f;
            audioHandlerActions.onPongHit?.Invoke();
            SetPossession(POSSESSION.PLAYER1);
        }
    }

    private void CheckCollsionWithPlayerTwo(Transform player, float newPosX, float newPosY)
    {
        if (newPosX > player.transform.position.x - playerOneSizeX &&
            newPosX < player.transform.position.x + playerOneSizeX &&
            newPosY > player.transform.position.y - playerOneSizeY &&
            newPosY < player.transform.position.y + playerOneSizeY)
        {
            movementX = -1f;
            audioHandlerActions.onPongHit?.Invoke();
            SetPossession(POSSESSION.PLAYER2);
        }
    }

    private void CheckCollisionWithPowerUp(Transform powerUp, float newPosX, float newPosY)
    {
        if (newPosX > powerUp.transform.position.x - powerUpSizeX &&
            newPosX < powerUp.transform.position.x + powerUpSizeX &&
            newPosY > powerUp.transform.position.y - powerUpSizeY &&
            newPosY < powerUp.transform.position.y + powerUpSizeY)
        {
            if(currentPossession != POSSESSION.NONE && powerUp.gameObject.activeSelf)
            {
                gameControllerActions.onPowerUp?.Invoke(currentPossession);
                audioHandlerActions.onPongHit?.Invoke();
            }            
        }
    }

    private Vector2 GetRandomPos()
    {
        Vector2 newRandomPos = Vector2.zero;
        newRandomPos.y = UnityEngine.Random.Range(-yBound, yBound);

        return newRandomPos;
    }

    private void SetPossession(POSSESSION currentPossession)
    {
        this.currentPossession = currentPossession;
    }

    private void SetOriginalSpeed()
    {
        speed = originalSpeed;
    }

    private void SetModifiedSpeed()
    {
        speed = modifiedSpeed;
    }

    private void UpdatePlayersSize()
    {
        playerOneSizeX = playerOne.GetSpriteRenderer().bounds.size.x / 2;
        playerOneSizeY = playerOne.GetSpriteRenderer().bounds.size.y / 2;

        playerTwoSizeX = playerTwo.GetSpriteRenderer().bounds.size.x / 2;
        playerTwoSizeY = playerTwo.GetSpriteRenderer().bounds.size.y / 2;
    }
    #endregion
}
