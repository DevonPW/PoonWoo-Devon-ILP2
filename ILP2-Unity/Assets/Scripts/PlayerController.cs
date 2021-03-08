using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody2D rBody;

    [SerializeField]
    float moveSpeed = 80;

    //[SerializeField]
    //float startSpeed = 1;

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
        Debug.Log("Player Speed: " + rBody.velocity.sqrMagnitude);
    }

    void FixedUpdate()
    {
        Move();

        animator.SetFloat("Speed", rBody.velocity.magnitude);
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

            //preventing player from moving before facing towards the mouse if they are standing still
            if (rBody.velocity.sqrMagnitude < 10) {//if player is not moving or barely moving
                if (angleDiff >= -30 && angleDiff <= 30) {//if player is facing towards mouse already
                    //Debug.Log("In Range");
                    moveTowardsMouse(mouseVec.sqrMagnitude);
                }
                else {
                    //Debug.Log("OUT of Range");
                }
            }
            else {//player is already moving
                //if (angleDiff >= -30 && angleDiff <= 30)
                    moveTowardsMouse(mouseVec.sqrMagnitude);
            }
        }

    }

    void moveTowardsMouse(float mouseDistance)
    {
        if (mouseDistance > minPointerDistance) {//if mouse is too close, movement stops
            float moveMagnitude = mouseDistance / 5;
            moveMagnitude = moveMagnitude > 2 ? moveMagnitude = 2 : moveMagnitude;//capping max move multiplier
            //Debug.Log("Magnitude: " + moveMagnitude);
            rBody.AddRelativeForce(new Vector2(0, moveSpeed * moveMagnitude), ForceMode2D.Force);
        }
    }

    /*void startTowardsMouse(float mouseDistance)
    {
        if (mouseDistance > minPointerDistance) {//if mouse is too close, movement stops

            rBody.AddRelativeForce(new Vector2(0, startSpeed), ForceMode2D.Impulse);
            //rBody.velocity = new Vector2(0, moveSpeed);
        }
    }*/

    Vector3 getMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void GameOver()
    {
        SceneManager.LoadScene(2);
    }

}
