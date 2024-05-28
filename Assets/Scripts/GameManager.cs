using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int emoCount;

    private void Start()
    {
        emoCount = GameObject.FindGameObjectsWithTag("Emo").Length;
        Debug.Log($"Total de emos: {emoCount}");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.G)) { EndGame(false); }
    }

    private void OnEnable()
    {
        TimerBehavior.OnTimeEnded += EndGame;
        EmoBehavior.OnEmoExplosion += DecreaseEmoCount;
    }

    private void OnDisable()
    {
        TimerBehavior.OnTimeEnded -= EndGame;
        EmoBehavior.OnEmoExplosion -= DecreaseEmoCount;
    }

    private void EndGame(bool badEnd)
    {
        MenuManager.bIsBadEnd = badEnd;
        MenuManager.LoadScene("EndMenu");
    }

    private void DecreaseEmoCount()
    {
        emoCount--;
        if (emoCount == 0 ) { Invoke("GoodEnd", 1.5f); }
    }

    private void GoodEnd() { EndGame(false); }
}
