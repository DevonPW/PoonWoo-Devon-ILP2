using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    GameManager manager;

    [SerializeField]
    int numHearts = 3;

    [SerializeField]
    Collider2D hitbox;

    float hitTime;

    [SerializeField]
    float invincibleTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitbox.enabled == false) {
            InvincTimeout();
        }
    }

    void FixedUpdate()
    {

    }

    void InvincTimeout()
    {
        if (Time.time- hitTime >= invincibleTime) {//reactivate hitbox
            hitbox.enabled = true;
        }
    }

    void WasHit()
    {
        manager.SendMessage("playerHit");

        //deactivate hitbox
        hitbox.enabled = false;

        hitTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            WasHit();
        }
    }

}
