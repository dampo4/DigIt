using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTexture : MonoBehaviour
{
    MeshFilter cubeMesh;
    Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        cubeMesh = GetComponent<MeshFilter>();
        mesh = cubeMesh.mesh;
        Vector2[] uvMap = mesh.uv;
        //front
        uvMap[0] = new Vector2(0.5f, 0.75f);
        uvMap[1] = new Vector2(0.75f, 0.75f);
        uvMap[2] = new Vector2(0.5f, 1f);
        uvMap[3] = new Vector2(0.75f, 1f);
        //top
        uvMap[4] = new Vector2(0.75f, 0.75f);
        uvMap[5] = new Vector2(1f, 0.75f);
        uvMap[8] = new Vector2(0.75f, 0.5f);
        uvMap[9] = new Vector2(1f, 0.5f);
        //back
        uvMap[6] = new Vector2(0.75f, 0.75f);
        uvMap[7] = new Vector2(0.5f, 0.75f);
        uvMap[10] = new Vector2(0.75f, 1f);
        uvMap[11] = new Vector2(0.5f, 1f);
        //bottom
        uvMap[12] = new Vector2(0.25f, 0.75f);
        uvMap[13] = new Vector2(0.25f, 1f);
        uvMap[14] = new Vector2(0.5f, 1f);
        uvMap[15] = new Vector2(0.5f, 0.75f);
        //left
        uvMap[16] = new Vector2(0.5f, 0.75f);
        uvMap[17] = new Vector2(0.5f, 1f);
        uvMap[18] = new Vector2(0.75f, 1f);
        uvMap[19] = new Vector2(0.75f, 0.75f);
        //right
        uvMap[20] = new Vector2(0.5f, 0.75f);
        uvMap[21] = new Vector2(0.5f, 1f);
        uvMap[22] = new Vector2(0.75f, 1f);
        uvMap[23] = new Vector2(0.75f, 0.75f);

        mesh.uv = uvMap;

    }

}
