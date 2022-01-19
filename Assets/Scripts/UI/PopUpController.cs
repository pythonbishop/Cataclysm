using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpController : MonoBehaviour
{
    // Start is called before the first frame update
    float startTimer;
    float timeLeft;
    public GameObject popUpMenu;
    public TextMeshProUGUI popUpText;
    bool windowOpen;
    bool startWindow;
    void Start()
    {
        startTimer = 5;
    }
    void Update()
    {
        startTimer -= Time.deltaTime;
        if (startTimer < 0 && !startWindow)
        {
            open(5, "Make your way to the portal to fight the boss.");
            startWindow = true;
        }

        timeLeft -= Time.deltaTime;

        if (windowOpen && !popUpMenu.activeSelf)
        {
            popUpMenu.SetActive(true);
        }

        if (timeLeft < 0 && windowOpen)
        {
            windowOpen = false;
            popUpMenu.SetActive(false);
        }
    }

    public void open(float duration, string text)
    {
        timeLeft = duration;
        windowOpen = true;
        popUpText.text = text;
    }
}
