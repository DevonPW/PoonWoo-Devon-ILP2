using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rBody;

    [SerializeField]
    float moveSpeed = 1;

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
            Vector3 mouseVec = new Vector3();
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
            //angleDiff = Mathf.Abs(angleDiff) > 180 ? angleDiff - 360 : angleDiff;
            Debug.Log("Angle Difference: " + angleDiff);

            if (angleDiff >= 10 || angleDiff <= -10) {
                transform.Rotate(0, 0, angleDiff / 100);
            }
            else {
                transform.localEulerAngles = new Vector3(0, 0, rotAngle);
            }

            //making player 'follow' mouse
            Vector2 moveForce = new Vector2();
            moveForce.y = mouseVec.sqrMagnitude / 10;

            rBody.AddRelativeForce(new Vector2(0, moveSpeed), ForceMode2D.Force);


        }
        if (Input.GetMouseButton(1)) {
            

        }

    }

    Vector3 getMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
