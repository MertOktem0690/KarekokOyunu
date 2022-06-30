using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CerceveManager : MonoBehaviour
{
    private Image cerceveImage;

    int randomDeger;

    Color color;

    void Start()
    {
        cerceveImage = GetComponent<Image>();
        RengiDegistir();
    }

    void RengiDegistir()
    {
        randomDeger = Random.Range(0, 50);
        if(randomDeger<=10)
        {
            color = Color.magenta;
        }else if(randomDeger<=20)
        {
            color = Color.red;
        } else if(randomDeger<=30)
        {
            color = Color.green;
        } else if(randomDeger<=40)
        {
            color = Color.cyan;
        } else if(randomDeger<=50)
        {
            color = Color.blue;
        }
        if(cerceveImage!=null)
        {
            cerceveImage.color = color;
        }
    }
}
