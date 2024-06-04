using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBehavior : MonoBehaviour
{
    [SerializeField] private Sprite[] animationSprites;

    private void OnEnable() { Debug.Log("OI");  StartCoroutine(PlayAnimation()); }

    private void OnDisable() { StopAllCoroutines(); }

    public IEnumerator PlayAnimation()
    {
        foreach (var sprite in animationSprites)
        {
            yield return new WaitForSeconds(.1f);

            this.GetComponent<Image>().sprite = sprite;
        }

        this.gameObject.SetActive(false);
    }
}
