using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private float speed = 7f;
    [SerializeField] private bool isCPU = false;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    #endregion

    #region PRIVATE_FIELDS
    private float height = 0f;
    private float yBound = 0f;    
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        yBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).y;
        height = spriteRenderer.bounds.size.y / 2;
    }

    private void Update()
    {
        if(isCPU)
        {

        }
        else
        {
            Movement();
        }        
    }
    #endregion

    #region PUBLIC_METHODS
    public void Init()
    {

    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
    #endregion

    #region PRIVATE_METHODS
    private void Movement()
    {
        float movement = Input.GetAxisRaw("Vertical");

        Vector2 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y + movement * speed * Time.deltaTime, -yBound + height, yBound - height);
        transform.position = pos;
    }
    #endregion
}
