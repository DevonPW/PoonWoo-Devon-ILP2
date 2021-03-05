using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rBody;

    [SerializeField]
    float moveSpeed = 0;

    [SerializeField]
    float turnRatio = 10;

    [SerializeField]
    float minPointerDistance = 0;
    //minimum distance mouse pointer has to be from player in order to trigger movement

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //0: left mouse button, 1: right mouse button, 2: middle mouse button

        //checking if left mouse button is held
        if (Input.GetMouseButton(0)) {
            //getting vector from player to mouse position
            Vector2 mouseVec = new Vector2();
            mouseVec = getMousePos() - transform.position;

            //getting angle between player (x axis?) and mouse
            float rotAngle = (Mathf.Rad2Deg * Mathf.Atan2(mouseVec.y, mouseVec.x) - 90);
            rotAngle = rotAngle < 0 ? rotAngle + 360 : rotAngle; //keep rotation angle measured as positive from y axis
            //Debug.Log("Mouse Angle: " + rotAngle);

            //rotating player to face mouse position
            float angleDiff = rotAngle - transform.eulerAngles.z;
            //making it so rotation will always be the shortest way around
            if (angleDiff > 180) {
                angleDiff -= 360;
            }
            else if (angleDiff < -180) {
                angleDiff += 360;
            }
            //Debug.Log("Angle Difference: " + angleDiff);

            //Rotating player towards mouse
            //if (angleDiff >= 10 || angleDiff <= -10) {
            transform.Rotate(0, 0, angleDiff / turnRatio);
            //}
            //else {
            //    transform.localEulerAngles = new Vector3(0, 0, rotAngle);
            //}

            //making player 'follow' mouse

            //Vector2 moveForce = new Vector2();
            //moveForce.y = mouseVec.sqrMagnitude / 10;

            //making it so movement speed changes depending on how far away mouse is
            //Debug.Log("Mouse Distance(sq): " + mouseVec.sqrMagnitude);

            Debug.Log("Player Speed: " + rBody.velocity.sqrMagnitude);
            //preventing player from moving before facing towards the mouse if they are standing still
            if (rBody.velocity.sqrMagnitude < 1) {//if player is not moving or barely moving
                if (angleDiff >= -22.5 && angleDiff <= 22.5) {//if player is facing towards mouse already
                    //Debug.Log("In Range");
                    moveTowardsMouse(mouseVec.sqrMagnitude);
                }
                //else
                    //Debug.Log("OUT of Range");
            }
            else {//player is already moving
                moveTowardsMouse(mouseVec.sqrMagnitude);
            }
        }

    }

    void moveTowardsMouse(float mouseDistance)
    {
        if (mouseDistance > minPointerDistance) {//if mouse is too close, movement stops
            float moveMagnitude = mouseDistance / 10;
            moveMagnitude = moveMagnitude > 100 ? moveMagnitude = 100 : moveMagnitude;//capping max move multiplier

            rBody.AddRelativeForce(new Vector2(0, moveSpeed), ForceMode2D.Force);
        }
    }

    Vector3 getMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
