using System.Collections;

using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private float speed = 7f;
    [SerializeField] private float startingPosX = 1f;
    [SerializeField] private bool isCPU = false;
    [SerializeField] private POSSESSION myPossession = default;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    #endregion

    #region PRIVATE_FIELDS
    private Camera camera = null;

    private float width = 0f;
    private float height = 0f;

    private float xBound = 0f;
    private float yBound = 0f;

    private float originalWidth = 0f;
    private float modifiedWidth = 0f;

    private float originalHeight = 0f;
    private float modifiedHeight = 0f;

    private bool isPlaying = false;

    private float difficultySpeed = 1f;

    private Vector3 originalScale = new Vector3();
    private Vector3 modifiedScale = new Vector3();

    private INPUT input = default;    

    private Ball ball = null;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {        
        Movement();  
    }
    #endregion

    #region PUBLIC_METHODS
    public void Init(GameControllerActions gameControllerActions, Camera camera,Vector3 cameraBounds)
    {
        gameControllerActions.onExit += () =>
        {
            ResetPos();
            SetOriginalScale();
            SetOriginalBounds();
            StopAllCoroutines();
        };

        gameControllerActions.onPowerUp += (poss) => {
            
            if(poss == myPossession)
            {
                SetPowerUp(gameControllerActions);
            }            
        };
        gameControllerActions.onPowerUpFinish += () =>
        {
            SetOriginalScale();
            SetOriginalBounds();
        };

        this.camera = camera;

        xBound = cameraBounds.x;
        yBound = cameraBounds.y;

        SetBounds();
        SetScales();
        ResetPos();
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }

    public void TogglePlaying(bool status)
    {
        isPlaying = status;
    }

    public void SetDifficultySpeed(float difficultySpeed)
    {
        this.difficultySpeed = difficultySpeed;
    }

    public void SetInput(INPUT input)
    {
        this.input = input;
    }

    public void SetBall(Ball ball)
    {
        this.ball = ball;
    }

    public void SetPowerUp(GameControllerActions gameControllerActions)
    {
        SetModifiedBounds();
        SetModifiedScale();

        StartCoroutine(ICountdown());

        IEnumerator ICountdown()
        {
            yield return new WaitForSeconds(3f);

            gameControllerActions.onPowerUpFinish?.Invoke();
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void Movement()
    {
        if(!isPlaying)
        {
            return;
        }

        float movement = GetInput();

        Vector2 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y + movement * speed * Time.deltaTime, -yBound + height, yBound - height);
        transform.position = pos;
    }

    private float GetInput()
    {
        float movement = 0f;

        if(isCPU)
        {
            movement = Mathf.Clamp(ball.transform.position.y - transform.position.y, -1, 1);
            movement *= difficultySpeed;
        }
        else
        {
            switch(input)
            {
                case INPUT.KEYBOARD:
                    movement = Input.GetAxisRaw("Vertical");
                    break;
                case INPUT.MOUSE:
                    movement = Mathf.Clamp((camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)).y) - transform.position.y, -1, 1);
                    break;
            }
            
        }

        return movement;
    }

    private void ResetPos()
    {
        Vector2 newPos = transform.position;
        newPos.y = 0;

        newPos.x = isCPU ? xBound - width - startingPosX : -xBound + width + startingPosX;

        transform.position = newPos;
    }

    private void SetScales()
    {
        originalScale = new Vector3((yBound * 2) / 5, (xBound * 2) - 2, 1f);

        float percentX = originalScale.x + (20f * originalScale.x / 100f);
        float percentY = originalScale.y + (20f * originalScale.y / 100f);

        modifiedScale = new Vector3(originalScale.x + percentX, originalScale.y + percentY, originalScale.y);        
    }

    private void SetOriginalScale()
    {
        transform.localScale = originalScale;
    }

    private void SetModifiedScale()
    {
        transform.localScale = modifiedScale;
    }

    private void SetBounds()
    {
        width = spriteRenderer.bounds.size.x / 2;
        height = spriteRenderer.bounds.size.y / 2;

        originalWidth = width;
        modifiedWidth = width + (20f * width / 100f);

        originalHeight = height;
        modifiedHeight = height + (20f * height / 100f);
    }

    private void SetOriginalBounds()
    {
        width = originalWidth;
        height = originalHeight;
    }

    private void SetModifiedBounds()
    {
        width = spriteRenderer.bounds.size.x / 2;
        height = spriteRenderer.bounds.size.y / 2;
    }
    #endregion
}
