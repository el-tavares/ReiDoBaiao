using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Sprite[] endSprites;
    [SerializeField] private Image endImage;

    private bool bIsBadEnd;

    private void Start()
    {
        if (endImage != null && endSprites != null)
        {
            if (!bIsBadEnd) { endImage.sprite = endSprites[0]; }
            else { endImage.sprite = endSprites[1]; }
        }
    }

    public void SetBadEnd(bool isBadEnd) { bIsBadEnd = isBadEnd; }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
