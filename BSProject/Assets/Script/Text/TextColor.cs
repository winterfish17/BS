using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColor : MonoBehaviour
{
    public Image image;
    Color imageColor;
    private void Start()
    {
        imageColor = image.color;
    }
    public void ColorEnter()
    {
        image.color = imageColor - new Color(0, 0, 0, 0.5f);
    }
    public void ColorExit()
    {
        image.color = imageColor;
    }
}