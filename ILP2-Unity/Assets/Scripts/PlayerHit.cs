﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField]
    GameManager manager;

    [SerializeField]
    Collider2D hitbox;

    [SerializeField]
    float invincibleTime;

    float hitTime;

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

    void InvincTimeout()
    {
        if (Time.time - hitTime >= invincibleTime) {//reactivate hitbox
            hitbox.enabled = true;
        }
    }

    void WasHit()
    {
        manager.SendMessage("PlayerHit");

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