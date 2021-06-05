using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static utils.Configs;

public class CutSceneTrigger : MonoBehaviour
{
    public GameObject cutSceneCamera;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) {
            cutSceneCamera.SetActive(true);
        }
    }
}

