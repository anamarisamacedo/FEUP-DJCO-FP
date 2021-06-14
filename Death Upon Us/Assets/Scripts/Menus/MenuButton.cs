using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static utils.Configs;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("hello");
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/MenuHover");
    }

    public void OnPointerClick(PointerEventData eventData){
        Debug.Log("hello2");
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/MenuSelect");
    }
}
