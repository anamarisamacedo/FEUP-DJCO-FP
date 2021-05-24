using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{

    public IEnumerator TakeDamage()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        canvGroup.alpha += (float)0.10;
        yield return new WaitForSeconds(4);
        canvGroup.alpha -= (float)0.10;
    }

    public void Heal()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        canvGroup.alpha -= (float)0.10;
    }

    public void RemoveBlood()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        canvGroup.alpha = (float)0;
    }

}
