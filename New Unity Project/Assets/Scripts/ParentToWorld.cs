using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentToWorld : MonoBehaviour
{
    private Transform world;
    // Start is called before the first frame update
    void Start()
    {
        world = (GameObject.Find("World")).transform;
        this.transform.parent = world;
    }
}
