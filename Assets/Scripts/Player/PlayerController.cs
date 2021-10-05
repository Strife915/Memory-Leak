using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float movementSpeed;
    public float limitX;
    public float xSpeed;
    private float _lastTouchedX;
    public Rigidbody playerRigidBody;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        instance = this;
    }
    void Update()
    {
        if(GameManager.instace.State == GameManager.GameState.IsStarted)
        {
            PlayerMoveOnX();
            PlayerMovement(PathCreator.instance.myPath[PathCreator.instance.listIndex]);
        }
    }
    //This Function make player move to next list index gameobject of z
    public void PlayerMovement(GameObject gameobject)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, gameobject.transform.position.z), movementSpeed * Time.deltaTime); ;
    }
    //Player movement on x axis
    private void PlayerMoveOnX()
    {
        float newX = 0;
        float touchxDelta = 0;
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _lastTouchedX = Input.GetTouch(0).position.x;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchxDelta = 2 * (_lastTouchedX - Input.GetTouch(0).position.x) / Screen.width;
            }

        }
        else if (Input.GetMouseButton(0))
        {
            touchxDelta = Input.GetAxis("Mouse X");
        }
        newX = transform.position.x + xSpeed * touchxDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitX, limitX);

        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
    // Enables gravity when we start the game
    public void StartMovement()
    {
        playerRigidBody.useGravity = enabled;
    }
}
