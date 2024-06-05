using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("End menu only")]
    [SerializeField] private Sprite[] endSprites;
    [SerializeField] private Image endImage;

    public static bool bIsBadEnd;

    private void Start()
    {
        if (endImage != null && endSprites != null)
        {
            if (!bIsBadEnd) { endImage.sprite = endSprites[0]; }
            else { endImage.sprite = endSprites[1]; }
        }
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
