using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float speed = 7f;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Paddle playerOne = null;
    [SerializeField] private Paddle playerTwo = null;
    #endregion

    #region PRIVATE_FIELDS
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

    private float playerSizeX = 0f;
    private float playerSizeY = 0f;

    private POSSESSION currentPossession = default;

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
    public void Init(GameControllerActions gameControllerActions, AudioHandlerActions audioHandlerActions, Camera camera,Vector3 cameraBounds)
    {
        this.gameControllerActions = gameControllerActions;
        this.audioHandlerActions = audioHandlerActions;

        this.camera = camera;

        xBound = cameraBounds.x;
        yBound = cameraBounds.y;

        height = spriteRenderer.bounds.size.y / 2;
        width = spriteRenderer.bounds.size.x / 2;

        topBound = yBound - height;
        bottomBound = -yBound + height;

        leftBound = -xBound + width;
        rightBound = xBound - width;

        playerSizeX = playerOne.GetSpriteRenderer().bounds.size.x / 2;
        playerSizeY = playerOne.GetSpriteRenderer().bounds.size.y / 2;

        speed = (xBound + yBound) / 2;
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
        if (newPosX == leftBound)
        {
            movementX = 1f;
            pos = GetRandomPos();
            gameControllerActions.onPlayerTwoScoreGoal?.Invoke();
            audioHandlerActions.onPongHit?.Invoke();
            SetPossession(POSSESSION.NONE);
        }

        if (newPosX == rightBound)
        {
            movementX = -1f;
            pos = GetRandomPos();
            gameControllerActions.onPlayerOneScoreGoal?.Invoke();
            audioHandlerActions.onPongHit?.Invoke();
            SetPossession(POSSESSION.NONE);
        }

        if(newPosY == bottomBound)
        {
            movementY = 1f;
            audioHandlerActions.onPongHit?.Invoke();
        }

        if(newPosY == topBound)
        {
            movementY = -1f;
            audioHandlerActions.onPongHit?.Invoke();
        }

        if(newPosX > playerOne.transform.position.x - playerSizeX && 
            newPosX < playerOne.transform.position.x + playerSizeX &&
            newPosY > playerOne.transform.position.y - playerSizeY &&
            newPosY < playerOne.transform.position.y + playerSizeY)
        {
            movementX = 1f;
            audioHandlerActions.onPongHit?.Invoke();
            SetPossession(POSSESSION.PLAYER1);
        }

        if (newPosX > playerTwo.transform.position.x - playerSizeX &&
            newPosX < playerTwo.transform.position.x + playerSizeX &&
            newPosY > playerTwo.transform.position.y - playerSizeY &&
            newPosY < playerTwo.transform.position.y + playerSizeY)
        {
            movementX = -1f;
            audioHandlerActions.onPongHit?.Invoke();
            SetPossession(POSSESSION.PLAYER2);
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
    #endregion
}
