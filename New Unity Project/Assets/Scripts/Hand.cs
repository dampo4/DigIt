using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum HandType
{
    Left,
    Right
}
public class Hand : MonoBehaviour
{
    public HandType handType;
    public Animator animator;
    private InputDevice inputDevice;
    public float thumbMoveSpeed = 0.1f;
    public float indexValue;
    public float thumbValue;
    public float threeFingerValue;
    //public GameObject right;
    //public GameObject left;
    public bool wasDeactive;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputDevice = GetInputDevice();
    }
    // Update is called once per frame
    void Update()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isSwitch);
        /*if (isSwitch)
        {
            Debug.Log("hgg");
            indexValue = 0;
            thumbValue = 0;
            threeFingerValue = 0;
            animator = GetComponent<Animator>();
            inputDevice = GetInputDevice();
        }*/
        AnimateHand();
    }
    InputDevice GetInputDevice()
    {
        InputDeviceCharacteristics controllerCharacteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
        if (handType == HandType.Left)
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Left;
        }
        else
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Right;
        }

        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, inputDevices);
        return inputDevices[0];
    }

    void AnimateHand()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.trigger, out indexValue);
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out threeFingerValue);

        inputDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out bool primaryTouched);
        inputDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out bool secondaryTouched);


        if (primaryTouched || secondaryTouched)
        {
            thumbValue += thumbMoveSpeed;
        }
        else
        {
            thumbValue -= thumbMoveSpeed;
        }

        thumbValue = Mathf.Clamp(thumbValue, 0, 1);

        animator.SetFloat("Index", indexValue);
        animator.SetFloat("ThreeFingers", threeFingerValue);
        animator.SetFloat("Thumb", thumbValue);

    }
}
