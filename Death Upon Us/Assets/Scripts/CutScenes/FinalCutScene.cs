using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static utils.Configs;

public class FinalCutScene : MonoBehaviour
{
    public GameObject rotationCenter;
    private float cameraHeight;

    public void Start() {
        cameraHeight = 20f;
        //transform.position = rotationCenter.transform.position + transform.up * cameraHeight;
    }

    public void Update() {
        transform.LookAt(rotationCenter.transform);
        //transform.Rotate(0, CutSceneSpeed, 0);
        transform.RotateAround(rotationCenter.transform.position, Vector3.up, CutSceneSpeed * Time.deltaTime);
    }
}