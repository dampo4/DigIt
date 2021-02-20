using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Dig : MonoBehaviour
{
    private InputDevice device;
    private bool dig;
    Collider collision;
    // Start is called before the first frame update
    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        device.TryGetFeatureValue(CommonUsages.primaryButton, out dig);
        if (dig == true && collision.gameObject.tag == "Artefact")
        {
            Debug.Log("Digging");
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        this.collision = collision;
        Debug.Log(collision.gameObject.tag);
    }

    void OnTriggerExit(Collider collision)
    {
        this.collision = null;
    }
}
