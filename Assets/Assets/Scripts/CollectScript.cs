using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    void OnTriggerEnter(Collider x){
        if(x.gameObject.tag=="pickup"){
            x.gameObject.SetActive(false);
        }
    }
}
