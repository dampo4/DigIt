using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class spray : MonoBehaviour
{
    public GameObject particle;
    public GameObject showerHand;
    public GameObject normalHand;
    public GameObject player;
    public GameObject model;
    // Update is called once per frame

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
        model.GetComponent<Renderer>().enabled = false;
        player.GetComponent<PlayerControls>().DisableRight();
        showerHand.SetActive(true);
    }
    /*public void PutDown()
    {
        model.SetActive(true);
        normalHand.SetActive(true);
        showerHand.SetActive(false);
    }*/
}
