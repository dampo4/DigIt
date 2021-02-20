using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject cube;
    [Range(0.0f, 100.0f)]
    public float chance;
    public List<string> metal = new List<string>();
    public List<string> shape = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        int area = width * height;
        for (int i = 0; i < area; i++)
        {
            float check = Random.Range(0f, 100f);
            if (check < chance)
            {
                var newObject = (GameObject)Instantiate(cube, new Vector3(.5f + Mathf.Floor(i / width),
                    3f,
                    (.5f + i) - (Mathf.Floor(i / height) * height)), Quaternion.identity);
                newObject.name = metal[Random.Range(0, metal.Count)] + ' ' + shape[Random.Range(0, shape.Count)];

            }
        }
    }

}
