using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    float startTime;
    float currentTime;

    [SerializeField]
    Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time - startTime;
        //updating timer display
        timerText.text = currentTime.ToString("0.0");
    }
}
