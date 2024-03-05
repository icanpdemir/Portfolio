using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CipherCell : MonoBehaviour
{
    [Header(" Object References ")]
    [SerializeField] TMP_Text cipherCellTxt;

    public void setCipherCellText(string txt){
        cipherCellTxt.text = txt;
    }
}
