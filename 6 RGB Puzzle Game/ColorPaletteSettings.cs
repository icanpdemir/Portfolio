using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPaletteSettings : MonoBehaviour
{
    [SerializeField] Color32[] colors;

    public Color32 getNonActiveColor(){
        if(colors != null)
            return colors[0];
        
        return Color.grey;
    }

    public Color32 getActiveColorOne(){
        if(colors != null)
            return colors[1];
        
        return Color.red;
    }

    public Color32 getActiveColorTwo(){
        if(colors != null)
            return colors[2];
        
        return Color.green;
    }

    public Color32 getActiveColorThree(){
        if(colors != null)
            return colors[3];
        
        return Color.blue;
    }


}
