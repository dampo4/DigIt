using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public XRNode inputSource;
    public Canvas canvas;
    public List<GameObject> items;
    public GameObject shovel;
    public GameObject shower;
    [SerializeField] private bool isTrigger;
    [SerializeField] private bool pressed = false;
    [SerializeField] private bool isSwitch;
    [SerializeField] private bool pressed1 = false;
    [SerializeField] private int active = 0;
    public GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        //Debug.Log(rightHand.GetComponent<ActionBasedController>().modelPrefab.name);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out isTrigger);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out isSwitch);
        if (isTrigger && !pressed)
        {
            pressed = true;
            canvas.enabled = !canvas.isActiveAndEnabled;
        }
        else if(!isTrigger)
        {
            pressed = false;
        }
        if (isSwitch && !pressed1)
        {
            pressed1 = true;
            if (items[3].activeSelf == true)
            {
                items[3].SetActive(false);
                shower.GetComponent<Renderer>().enabled = true;

            }
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetActive(false);
            }
            active++;
            if (active == 1 || active == 2)
            {
                rightHand.GetComponent<XRRayInteractor>().enabled = false;
            }
            else
            {
                rightHand.GetComponent<XRRayInteractor>().enabled = true;
            }
            items[active].SetActive(true);
            //rightHand.GetComponent<ActionBasedController>().modelPrefab = items[active].transform;
            if (active == 2)
            {
                active = -1;
            }
            
        }
        else if (!isSwitch)
        {
            pressed1 = false;
        }
    }
}
