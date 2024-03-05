using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutFruitParticle : MonoBehaviour
{
    [SerializeField] colorPair[] fruitParticleColors;
    [SerializeField] float[] fruitParticleSizeScales = { };
    [SerializeField] ParticleSystem psObject;

    Dictionary<string, Gradient> colorPairs;

    public static CutFruitParticle Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        colorPairs = new Dictionary<string, Gradient>();

        foreach (var item in fruitParticleColors)
        {
            colorPairs.Add(item.key, item.value);
        };
    }


    public void createCutParticle(string ballType, int ballValue, Transform pos)
    {
        ParticleSystem pS = Instantiate(psObject, pos.position, pos.rotation, pos.parent);
        var main = pS.main;
        main.startColor = colorPairs[ballType];
        main.startSizeMultiplier = main.startSizeMultiplier * fruitParticleSizeScales[ballValue];
        pS.Play();
    }
}

[Serializable]
public class colorPair
{
    public string key;
    public Gradient value;
}

