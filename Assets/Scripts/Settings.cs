using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject[] panelsToHide;//менюшки в настройках
    [SerializeField] private GameObject settingPanel;//панель настроек

    public void Show()
    {
        settingPanel.SetActive(!settingPanel.active);
        if (!settingPanel.active)
        {
            foreach (var pan in panelsToHide)
            {
                pan.SetActive(false);//закрываем все окна, если скрываем натройки
            }
        }
    }
    
    public void Hide()
    {
        settingPanel.SetActive(false);
        for(int i = 0; i < panelsToHide.Length; ++i)
            panelsToHide[i].SetActive(false);
        
    }
    
}
