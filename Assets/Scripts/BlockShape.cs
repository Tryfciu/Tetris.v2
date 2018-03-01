using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShape
{

    public Block[] blocks = new Block[4];
    int[][] shapes = new int[4][];
    public GameObject prefab;
    Color color = Color.black;

    public BlockShape()
    {
        prefab = (GameObject)Resources.Load("MainBlock");
        SetRandomShape();
        InitializeShape();
    }

    public BlockShape(int testIterator) //TEST
    {
        prefab = (GameObject)Resources.Load("MainBlock");
        if(testIterator == 1)
        {
            SetTestShape(1);
        }
        else
        {
            SetTestShape(2);
        }
        InitializeShape();
    }

    void InitializeShape()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = new Block(shapes[i]);
        }
    }

    void SetTestShape(int iterator) //TEST
    {
        if(iterator == 1)
        {
            shapes[0] = new int[] { 0, 15 };
            shapes[1] = new int[] { 0, 14 };
            shapes[2] = new int[] { 0, 13 };
            shapes[3] = new int[] { 0, 12 };
        }
        else
        {
            shapes[0] = new int[] { 5, 9 };
            shapes[1] = new int[] { 5, 8 };
            shapes[2] = new int[] { 4, 9 };
            shapes[3] = new int[] { 4, 8 };
        }
    }

    void SetRandomShape()
    {
        shapes[0] = new int[] { 4, 12 };
        int blockedMove = 0;
        int randomMove;
        int indexOfBlockOnTheBottom = 0;

        for (int i = 1; i < 4; i++)
        {
            shapes[i] = new int[] { shapes[i - 1][0], shapes[i - 1][1] };

            do
            {
                randomMove = Random.Range(1, 5);
            } while (randomMove == blockedMove);

            switch(randomMove)
            {
                case 1:
                    shapes[i][0] -= 1;
                    blockedMove = 2;
                    break;
                case 2:
                    shapes[i][0] += 1;
                    blockedMove = 1;
                    break;
                case 3:
                    shapes[i][1] -= 1;
                    blockedMove = 4;
                    break;
                case 4:
                    shapes[i][1] += 1;
                    blockedMove = 3;
                    break;
            }

            if(shapes[i][1] < shapes[i-1][1])
            {
                indexOfBlockOnTheBottom = i;
            }
        }

        SetStartingPosition(indexOfBlockOnTheBottom);
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

    private void SetStartingPosition(int indexOfBlockOnTheBottom)
    {
        bool startingPositionIsSet = false;
        while (startingPositionIsSet == false)
        {
            if (shapes[indexOfBlockOnTheBottom][1] != 12)
            {
                foreach (int[] position in shapes)
                {
                    position[1]++;
                }
            }
            else
            {
                startingPositionIsSet = true;
            }
        }
    }
}
