using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParameterManager : MonoBehaviour
{
    [SerializeField] TMP_Text[] parameterInitialTxts;
    [SerializeField] int[] parameterInitialValues = { 50, 50, 50, 50 };
    
    static int[] parameterValues;
    static TMP_Text[] parameterTxts;
    private static ParameterManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        parameterValues = parameterInitialValues;
        parameterTxts = parameterInitialTxts;
    }
    private void Start()
    {
        for (int i = 0; i < parameterInitialTxts.Length; i++)
        {
            parameterInitialTxts[i].text = parameterValues[i].ToString();
        }
    }

    public static void changeParameterValue(int id, int changeValue)
    {
        parameterValues[id] += changeValue;
        parameterTxts[id].text = parameterValues[id].ToString();
    }
}
