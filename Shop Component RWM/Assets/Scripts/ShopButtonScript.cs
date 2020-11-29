using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopButtonScript : MonoBehaviour
{
    public  GameObject shop;
    GameObject item;
    int itemCost;
    public Sprite itemSprite;
    int playerMoney;
    ColorBlock cb;

    public Color cantAffordColor;
    public Color canAffordColor;
    Color defaultColor;
    
    // Start is called before the first frame update
    void Start()
    {
       
        cb = GetComponent<Button>().colors;
        defaultColor = cb.normalColor;
        

    }

   void Update()
    {
        ChangeItemColor();
    }
    public void SetItem(GameObject t_item, Vector2 t_buttonSize)
    {
        item = t_item;
        itemCost = item.GetComponent<ItemObjectScript>().GetPriceOfItem();
        itemSprite = item.GetComponent<ItemObjectScript>().GetItemSpriteFromScript();
        InitButton(t_buttonSize);
    }


    void InitButton(Vector2 t_buttonSize)
    {
        GetComponent<Image>().sprite = itemSprite;
        RectTransform panelRectT = GetComponent<RectTransform>();
        panelRectT.sizeDelta = t_buttonSize;

    }

    public GameObject GetItem()
    {
        return item;
    }

    public int GetItemPrice()
    {
        return itemCost;
    }

    public void ChangeItemColor()
    {
        if (GetComponentInParent<ShopMenuController>().CanAfford(itemCost))
        {
            cb.normalColor = canAffordColor;
        }
        else if (!GetComponentInParent<ShopMenuController>().CanAfford(itemCost))
        {
            cb.normalColor = cantAffordColor;
        }
        GetComponent<Button>().colors = cb;
    }

    public bool CanPlayerBuyItem()
    {
        if (GetComponentInParent<ShopMenuController>().CanAfford(itemCost))
        {
            ChangeItemColor();
            return true;
        }
        ChangeItemColor();
        return false;
    }

    public void OnItemClicked()
    {
        if (CanPlayerBuyItem())
        {
            GetComponentInParent<ShopMenuController>().BuyItem(item, itemCost);
            print("Can Afford");
            
        }
        else
        {
            print(GetComponentInParent<ShopMenuController>().GetPlayerMoney());
            print("Can't Afford");
        }
       
    }
}
