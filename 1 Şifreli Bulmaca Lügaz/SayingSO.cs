using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SayingSO", fileName = " Saying")]
public class SayingSO : ScriptableObject
{
    [TextArea(5,1000)]
    [SerializeField] String saying;
    [SerializeField] String author;

    public String getSaying()
    {
        if (saying != null)
            return saying;
        else
            return "null";
    }

    public String getAuthorName()
    {
        if (saying != null)
            return author;
        else
            return "null";
    }
}
