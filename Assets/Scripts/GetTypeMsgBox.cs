using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GetTypeMsgBox : MonoBehaviour
{

    [SerializeField] private Text title, text, btnText;
    
    
    public void showMessage(string title, string text, string btnText)
    {
        this.title.text = title;
        this.text.text = text;
        this.btnText.text = btnText;
    }

    public void close()
    {
        Destroy(gameObject);
    }
}
