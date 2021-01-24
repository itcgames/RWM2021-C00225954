using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimizebuttonScript : MonoBehaviour
{
    public GameObject Shop;
    private bool shopActive = true;
    Vector3 positionWhenMenuOpen;
    public Vector2 whenShopMinimizedPosition;
    public bool moveButtonWhenMenuIsHidden;
    Vector2 moveShopBy;
    Vector2 shopPos;
    void Start()
    {
        moveShopBy = new Vector2(0, 500);
        whenShopMinimizedPosition.y += 768;
        shopPos = Shop.transform.position;
    }

    /// <summary>
    /// Move the position of all the children of the shop canvas. we can't just disable the elements of the shop due to the shop needing to update changes so this is the most elegant solution.
    /// </summary>
    public void ToggleMenuOnClick()
    {
       
        if (shopActive)
        {
            positionWhenMenuOpen = this.transform.position;
            shopActive = false;
           
            if (moveButtonWhenMenuIsHidden)
            {
                transform.position = whenShopMinimizedPosition;
            }
            for (int i = 0; i < Shop.transform.childCount; i++)
            {
                Vector2 ah = Shop.transform.GetChild(i).transform.position;
                Shop.transform.GetChild(i).transform.position = ah + moveShopBy;
            }       
            
        }
        else
        {
            shopActive = true;
           
            if (moveButtonWhenMenuIsHidden)
            {
                transform.position = positionWhenMenuOpen;
               
            }
            for (int i = 0; i < Shop.transform.childCount; i++)
            {
                Vector2 ah = Shop.transform.GetChild(i).transform.position;
                Shop.transform.GetChild(i).transform.position = ah - moveShopBy;
            }
        }
       
    }
}
