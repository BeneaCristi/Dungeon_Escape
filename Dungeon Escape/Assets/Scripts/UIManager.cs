using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text playerGemCountText;
    public Image selectionImg;
    public Text gemCount;

    public Image[] healthBars;


    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UIManager null");
            }
            return _instance;
        }
    }


    public void UpdateShopSelection(double yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, (float)yPos);
    }


    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";

    }

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateGemCount(int count)
    {
        gemCount.text = "" + count;
    }

    public void UpdateLives(int lives)
    {
        for(int i=0; i <= lives; i++)
        {
            if(i==lives)
            {
                healthBars[i].enabled = false;
            }
        }
       
    }

}
