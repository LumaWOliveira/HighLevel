using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScene : MonoBehaviour
{

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if(image.color.a < 5)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a+ 1*Time.deltaTime);

        }
   
    }
}
