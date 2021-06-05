using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static utils.Configs;

public class FinalCutScene : MonoBehaviour
{
    public GameObject rotationCenter;

    public void Start() {
        gameObject.SetActive(false);
    }

    public void Update() {
        transform.LookAt(rotationCenter.transform);
        transform.RotateAround(rotationCenter.transform.position, Vector3.up, CutSceneSpeed * Time.deltaTime);
    }
}