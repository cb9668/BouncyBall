﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
    public GameObject startButton;
    public GameObject helpButton;
    public GameObject creditButton;

    public GameObject backButton;
    public GameObject mainText;

    
    // Use this for initialization
    void Start () {

        if (startButton)
        {
            startButton.SetActive(true);
            Time.timeScale = 1;
        }
        if (helpButton)
            helpButton.SetActive(true);
        if(mainText)
            mainText.SetActive(false);
        if (backButton)
            backButton.SetActive(false);

      
    }
	

    public void Help()
    {
        startButton.SetActive(false);
        helpButton.SetActive(false);

        mainText.SetActive(true);
        backButton.SetActive(true);

        mainText.GetComponent<Text>().text = "Click on the ball to make it bounce and avoid obstacles falling from sky. Do not drop the ball!!";
    }

    public void Credit()
    {
        startButton.SetActive(false);
        helpButton.SetActive(false);
        creditButton.SetActive(false);


        mainText.SetActive(true);
        backButton.SetActive(true);

        mainText.GetComponent<Text>().text = " Cassie Bian.  VGP202 ";
    }

    public void Back()
    {
        startButton.SetActive(true);
        helpButton.SetActive(true);
 
        mainText.SetActive(false);
        backButton.SetActive(false);
    }
}
