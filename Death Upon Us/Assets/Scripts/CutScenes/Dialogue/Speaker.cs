#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue/New Speaker")]
public class Speaker : ScriptableObject
{
    [SerializeField] private Sprite speakerSprite;

    public Sprite GetSprite()
    {
        return speakerSprite;
    }
}
