using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should use composition, have generic character controller

public class EnemyController : MonoBehaviour
{
    GameObject player;
    GameManager gm;

    [SerializeField]
    Rigidbody2D rBody;

    [SerializeField]
    float moveSpeed = 80;

    //[SerializeField]
    //float startSpeed = 1;

    [SerializeField]
    float turnRatio = 10;

    void FixedUpdate()
    {
        Move();
    }

    public void setRefs(GameObject playerRef, GameManager gmRef)
    {
        player = playerRef;
        gm = gmRef;
    }

    void Move()
    {
        float rotAngle = getPlayerAngle();

        //calculating difference between current rotation and desired rotation
        float angleDiff = rotAngle - transform.eulerAngles.z;
        //making it so rotation will always be the shortest way around
        if (angleDiff > 180) {
            angleDiff -= 360;
        }
        else if (angleDiff < -180) {
            angleDiff += 360;
        }
        //Debug.Log("Angle Difference: " + angleDiff);

        //Rotating enemy towards player
        transform.Rotate(0, 0, angleDiff / turnRatio);

        //making enemy 'follow' player
        if (rBody.velocity.sqrMagnitude < 10) {//if enemy is not moving or barely moving
            if (angleDiff >= -30 && angleDiff <= 30) {//if enemy is facing towards mouse already
                //Debug.Log("In Range");
                moveTowardsPlayer();
            }
            else {
                //Debug.Log("OUT of Range");
            }
        }
        else {//enemy is already moving
              //if (angleDiff >= -30 && angleDiff <= 30)
            moveTowardsPlayer();
        }

    }

    void moveTowardsPlayer()
    {
        rBody.AddRelativeForce(new Vector2(0, moveSpeed), ForceMode2D.Force);
    }

    float getPlayerAngle()
    {
        //getting vector to player
        Vector3 playerVec = new Vector3();
        playerVec = player.transform.position - transform.position;

        //getting angle between player and enemy
        float playerAngle = (Mathf.Rad2Deg * Mathf.Atan2(playerVec.y, playerVec.x) - 90);
        playerAngle = playerAngle < 0 ? playerAngle + 360 : playerAngle; //keep rotation angle measured as positive from y axis

        return playerAngle;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack") {
            Destroy(gameObject);//enemy is kill
            //no object pooler for now...

            gm.UpdateScore();//will look into event system...
        }
    }

   /* void OnCollisionEnter2D(Collision2D collision)
    {
        if (gm.isDead == true && collision.gameObject.tag == "Player") {
            Destroy(gameObject);//enemy is kill
        }
    }*/

    void OnCollisionStay2D(Collision2D collision)
    {
        if (gm.isDead == true && collision.gameObject.tag == "Player") {
            Destroy(gameObject);//enemy is kill
        }
    }
}
