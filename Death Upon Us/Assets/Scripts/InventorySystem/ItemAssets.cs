using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite medKitSprite;
    public Sprite arrowSprite;
    public Sprite bowSprite;
    public Sprite knifeSprite;
    public Sprite blueMonsterDropSprite;
    public Sprite orangeMonsterDropSprite;
    public Sprite purpleMonsterDropSprite;

}
