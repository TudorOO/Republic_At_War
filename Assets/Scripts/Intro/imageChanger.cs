using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageChanger : MonoBehaviour
{
    public int imageID;
    private Image image;

    [SerializeField]
    private Sprite sprite_1;
    [SerializeField]
    private Sprite sprite_2;
    [SerializeField]
    private Sprite sprite_3;
    [SerializeField]
    private Sprite sprite_4;
    [SerializeField]
    private Sprite sprite_5;
    [SerializeField]
    private Sprite sprite_6;
    [SerializeField]
    private Sprite sprite_7;
    [SerializeField]
    private Sprite sprite_8;
    [SerializeField]
    private Sprite sprite_9;
    [SerializeField]
    private Sprite sprite_10;
    [SerializeField]
    private Sprite sprite_11;
    [SerializeField]
    private Sprite sprite_12;
    [SerializeField]
    private Sprite sprite_13;
    [SerializeField]
    private Sprite sprite_14;
    [SerializeField]
    private Sprite sprite_15;
    [SerializeField]
    private Sprite sprite_16;

    void Start(){
        imageID = 0;
        image = GetComponent<Image>();
    }

    void Update(){
        switch (imageID){
            case 0:
                image.sprite = sprite_1;
                break;
            case 1:
                image.sprite = sprite_2;
                break;
            case 2:
                image.sprite = sprite_3;
                break;
            case 3:
                image.sprite = sprite_4;
                break;
            case 4:
                image.sprite = sprite_5;
                break;
            case 5:
                image.sprite = sprite_6;
                break;
            case 6:
                image.sprite = sprite_7;
                break;
            case 7:
                image.sprite = sprite_8;
                break;
            case 8:
                image.sprite = sprite_9;
                break;
            case 9:
                image.sprite = sprite_10;
                break;
            case 10:
                image.sprite = sprite_11;
                break;
            case 11:
                image.sprite = sprite_12;
                break;
            case 12:
                image.sprite = sprite_13;
                break;
            case 13:
                image.sprite = sprite_14;
                break;
            case 14:
                image.sprite = sprite_15;
                break;
            case 15:
                image.sprite = sprite_16;
                break;
        }
    }
}
