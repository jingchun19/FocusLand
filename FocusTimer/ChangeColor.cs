using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Image img;


    public void ChangeToRed()
    {
        img.color = Color.red;
    }

    public void ChangeToGreen()
    {
        img.color = Color.green;
    }
}
