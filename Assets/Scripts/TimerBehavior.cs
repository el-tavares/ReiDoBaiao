using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehavior : MonoBehaviour
{
    [SerializeField] private Sprite[] timerSprites = new Sprite[6];
    [SerializeField] private float startTimer = 600;

    private float currentTimer;

    private void Start()
    {
        // Inicializar temporizador
        currentTimer = startTimer;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (currentTimer >= 0f)
        {
            // Atualizar o sprite conforme o percentual de tempo
            Sprite sprite = GetSpriteByTime(currentTimer / startTimer);
            if (sprite != null) { GetComponent<Image>().sprite = sprite; }

            yield return new WaitForSeconds(1f);
            currentTimer--;
        }

        Debug.Log("CABOOOU O TEMPO!");
    }

    private Sprite GetSpriteByTime(float percentage)
    {
        if (percentage == 1.0f) { return timerSprites[0]; }
        else if (percentage == 0.8f) { return timerSprites[1]; }
        else if (percentage == 0.6f) { return timerSprites[2]; }
        else if (percentage == 0.4f) { return timerSprites[3]; }
        else if (percentage == 0.2f) { return timerSprites[4]; }
        else if (percentage == 0f) { return timerSprites[5]; }
        else { return null; }
    }
}
