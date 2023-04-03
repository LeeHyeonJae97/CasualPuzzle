using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    private const float WIDTH = 9;
    private const float HEIGHT = 16;

    private void Start()
    {
        var camera = GetComponent<Camera>();

        var rect = camera.rect;

        float heightScale = ((float)Screen.width / Screen.height) / (WIDTH / HEIGHT);
        float widthScale = 1f / heightScale;

        if (heightScale < 1)
        {
            rect.height = heightScale;
            rect.y = (1f - heightScale) / 2f;
        }
        else
        {
            rect.width = widthScale;
            rect.x = (1f - widthScale) / 2f;
        }

        camera.rect = rect;
    }

    private void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }
}