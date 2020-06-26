using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject handClickPanel;
    
    public void showShop()
    {
        
        shopPanel.SetActive(!shopPanel.active);
        handClickPanel.SetActive(false);
        
    }

    public void showClick()
    {
        
        handClickPanel.SetActive(!handClickPanel.active);
    }
}
