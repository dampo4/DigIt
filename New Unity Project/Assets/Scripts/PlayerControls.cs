using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public XRNode inputSource;
    public XRNode inputSource2;
    public Canvas canvas;
    public List<GameObject> items;
    public List<GameObject> LeftHands;
    public List<GameObject> RightHands;
    public List<Finds> finds;
    public GameObject shovel;
    public GameObject shower;
    public GameObject RHand;
    public GameObject detector;
    public List<Sprite> treasures;
    public GameObject dirtHand;
    public GameObject lHand;
    public GameObject satchel;
    public GameObject showermodel;
    public List<GameObject> treasureHands;
    public List<int> found;
    public bool dirtOut = false;
    [SerializeField] private bool isTrigger;
    [SerializeField] private bool pressed = false;
    [SerializeField] private bool isSwitch;
    [SerializeField] private bool pressed1 = false;
    [SerializeField] private bool isSwitch2;
    [SerializeField] private bool pressed2 = false;
    [SerializeField] private int active = 0;
    public GameObject rightHand;
    public GameObject test;
    public int bookIndex;
    public GameObject pageLeft;
    public GameObject pageMonarch;
    public GameObject pageDate;
    public GameObject pageDesc;
    public Animator Ranimator;
    public GameObject right;
    public Animator Lanimator;
    public GameObject left;
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
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        InputDevice device2 = InputDevices.GetDeviceAtXRNode(inputSource2);
        //Debug.Log(rightHand.GetComponent<ActionBasedController>().modelPrefab.name);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out isTrigger);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out isSwitch);
        device2.TryGetFeatureValue(CommonUsages.primaryButton, out isSwitch2);
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
            if (RHand.activeSelf)
            {
                DisableRight();
                shovel.SetActive(true);
            }
            else if (shovel.activeSelf)
            {
                DisableRight();
                detector.SetActive(true);
            }
            else if (detector.activeSelf)
            {
                DisableRight();
                RHand.SetActive(true);
            }
            else if (shower.activeSelf)
            {
                DisableRight();
                RHand.SetActive(true);
                showermodel.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                DisableRight();
                RHand.SetActive(true);

            }

        }
        else if (!isSwitch)
        {
            pressed1 = false;
        }

        if (isSwitch2 && !pressed2)
        {
            pressed2 = true;
            if (lHand.activeSelf)
            {
                DisableLeft();
                satchel.SetActive(true);
            }
            else if (satchel.activeSelf)
            {
                DisableLeft();
                if (dirtOut)
                {
                    dirtHand.SetActive(true);
                }
                else
                {
                    lHand.SetActive(true);
                }
            }
            else if (dirtHand.activeSelf)
            {
                DisableLeft();
                lHand.SetActive(true);

            }
            else
            {
                DisableLeft();
                lHand.SetActive(true);

            }

        }
        else if (!isSwitch2)
        {
            pressed2 = false;
        }
    }
    public void CleanedHand()
    {
        DisableLeft();
        int rand = Random.Range(0, 10);
        treasureHands[rand].SetActive(true);
        UpdateFound(rand);
        
    }
    public void DirtInHand(GameObject button)
    {
        if (button.GetComponent<Image>().isActiveAndEnabled && dirtOut == false)
        {
            DisableLeft();
            dirtOut = true;
            dirtHand.SetActive(true);
            button.GetComponent<Image>().enabled = false;
        }


    }
    public void DisableLeft()
    {
        Lanimator = left.GetComponent<Hand>().animator;
        Lanimator.Rebind();
        Lanimator.Update(0f);
        foreach (GameObject hand in LeftHands)
        {
            hand.SetActive(false);
        }
    }
    public void DisableRight()
    {
        Ranimator = right.GetComponent<Hand>().animator;
        Ranimator.Rebind();
        Ranimator.Update(0f);
        foreach (GameObject hand in RightHands)
        {
            hand.SetActive(false);
        }
    }
    public void UpdateFound(int rand)
    {
        if (found.Count == 0)
        {
            pageLeft.GetComponent<Image>().sprite = finds[rand].pic;
            pageLeft.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
            pageMonarch.GetComponent<Text>().text = finds[rand].monarch;
            pageDate.GetComponent<Text>().text = finds[rand].date;
            pageDesc.GetComponent<Text>().text = finds[rand].item;
        }
        if (!found.Contains(rand))
        {
            found.Add(rand);
        }
    }

    public void BookLeft()
    {
        if (!(bookIndex <= 0))
        {
            bookIndex--;
            pageLeft.GetComponent<Image>().sprite = finds[found[bookIndex]].pic;
            pageMonarch.GetComponent<Text>().text = finds[found[bookIndex]].monarch;
            pageDate.GetComponent<Text>().text = finds[found[bookIndex]].date;
            pageDesc.GetComponent<Text>().text = finds[found[bookIndex]].item;
        }
    }
    public void BookRight()
    {
        if (!(bookIndex >= found.Count-1))
        {
            bookIndex++;
            pageLeft.GetComponent<Image>().sprite = finds[found[bookIndex]].pic;
            pageMonarch.GetComponent<Text>().text = finds[found[bookIndex]].monarch;
            pageDate.GetComponent<Text>().text = finds[found[bookIndex]].date;
            pageDesc.GetComponent<Text>().text = finds[found[bookIndex]].item;
        }
    }
    public void SetDirt()
    {
        Debug.Log("Hi");
        dirtOut = false;
    }
}
