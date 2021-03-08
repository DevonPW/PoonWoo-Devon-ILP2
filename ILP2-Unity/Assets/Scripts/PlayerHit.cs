using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameManager manager;

    [SerializeField]
    Collider2D hitbox;

    [SerializeField]
    float invincibleTime;

    float hitTime;

    bool isHit = false;

    // Update is called once per frame
    void Update()
    {
        if (isHit == true) {
            if (manager.isDead == false) {
                InvincTimeout();
            }
            else {
                isHit = false;
                gameObject.layer = LayerMask.NameToLayer("Default");
            }

        }
        Debug.Log(gameObject.layer);
    }

    void InvincTimeout()
    {
        if (Time.time - hitTime >= invincibleTime) {//reactivate hitbox
            //hitbox.enabled = true;
            isHit = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
            animator.SetBool("IsHit", false);
        }
    }

    void WasHit()
    {
        manager.SendMessage("PlayerHit");

        animator.SetBool("IsHit", true);

        //deactivate hitbox
        //hitbox.enabled = false;

        isHit = true;

        //change layer
        gameObject.layer = LayerMask.NameToLayer("PlayerHit");

        hitTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            if (manager.isDead == false) {
                WasHit();
            }
        }
    }
}
