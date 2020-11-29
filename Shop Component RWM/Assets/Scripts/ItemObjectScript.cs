using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectScript : MonoBehaviour
{
    public int priceOfItem;
    public Sprite itemSprite; // if you need to change item sprite
    public string ItemName;
    // Start is called before the first frame update
    void Start()
    {
        if (itemSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = itemSprite;
        }
       
    }




    public int GetPriceOfItem()
    {
        return priceOfItem;
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

    public string GetItemName()
    {
        return ItemName;
    }
}
