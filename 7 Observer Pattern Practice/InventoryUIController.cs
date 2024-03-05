using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] Text seedCounterTxt;

    int seedCount = 0;

    private void Start()
    {
        seedCounterTxt.text = "Seed: " + seedCount;

    }

    private void OnEnable()
    {
        Seed.onSeedCollected += increaseSeedCount;
    }

    private void OnDisable()
    {
        Seed.onSeedCollected -= increaseSeedCount;
    }

    public void increaseSeedCount()
    {
        seedCount++;
        seedCounterTxt.text = "Seed: " + seedCount;
    }
}
