using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClass : MonoBehaviour
{
    private Text buttonText;

    private void Start()
    {
        buttonText = GetComponentInChildren<Text>();
    }
    public void changeText(string text)
    {
        buttonText.text = text;
    }
}
