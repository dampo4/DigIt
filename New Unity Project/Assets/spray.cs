using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class spray : MonoBehaviour
{
    public GameObject particle;
    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void Selected()
    {
        particle.SetActive(true);
        Debug.Log("hello");
    }
    public void NotSelected()
    {
        particle.SetActive(false);
        Debug.Log("hi");
    }
}
