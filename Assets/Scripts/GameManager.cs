using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.K)) { EndGame(false); }
    }

    private void OnEnable()
    {
        TimerBehavior.OnTimeEnded += EndGame;
    }

    private void OnDisable()
    {
        TimerBehavior.OnTimeEnded -= EndGame;
    }

    private void EndGame(bool badEnd)
    {
        MenuManager.bIsBadEnd = badEnd;
        MenuManager.LoadScene("EndMenu");
    }
}
