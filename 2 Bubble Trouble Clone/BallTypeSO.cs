using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BallTypeSO", fileName = " ball")]
public class BallTypeSO : ScriptableObject
{
    [SerializeField] GameObject ballPrefab;

    public GameObject getBall(){
        return ballPrefab;
    }
}
