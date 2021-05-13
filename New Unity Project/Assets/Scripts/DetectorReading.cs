using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectorReading : MonoBehaviour
{
    public Text detectorReading;
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    public Material found;
    public Material nothing;
    public List<GameObject> lights;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "Artefact")
        {
            foreach (GameObject item in lights)
            {
                item.GetComponent<Renderer>().material = found;
            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        foreach (GameObject item in lights)
        {
            item.GetComponent<Renderer>().material = nothing;
        }
    }
}