using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class Move : MonoBehaviour
{
    private InputDevice _device_leftController;
    private InputDevice _device_rightController;
    private CharacterController _character;
    private Vector2 _inputAxis_leftController;
    private Vector2 _inputAxis_rightController;
    private GameObject _camera;
    private float speed;

    void Start()
    {
        //try to get the left and right controllers
        _device_leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        _device_rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        //get access to the player and camera to aid with movement
        _character = GetComponent<CharacterController>();
        _camera = GetComponent<XRRig>().cameraGameObject;
    }
    void Update()
    {
        //get input from the left and right joysticks
        _device_leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out _inputAxis_leftController);
        _device_rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out _inputAxis_rightController);
        //set y to 0 so the player remains on the ground
        var inputVector_leftController = new Vector3(_inputAxis_leftController.x, 0, _inputAxis_leftController.y);
        var inputVector_rightController = new Vector3(_inputAxis_rightController.x, 0, _inputAxis_rightController.y);
        //gets the angle the player is currently looking at and adds it to the direction pressed on the controller
        float targetAngle = Mathf.Atan2(_inputAxis_leftController.x, _inputAxis_leftController.y)
            * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
        var newDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        //assigns a speed value to the player's motion
        if (inputVector_leftController.magnitude > 0.2)
        {
            speed = inputVector_leftController.magnitude;
        }
        else
        {
            speed = 0f;
        }
        if (_inputAxis_rightController.x > 0.2f || _inputAxis_rightController.x < -0.2f)
        {
            transform.Rotate(Vector3.up * _inputAxis_rightController.x);
        }
        //moves the player in the correct direction
        _character.Move(newDirection * Time.deltaTime * speed*2);
        //applies gravity to the player
        _character.Move(new Vector3(0, -0.1f, 0));
    }
}
