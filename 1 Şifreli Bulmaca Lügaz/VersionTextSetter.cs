using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersionTextSetter : MonoBehaviour
{
    [SerializeField] TMP_Text versionText;

    private void Start() {
        versionText.text = "Version " + Application.version;
    }
}
