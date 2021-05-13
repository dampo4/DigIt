using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Chunk
{
    public ChunkCoord coord;

    GameObject chunkObject;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    int vertexIndex = 0;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();
    byte[,,] voxelMap = new byte[VoxelData.ChunkWidth, VoxelData.ChunkHeight, VoxelData.ChunkWidth];
    World world;

    public Chunk(ChunkCoord _coord, World _world)
    {
        coord = _coord;
        world = _world;
        chunkObject = new GameObject();
        meshFilter = chunkObject.AddComponent<MeshFilter>();
        meshRenderer = chunkObject.AddComponent<MeshRenderer>();
        meshCollider = chunkObject.AddComponent<MeshCollider>();

        meshRenderer.material = world.material;
        chunkObject.transform.SetParent(world.transform);
        chunkObject.transform.position = new Vector3(coord.x * VoxelData.ChunkWidth, 0f, coord.z * VoxelData.ChunkWidth);
        chunkObject.name = "Chunk" + coord.x + ", " + coord.z;

        PopulateVoxelMap();
        CreateMeshData();
        CreateMesh();

        meshCollider.sharedMesh = meshFilter.mesh;
    }
    
    void PopulateVoxelMap()
    {
        for (int y = 0; y < VoxelData.ChunkHeight; y++)
        {
            for (int x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    voxelMap[x, y, z] = world.GetVoxel(new Vector3(x, y, z)+ position, chunkObject);
                }
            }
        }
    }
    void CreateMeshData()
    {
        for (int y = 0; y < VoxelData.ChunkHeight; y++)
        {
            for (int x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    if (world.blockTypes[voxelMap[x,y,z]].isSolid)
                    {
                        AddVoxelDataToChunk(new Vector3(x, y, z));
                    }
                    
                }
            }
        }
    }

    public bool isActive
    {
        get { return chunkObject.activeSelf; }
        set { chunkObject.SetActive(value); }
    }

    public Vector3 position
    {
        get { return chunkObject.transform.position; }
    }

    bool isVoxelInChunk(int x, int y, int z)
    {
        if (x < 0 || x > VoxelData.ChunkWidth - 1 || y < 0 || y > VoxelData.ChunkHeight - 1 || z < 0 || z > VoxelData.ChunkWidth - 1)
        {
            return false;
        }
        else
            return true;
    }

    bool CheckVoxel(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);

        if (!isVoxelInChunk(x,y,z))
        {
            return world.blockTypes[world.GetVoxel(pos + position)].isSolid;
        }

        return world.blockTypes[voxelMap[x, y, z]].isSolid;
    }
    void AddVoxelDataToChunk(Vector3 pos)
    {
        for (int j = 0; j < 6; j++)
        {
            if (!CheckVoxel(pos + VoxelData.faceChecks[j]))
            {
                byte blockID = voxelMap[(int)pos.x, (int)pos.y, (int)pos.z];
                vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[j, 0]]);
                vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[j, 1]]);
                vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[j, 2]]);
                vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[j, 3]]);
                AddTexture(world.blockTypes[blockID].GetTextureID(j));
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 3);
                vertexIndex += 4;
            }
        }
    }
    void CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        chunkObject.AddComponent<TeleportationArea>();
    }

    void AddTexture (int textureID)
    {
        float y = textureID / VoxelData.TextureAtlasSizeInBlocks;
        float x = textureID - (y * VoxelData.TextureAtlasSizeInBlocks);

        x *= VoxelData.NormalisedBlockTextureSize;
        y *= VoxelData.NormalisedBlockTextureSize;

        y = 1f - y - VoxelData.NormalisedBlockTextureSize;
        uvs.Add(new Vector2(x, y));
        uvs.Add(new Vector2(x, y + VoxelData.NormalisedBlockTextureSize));
        uvs.Add(new Vector2(x + VoxelData.NormalisedBlockTextureSize, y));
        uvs.Add(new Vector2(x + VoxelData.NormalisedBlockTextureSize, y + VoxelData.NormalisedBlockTextureSize));
    }
}

public class ChunkCoord
{
    public int x;
    public int z;

    public ChunkCoord(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public bool Equals (ChunkCoord other)
    {
        if (other == null)
        {
            return false;
        }
        else if (other.x == x && other.z == z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
} 