using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private float speed = 7f;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Paddle player = null;
    #endregion

    #region PRIVATE_FIELDS
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

    private bool started = false;




    private float playerSizeX = 0f;
    private float playerSizeY = 0f;
    #endregion

    #region ACTIONS
    //private Action onReachedGoal = null;
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        yBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).y;
        xBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;

        height = spriteRenderer.bounds.size.y / 2;
        width = spriteRenderer.bounds.size.x / 2;

        topBound = yBound - height;
        bottomBound = -yBound + height;

        leftBound = -xBound + width;
        rightBound = xBound - width;


        playerSizeX = player.GetSpriteRenderer().bounds.size.x / 2;
        playerSizeY = player.GetSpriteRenderer().bounds.size.y / 2;




        started = true;
    }

    private void Update()
    {
        if(started)
        {
            Movement();
        }        
    }
    #endregion

    #region PUBLIC_METHODS
    public void Init()
    {

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

        CheckCollision(newPosX, newPosY);

        transform.position = pos;
    }

    private void CheckCollision(float newPosX, float newPosY)
    {
        if (newPosX == leftBound || newPosY == bottomBound)
        {
            movementX = 1f;
        }

        if (newPosX == rightBound || newPosY == topBound)
        {
            movementX = -1f;
        }

        if(newPosX > player.transform.position.x - playerSizeX && 
            newPosX < player.transform.position.x + playerSizeX &&
            newPosY > player.transform.position.y - playerSizeY &&
            newPosY < player.transform.position.y + playerSizeY)
        {
            movementX = 1f;
        }
    }
    #endregion
}
