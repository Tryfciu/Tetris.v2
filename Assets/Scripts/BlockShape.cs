using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShape
{

    public Block[] blocks = new Block[4];
    int[][] shapes = new int[4][];
    public GameObject prefab;
    Color color = Color.black;

    //public int leftExtreme = 100000; //NEED TO BE GREATER THAN 9
    //public int rightExtreme = 0;
    //public int bottomExtreme = 100000; //NEED TO BE GREATER THAN 16

    public BlockShape()
    {
        prefab = (GameObject)Resources.Load("MainBlock");
        RandomShape();
        InitializeShape();
    }

    void InitializeShape()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = new Block(shapes[i]);

            //if (blocks[i].position[0] > rightExtreme)
            //{
            //    rightExtreme = blocks[i].position[0];
            //}
            //if (blocks[i].position[0] < leftExtreme)
            //{
            //    leftExtreme = blocks[i].position[0];
            //}
            //if (blocks[i].position[1] < bottomExtreme)
            //{
            //    bottomExtreme = blocks[i].position[1];
            //}
        }
    }

    void RandomShape()
    {
        shapes[1] = new int[] { 5, 12 };
        shapes[0] = new int[] { 5, 13 };
        shapes[2] = new int[] { 4, 13 };
        shapes[3] = new int[] { 3, 13 };
    }

    public void RollAndChangeColor()
    {

        switch(Random.Range(0, 3))
        {
            case 0:
                color = Color.blue;
                break;
            case 1:
                color = Color.red;
                break;
            case 2:
                color = Color.yellow;
                break;
        }

        foreach (Block block in blocks)
        {
            block.gameObjectBlock.GetComponent<Renderer>().material.color = color;
        }
    }

}
