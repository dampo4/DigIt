using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectorReading : MonoBehaviour
{
    public Text detectorReading;
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            detectorReading.text = "0";
            timer = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "Artefact")
        {
            timer = 0;
            if (other.gameObject.name.Contains("gold"))
            {
                if (other.gameObject.name.Contains("disc"))
                {
                    detectorReading.text = Random.Range(90, 100).ToString();
                    Debug.Log("2");
                }
                if (other.gameObject.name.Contains("random"))
                {
                    detectorReading.text = Random.Range(90, 100).ToString();
                    Debug.Log("2");
                }
            }
            if (other.gameObject.name.Contains("silver"))
            {
                if (other.gameObject.name.Contains("disc"))
                {
                    detectorReading.text = Random.Range(80, 90).ToString();
                    Debug.Log("2");
                }
                if (other.gameObject.name.Contains("random"))
                {
                    detectorReading.text = Random.Range(80, 90).ToString();
                    Debug.Log("2");
                }
            }
            if (other.gameObject.name.Contains("copper"))
            {
                if (other.gameObject.name.Contains("disc"))
                {
                    detectorReading.text = Random.Range(70, 80).ToString();
                    Debug.Log("2");
                }
                if (other.gameObject.name.Contains("random"))
                {
                    detectorReading.text = Random.Range(70, 80).ToString();
                    Debug.Log("2");
                }
            }
            if (other.gameObject.name.Contains("iron"))
            {
                if (other.gameObject.name.Contains("disc"))
                {
                    detectorReading.text = "72";
                    Debug.Log("2");
                }
                if (other.gameObject.name.Contains("random"))
                {
                    detectorReading.text = "72";
                    Debug.Log("2");
                }
            }

        }
    }
}