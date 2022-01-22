using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunTypeUI : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    public Sprite[] sprites;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchType(int i)
    {
        image.sprite = sprites[i];
    }
}

