using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.Events;
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

    [Tooltip("The background image for item Window(.PNG)")]
    public Sprite gridBackgroundSprite;
    [Tooltip("The border/window for an item being displayed(.PNG)")]
    public Sprite itemBorderBoxSprite;

    public Vector2 purchaseWindowPos;
    public Vector2 purchaseGridBackgroundSize;

    [Range(1, 50)]
    public int windowSlots;
    [Range(10, 200)]
    public float cellsize;
    [Range(1, 10)]
    public int constraintCount;
    [Range(1, 100)]
    public float itemBoxGridOffset;

    [Tooltip("The offset of the item grid from its background")]
    public Vector2Int padding;

    public GameObject[] items;
    public GameObject buttonPrefab;
    
    public Color buttonHoveredColour;



    public GameObject Player;
    int playerMoney;
    // Start is called before the first frame update
    void Start()
    {
        if (scaleAllToBackground)
        {
            TitlePosition *= backgroundScale;
            fontSize *= backgroundScale;
            purchaseWindowPos *= backgroundScale;
            purchaseGridBackgroundSize *= backgroundScale;
            cellsize *= backgroundScale;
            Width *= backgroundScale;
            Height *= backgroundScale;
            itemBoxGridOffset *= backgroundScale;
            padding *= (int)backgroundScale;
            
        }
        playerMoney = Player.GetComponent<ExamplePlayer>().getPlayerMoney();
        InitBackgroundImage();
        InitTitle();

        InitPanel();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitBackgroundImage()
    {
        backgroundPosition.y *= -1;
       
        if (!scaleAllToBackground)
        {
            Width *= backgroundScale;
            Height *= backgroundScale;
        }
       
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
       
        TitleOb.text = Title;
        TitleOb.color = fontColor;
        TitleOb.fontSize = (int)fontSize;
        TitleOb.rectTransform.sizeDelta = new Vector2(Width, Height);
        TitlePosition += backgroundPosition;
        TitleOb.rectTransform.anchoredPosition = TitlePosition;
    }


    void InitPanel()
    {
        GameObject panel = new GameObject();
        panel.name = "Panel Tile Grid";
        panel.AddComponent<CanvasRenderer>();
        panel.AddComponent<RectTransform>();
        panel.AddComponent<Image>();
        panel.AddComponent<GridLayoutGroup>();
        panel.transform.SetParent(transform);

        GridLayoutGroup grid = panel.GetComponent<GridLayoutGroup>();
        // cell size
        // offset
        // type of 

        grid.cellSize = new Vector2(cellsize, cellsize);
        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
        panelRectTransform.anchorMin = new Vector2(0, 1);
        panelRectTransform.anchorMax = new Vector2(0, 1);
        panelRectTransform.pivot = new Vector2(0.0f, 1f);
        panelRectTransform.anchoredPosition = new Vector2(0, 0);


        panel.GetComponent<Image>().sprite = gridBackgroundSprite;
        panel.GetComponent<RectTransform>().sizeDelta = purchaseGridBackgroundSize;
        purchaseWindowPos.y *= -1;
        panelRectTransform.anchoredPosition += purchaseWindowPos;


        grid.SetLayoutHorizontal();
        grid.constraint = GridLayoutGroup.Constraint.Flexible;
        CalculateColumnCount(grid);
        grid.cellSize = new Vector2(cellsize, cellsize);
        grid.padding.left = padding.x;
        grid.padding.top = padding.y ;
        grid.padding.right = padding.x;
        grid.padding.bottom = padding.y ;
        for (int i = 0; i < windowSlots; i++)
        {
            GameObject windowSlot = new GameObject();
           
            
            windowSlot.name = "Window Box " + i;
            windowSlot.AddComponent<Image>();
            windowSlot.GetComponent<Image>().sprite = itemBorderBoxSprite;
            if (i < items.Length)
            {
                GameObject button = Instantiate(buttonPrefab);
                button.GetComponent<ShopButtonScript>().SetItem(items[i], new Vector2(cellsize,cellsize));
                button.transform.SetParent(windowSlot.transform);
            }
            
            windowSlot.transform.SetParent(grid.transform);
        }
    }

    void CalculateColumnCount(GridLayoutGroup g)
    {
        constraintCount = g.constraintCount;
        int numInRow = windowSlots / constraintCount;
        // cellsize = (purchaseGridBackgroundSize.x - 20 - itemBoxGridOffset * ( numInRow )) / numInRow;
        // float boxy = ((cellsize + itemBoxGridOffset) * numInRow) - itemBoxGridOffset;
        g.spacing = new Vector2(itemBoxGridOffset, itemBoxGridOffset);
        // g.constraintCount = constraintCount;

    }


    public bool CanAfford(int t_itemPrice)
    {
        if (playerMoney < t_itemPrice)
        {
            return false;
        }
        else if (playerMoney >= t_itemPrice)
        {

            return true;
        }

        return false; // something weird has happened
    }
   
   public void BuyItem(GameObject t_itemBought, int t_itemPrice)
    {
        // add item to an inventory here not included in component spec
        playerMoney -= t_itemPrice;
        print("Bought a " + t_itemBought.GetComponent<ItemObjectScript>().GetItemName());
    }

    public int GetPlayerMoney()
    {
        return playerMoney;
    }
}
