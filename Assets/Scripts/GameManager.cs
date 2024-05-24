using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float timer = 0f;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            Debug.Log(timer);
            yield return new WaitForSeconds(1f);
            timer++;            
        }        
    }
}
