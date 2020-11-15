using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public class ShopMenuController : MonoBehaviour
{

    [Tooltip("The background image for shop menu(.PNG)")]
    public Sprite background;

    [Tooltip("Width attribute to scale background image by (float)")]
    [Range(0, 1000)]
    public float Width;

    [Tooltip("Height attribute to scale background image by (float)")]
    [Range(0, 1000)]
    public float Height;

    [Tooltip("Scale the size of the background image by this amount. leave as 1 for no scaling")]
    [Range(0, 10)]
    public float backgroundScale;

    [Tooltip("Scale all variables to inputted backgroundScale factor")]
    public bool scaleAllToBackground;

    [Tooltip("Position of background image on canvas. Y is flipped(Vector 2)")]
    public Vector2 backgroundPosition;

    [Tooltip("Menu Name (String)")]
    public String Title;
    [Tooltip("Position of title, offset from the position of top left corner of the background sprite")]
    public Vector2 TitlePosition;


    public Font font;
    [Range(1, 50)]
    public float fontSize;
  
    public Color fontColor;
   
    private float BGWidth;
    private float BGHeight;

   


    // Start is called before the first frame update
    void Start()
    {

        InitBackgroundImage();
        InitTitle();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitBackgroundImage()
    {
        backgroundPosition.y *= -1;
        BGWidth = background.textureRect.width;
        BGHeight = background.textureRect.height;

        Width *= backgroundScale;
        Height *= backgroundScale;
        Image backgroundImage = GetComponentInChildren<Image>();
        backgroundImage.sprite = background;
        backgroundImage.rectTransform.anchoredPosition = backgroundPosition;
        backgroundImage.rectTransform.sizeDelta = new Vector2(Width, Height);

       
    }

    void InitTitle()
    {
        
        TitlePosition.y *= -1;
        Text TitleOb = GetComponentInChildren<Text>();
      
        if (font != null)
        {
            TitleOb.font = font;
        }
        if (scaleAllToBackground)
        {
            TitlePosition *= backgroundScale;
            fontSize *= backgroundScale;
        }
        TitleOb.text = Title;
        TitleOb.color = fontColor;
        TitleOb.fontSize = (int)fontSize;
        TitleOb.rectTransform.sizeDelta = new Vector2(Width, Height);
        TitlePosition += backgroundPosition;
        TitleOb.rectTransform.anchoredPosition = TitlePosition;
    }

   

}
