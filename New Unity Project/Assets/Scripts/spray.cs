using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class spray : MonoBehaviour
{
    public GameObject particle;
    public GameObject normalHand;
    public GameObject showerHand;
    public GameObject shower;
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, -transform.up, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 5))
        {
            if (hit.collider.gameObject.tag == "Clump")
            {
                hit.collider.gameObject.transform.localScale -= new Vector3(0.0004f, 0.0004f, 0.0004f);
            }
        }

    }
    public void Selected()
    {
        particle.SetActive(true);
    }
    public void NotSelected()
    {
        particle.SetActive(false);
    }
    public void PickUp()
    {
        shower.GetComponent<Renderer>().enabled = false;
        normalHand.SetActive(false);
        showerHand.SetActive(true);
    }
    public void PutDown()
    {
        shower.SetActive(true);
        normalHand.SetActive(true);
        showerHand.SetActive(false);
    }
}
