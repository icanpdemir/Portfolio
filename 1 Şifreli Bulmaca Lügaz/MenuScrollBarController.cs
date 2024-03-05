using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScrollBarController : MonoBehaviour
{
    [SerializeField] GameObject scrollBarObject;
    [SerializeField] Scrollbar scrollBar;
    [SerializeField] GameObject contentObject;
    [Range(0f, 2f)]
    [SerializeField] float scaleRatioFitToScreen = 1f;

    float scrollPos = 0;
    float[] pos;

    private void Update()
    {
        pos = new float[contentObject.transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollBar.value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollBar.value = Mathf.Lerp(scrollBar.value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                contentObject.transform.GetChild(i).localScale = Vector2.Lerp(contentObject.transform.GetChild(i).localScale, new Vector2(1f, 1f) * scaleRatioFitToScreen, 0.1f);
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        contentObject.transform.GetChild(a).localScale = Vector2.Lerp(contentObject.transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f) * scaleRatioFitToScreen, 0.1f);
                    }
                }
            }
        }
    }

    public void setScaleRatio(float value){
        scaleRatioFitToScreen = value;
    }
}
