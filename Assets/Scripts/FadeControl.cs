using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        //image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime / 2);
        }

        if (image.color.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
