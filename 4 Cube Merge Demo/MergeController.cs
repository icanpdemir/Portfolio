using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeController : MonoBehaviour
{
    [SerializeField] GameObject[] cubePrefabs;
    RaycastHit nesne;

    
    public bool mergeCubes(Transform transformMerge, int cubeIndex){
        if( cubePrefabs.Length -1 != cubeIndex){
            Instantiate(cubePrefabs[cubeIndex+1], transformMerge.position, transformMerge.rotation);
            return true;
        }else {
            return false;
        }
            
    }

   
}
