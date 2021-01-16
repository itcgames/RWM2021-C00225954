using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectScript : MonoBehaviour
{
    public int priceOfItem;
    public Sprite itemSprite; // if you need to change item sprite
    public string ItemName;
    public int coolDownAfterPurchase;
   
    public int actionCoolDownTimer;
    bool isActive;
    int currentActionCoolDownCount;

    bool itemBought;
    public int itemID; // mainly used to just scale the item correctly due to varying sprite sizes
  
    void Start()
    {
        if (itemSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = itemSprite;
           
        }
     
        isActive = false;
        itemBought = true;

        currentActionCoolDownCount = actionCoolDownTimer;
     
    }

    public Sprite GetItemSpriteFromScript()
    {
        if (itemSprite != null)
        {
            return itemSprite;
        }
        else
        {
            return GetComponent<SpriteRenderer>().sprite;
        }
    }

    public string GetItemName() { return ItemName; }

    public int GetCoolDownTime() { return coolDownAfterPurchase; }

    public void setBought(bool t_isBought) { itemBought = t_isBought; }

    public void setPosition(Vector2 t_pos) { transform.position = t_pos; }

    public int GetPriceOfItem() { return priceOfItem; }


}
