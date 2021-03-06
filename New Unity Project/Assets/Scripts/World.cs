﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int seed;
    public BiomeAttributes biome;
    public Transform player;
    public GameObject test;
    public Vector3 spawnPosition;
    public Material material;
    public BlockType[] blockTypes;
    public GameObject digable;

    Chunk[,] chunks = new Chunk[VoxelData.WorldSizeInChunks, VoxelData.WorldSizeInChunks];

    List<ChunkCoord> activeChunks = new List<ChunkCoord>();
    ChunkCoord playerChunkCoord;
    ChunkCoord playerLastChunkCoord;
    private void Start()
    {
        //generate a random seed for the world
        Random.InitState(seed);
        //generate the spawn position for the player based on world size
        spawnPosition = new Vector3((VoxelData.WorldSizeInChunks * VoxelData.ChunkWidth) / 2f, VoxelData.ChunkHeight, (VoxelData.WorldSizeInChunks * VoxelData.ChunkWidth) / 2f);
        GenerateWorld();
        //used for render distance
        playerLastChunkCoord = GetChunkCoordFromVector3(player.position);
    }
    private void Update()
    {
        playerChunkCoord = GetChunkCoordFromVector3(player.position);
        if (!playerChunkCoord.Equals(playerLastChunkCoord))
        {

        }
        CheckViewDistance();
    }
    void GenerateWorld()
    {
        //creates enough chunks to populate the world
        for (int x = (VoxelData.WorldSizeInChunks / 2) - VoxelData.viewDistanceInChunks; x < (VoxelData.WorldSizeInChunks / 2) + VoxelData.viewDistanceInChunks; x++)
        {
            for (int z = (VoxelData.WorldSizeInChunks / 2) - VoxelData.viewDistanceInChunks; z < (VoxelData.WorldSizeInChunks / 2) + VoxelData.viewDistanceInChunks; z++)
            {
                CreateNewChunk(x, z);
            }
        }
        //spawns the player
        player.position = spawnPosition;
    }

    ChunkCoord GetChunkCoordFromVector3(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x / VoxelData.ChunkWidth);
        int z = Mathf.FloorToInt(pos.z / VoxelData.ChunkWidth);
        return new ChunkCoord(x, z);
    }

    void CheckViewDistance()
    {
        ChunkCoord coord = GetChunkCoordFromVector3(player.position);

        List<ChunkCoord> previouslyActiveChunks = new List<ChunkCoord>(activeChunks);
        for (int x = coord.x - VoxelData.viewDistanceInChunks; x < coord.x + VoxelData.viewDistanceInChunks; x++)
        {
            for (int z = coord.z - VoxelData.viewDistanceInChunks; z < coord.z + VoxelData.viewDistanceInChunks; z++)
            {
                if (isChunkInWorld(new ChunkCoord(x,z)))
                {
                    if (chunks[x,z] == null)
                    {
                        CreateNewChunk(x,z);
                    }
                    else if (!chunks[x,z].isActive)
                    {
                        chunks[x, z].isActive = true;
                        activeChunks.Add(new ChunkCoord(x, z));
                    }
                }
                for (int i = 0; i < previouslyActiveChunks.Count; i++)
                {
                    if (previouslyActiveChunks[i].Equals(new ChunkCoord(x,z)))
                    {
                        previouslyActiveChunks.RemoveAt(i);
                    }
                }
            }
        }
        foreach (ChunkCoord c in previouslyActiveChunks)
        {
            chunks[c.x, c.z].isActive = false;
        }
    }
    public byte GetVoxel(Vector3 pos, GameObject chunk)
    {
        int yPos = Mathf.FloorToInt(pos.y);
        if (!isVoxelInWorld(pos))
        {
            return 0;
        }
        if (yPos == 0)
        {
            return 1;
        }

        int terrainHeight = Mathf.FloorToInt(biome.terrainHeight * Noise.Get2DPerlin(new Vector2(pos.x, pos.z), 0, biome.terrainScale)) + biome.solidGroundHeight;
        byte voxelValue = 0;

        if (yPos == terrainHeight)
        {
            float chance = 5;
            int dirtType = Random.Range(3, 20);
            float check = Random.Range(0f, 100f);
            if (check <= chance && (pos.x % 16) != 0 && (pos.x % 16) != 15 && (pos.z % 16) != 0 && (pos.z % 16) != 15)
            {
                voxelValue = 0;
                GameObject go;
                go = Instantiate(digable, new Vector3(pos.x, yPos, pos.z), Quaternion.identity) as GameObject;
                go.transform.parent = chunk.transform;
            }
            else if (dirtType <= 15)
            {
                voxelValue = 3;
            }
            else if (dirtType == 16)
            {
                voxelValue = 4;
            }
            else if (dirtType == 17)
            {
                voxelValue = 5;
            }
            else if (dirtType == 18)
            {
                voxelValue = 6;
            }
            else if (dirtType == 19)
            {
                voxelValue = 7;
            }
        }
        else if (yPos < terrainHeight && yPos > terrainHeight - 4)
        {
            voxelValue = 9;
        }
        else if (yPos > terrainHeight)
        {
            return 0;
        }
        else
        {
            voxelValue = 2;
        }


        if (voxelValue == 2)
        {
            foreach (Lode lode in biome.lodes)
            {
                if (yPos > lode.minHeight && yPos < lode.maxHeight)
                {
                    if (Noise.Get3DPerlin(pos,lode.noiseOffset,lode.scale,lode.threshold))
                    {
                        voxelValue = lode.blockID;
                    }
                }
            }
        }  
        return voxelValue;
        
    }

    public byte GetVoxel(Vector3 pos)
    {
        int yPos = Mathf.FloorToInt(pos.y);
        if (!isVoxelInWorld(pos))
        {
            return 0;
        }
        if (yPos == 0)
        {
            return 1;
        }

        int terrainHeight = Mathf.FloorToInt(biome.terrainHeight * Noise.Get2DPerlin(new Vector2(pos.x, pos.z), 0, biome.terrainScale)) + biome.solidGroundHeight;
        byte voxelValue = 0;

        if (yPos == terrainHeight)
        {
            float chance = 1;
            float check = Random.Range(0f, 100f);
            if (check <= chance && (pos.x % 16) != 0 && (pos.x % 16) != 15 && (pos.z % 16) != 0 && (pos.z % 16) != 15)
            {
                voxelValue = 0;
                GameObject dig;

            }
            else
            {
                voxelValue = 3;
            }
        }
        else if (yPos < terrainHeight && yPos > terrainHeight - 4)
        {
            voxelValue = 5;
        }
        else if (yPos > terrainHeight)
        {
            return 0;
        }
        else
        {
            voxelValue = 2;
        }


        if (voxelValue == 2)
        {
            foreach (Lode lode in biome.lodes)
            {
                if (yPos > lode.minHeight && yPos < lode.maxHeight)
                {
                    if (Noise.Get3DPerlin(pos, lode.noiseOffset, lode.scale, lode.threshold))
                    {
                        voxelValue = lode.blockID;
                    }
                }
            }
        }
        return voxelValue;

    }

    void CreateNewChunk(int x, int z)
    {
        chunks[x, z] = new Chunk(new ChunkCoord(x, z), this);
        activeChunks.Add(new ChunkCoord(x, z));
    }

    bool isChunkInWorld(ChunkCoord coord)
    {
        if (coord.x > 0 && coord.x < VoxelData.WorldSizeInChunks -1 && coord.z > 0 && coord.z < VoxelData.WorldSizeInChunks -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool isVoxelInWorld(Vector3 pos)
    {
        if (pos.x >= 0 && pos.x < VoxelData.WorldSizeInVoxels && pos.y >= 0 && pos.y < VoxelData.ChunkHeight && pos.z >= 0 && pos.z < VoxelData.WorldSizeInVoxels)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

[System.Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;

    [Header("Texture Values")]
    public int backFaceTexture;
    public int frontFaceTexture;
    public int topFaceTexture;
    public int bottomFaceTexture;
    public int leftFaceTexture;
    public int rightFaceTexture;
    public int GetTextureID(int faceIndex)
    {
        switch (faceIndex)
        {
            case 0:
                return backFaceTexture;
            case 1:
                return frontFaceTexture;
            case 2:
                return topFaceTexture;
            case 3:
                return bottomFaceTexture;
            case 4:
                return leftFaceTexture;
            case 5:
                return rightFaceTexture;
            default:
                Debug.Log("error");
                return 0;
        }
    }
}
