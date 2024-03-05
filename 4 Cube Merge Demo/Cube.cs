using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] int cubeIndex;
    MergeController mergeController;
    int id;
    bool mergeCompleted = false;

    private void Start() {
        mergeController = FindObjectOfType<MergeController>();
        id = GetInstanceID();
    }

    private void OnCollisionEnter(Collision other) {
        if(gameObject.tag == other.gameObject.tag && id < other.gameObject.GetInstanceID()){
            Debug.Log("same tag letss merge");
            mergeCompleted = mergeController.mergeCubes(gameObject.transform, cubeIndex);
            if(mergeCompleted){
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            
        }
    }

    

    
}
