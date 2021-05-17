using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clean : MonoBehaviour
{
    public GameObject player;
    public void Start()
    {
        player = GameObject.Find("XR Rig");
    }
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
            if (hit.collider.gameObject.tag == "Clump" && hit.collider.gameObject.transform.localScale.x < 0.01)
            {
                
                player.GetComponent<PlayerControls>().SetDirt();
                player.GetComponent<PlayerControls>().CleanedHand();
                hit.collider.gameObject.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            }
        }

    }
}
