using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    [SerializeField]
    UITimer timer;

    //array of heart icons
    [SerializeField]
    HeartIconAnimation[] Hearts;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    int score;

    int numHearts;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.maxQueuedFrames = 1;

        numHearts = Hearts.Length - 1;

        score = 0;

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreDisplay();

        if (numHearts >= 0) {
            PlayHeartAnim();
        }
        checkDead();
    }

    void checkDead()
    {
        if (isDead == false && numHearts < 0) {
            isDead = true;
            player.Die();
            timer.playerDied();
        }
    }

    void PlayerHit()
    {
        //decrease hearts by 1
        //Hearts[numHearts].SetActive(false);

        Hearts[numHearts].gameObject.SetActive(false);
        numHearts--;
        
    }

    public void UpdateScore()
    {
        score++;
    }

    void UpdateScoreDisplay()
    {
        //updating timer display
        scoreText.text = "SCORE: " + score.ToString();
    }

    void PlayHeartAnim()
    {
        //Hearts[numHearts].SendMessage("playAnimation");

        /*HeartIconAnimation  heartAnim = new HeartIconAnimation();
        heartAnim = (HeartIconAnimation) Hearts[numHearts].GetComponent("HeartIconAnimation");
        heartAnim.playAnimation();*/

        //Hearts[numHearts].GetComponent<HeartIconAnimation>().playAnimation();

        Hearts[numHearts].playAnimation();
    }
}
