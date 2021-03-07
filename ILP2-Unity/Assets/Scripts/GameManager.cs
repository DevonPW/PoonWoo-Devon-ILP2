using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //array of heart icons
    [SerializeField]
    HeartIconAnimation[] Hearts;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    int score;

    int numHearts;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.maxQueuedFrames = 1;

        numHearts = Hearts.Length - 1;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreDisplay();

        if (numHearts >= 0) {
            PlayHeartAnim();
        }

        if (numHearts < 0) {
            GameOver();
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

    void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}
