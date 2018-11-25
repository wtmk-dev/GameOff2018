using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shootable : MonoBehaviour {

    public Transform getTransform(){
        return GetComponent<Transform>();
    }
}
