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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit == true) {
            InvincTimeout();
        }
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
        gameObject.layer = LayerMask.NameToLayer("Enemies");

        hitTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            WasHit();
        }
    }
}
