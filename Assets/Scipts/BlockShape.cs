using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShape
{

    public Block[] blocks = new Block[4];
    int[][] shapes = new int[4][];
    public GameObject prefab;

    public BlockShape()
    {
        prefab = (GameObject)Resources.Load("MainBlock");
        randomShape();
        initializeShape();
    }

    void initializeShape()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = new Block(shapes[i]);
        }
    }

    void randomShape()
    {
            shapes[1] = new int[] { 5, 10 };
            shapes[0] = new int[] { 5, 9 };
            shapes[2] = new int[] { 4, 9 };
            shapes[3] = new int[] { 3, 9 };
    }

}
