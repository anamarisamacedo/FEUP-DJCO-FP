using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{

    public IEnumerator takeDamage()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        canvGroup.alpha += (float)0.10;
        yield return new WaitForSeconds(4);
        canvGroup.alpha -= (float)0.10;
    }

    public void heal()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        canvGroup.alpha -= (float)0.10;
    }

    public void heal100()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        canvGroup.alpha = (float)0;
    }

}
