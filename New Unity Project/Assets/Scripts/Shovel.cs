using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    private bool empty = true;
    public GameObject item;
    public Material dug;
    // Update is called once per frame
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("Cube") && empty == true)
        {
            empty = false;
            collider.gameObject.GetComponent<Transform>().parent.localScale -= new Vector3(0, .33f, 0);
            collider.gameObject.GetComponent<Renderer>().material = dug;
            if (collider.gameObject.name.Contains("2"))
            {
                
                Destroy(collider.gameObject);
                Instantiate(item, collider.transform.position, Quaternion.identity);

            }
            else if (collider.gameObject.name.Contains("1"))
            {
                collider.gameObject.name = "Cube2";
            }
            else if (!collider.gameObject.name.Contains("1") && !collider.gameObject.name.Contains("2"))
            {
                collider.gameObject.name = "Cube1";
            }


        }
    }
    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.up, out hit, 5))
        {
            empty = true;
            Debug.Log(empty);
        }
        
    }
}
