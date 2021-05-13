using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReset : MonoBehaviour
{
    public GameObject right;
    public GameObject left;
    public GameObject Mright;
    public GameObject Mleft;
    public bool wasDeac;
    public Animator Ranimator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (right == null)
        {
            right = GameObject.FindWithTag("right");
        }
        if (left == null)
        {
            left = GameObject.FindWithTag("left");
        }
        if (!Mright.activeSelf)
        {
            wasDeac = true;
        }
        if (Mright.activeSelf && right != null && wasDeac)
        {
            /*Ranimator = right.GetComponent<Hand>().animator;
            Ranimator.SetFloat("Index", 0f);
            Ranimator.SetFloat("ThreeFingers", 0f);
            Ranimator.SetFloat("Thumb", 0f);
            Ranimator.Rebind();
            Ranimator.Update(0f);
            wasDeac = false;
            Debug.Log("sasa");
            //right.GetComponent<Hand>().threeFingerValue = 0;
            //right.GetComponent<Hand>().thumbValue = 0;
            //right.GetComponent<Hand>().indexValue = 0;*/
        }
    }
}
