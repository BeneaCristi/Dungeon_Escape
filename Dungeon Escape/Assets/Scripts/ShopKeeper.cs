using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopKeeper : MonoBehaviour
{
    public GameObject panel;
    private Player player;


    public int itemSelected;
    public int itemCost;

   

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            if(player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }

            panel.SetActive(true);
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            panel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        Debug.Log("SelectedItem()" + item);

        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(103.4);
                itemSelected = 0;
                itemCost = 200;
                break;

            case 1:
                UIManager.Instance.UpdateShopSelection(1.9);
                itemSelected = 1;
                itemCost = 400;
                break;

            case 2:
                UIManager.Instance.UpdateShopSelection(-101);
                itemSelected = 2;
                itemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {

        if (player.diamonds >= itemCost)
        {
            if(itemSelected == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            Debug.Log("bought item :" + itemSelected);
            player.diamonds -= itemCost;
            UIManager.Instance.OpenShop(player.diamonds);
            UIManager.Instance.UpdateGemCount(player.diamonds);
        }
        else if(player.diamonds < itemCost)
        {
            panel.SetActive(false);
        }
       
    }
}
