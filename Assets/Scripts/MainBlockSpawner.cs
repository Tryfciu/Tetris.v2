using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlockSpawner : MonoBehaviour
{
    bool isMainShapeAlive = false;
    bool[,] isOcupiedNetScheme = new bool[9, 16];
    List<Block> allPlacedBlocks = new List<Block>();
    BlockShape currentShape;
    float timeFromLastMoveDown = 0;
    float fallingSpeedInSeconds = 0.4f;
	
	void Update ()
    {
        if (isMainShapeAlive == false)
        {
            RenderNewShape();
        }

        if(IsPossibleToMoveShapeDown())
        {
            ShapeDefultLowerMove();
        }
        else
        {
            DeactivateCurrentShape();
            ClearFullRows();
        }

        ShapeControl();

	}

    int testIterator = 1; //TEST

    void RenderNewShape()
    {
        currentShape = new BlockShape();
        //currentShape = new BlockShape(testIterator); //TEST
        testIterator = testIterator == 1? 2 : 1; //TEST

        foreach (Block block in currentShape.blocks)
        {
            block.gameObjectBlock = Instantiate(currentShape.prefab);
            SetPositionOfBlock(block);
        }

        currentShape.RollAndChangeColor();
        isMainShapeAlive = true;
    }

    bool IsPossibleToMoveShapeDown()
    {
        foreach (Block block in currentShape.blocks)
        {
            if (block.position[1] == 0 || isOcupiedNetScheme[block.position[0], block.position[1] - 1] == true)
            {
                return false;
            }
        }
        return true;
    }

    void MoveShapeToOneLowerPosition()
    {
        foreach (Block block in currentShape.blocks)
        {
                block.position[1] -= 1;
                SetPositionOfBlock(block);
        }
    }

    void SetPositionOfBlock(Block block)
    {
        float positionX = -3.31f + (block.spaceBetweenBlocksX * (block.position[0] + 1) + block.blocksWidth * block.position[0]);
        float positionY = -4.10f + (block.spaceBetweenBlocksY * (block.position[1] + 1) + block.blocksHeight * block.position[1]);
        block.gameObjectBlock.transform.position = new Vector3(positionX, positionY, 0);
    }

    void ShapeDefultLowerMove()
    {
        if(timeFromLastMoveDown >= fallingSpeedInSeconds)
        {
            MoveShapeToOneLowerPosition();
            timeFromLastMoveDown = 0;
        }
        else
        {
            timeFromLastMoveDown += Time.deltaTime;
        }
    }

    void DeactivateCurrentShape()
    {
        foreach(Block block in currentShape.blocks)
        {
            if (block.position[1] >= 12)
            {
                GetComponent<MainBlockSpawner>().enabled = false;
                EndGame();
                break;
            }

            isOcupiedNetScheme[block.position[0], block.position[1]] = true;
            allPlacedBlocks.Add(block);
        }

        isMainShapeAlive = false;
    }

    void ShapeControl()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) == true && CurrentShapeCanMoveAside("left"))
        {
            MoveShapeAside("left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) == true && CurrentShapeCanMoveAside("right"))
        {
            MoveShapeAside("right");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) == true && IsPossibleToMoveShapeDown())
        {
            MoveShapeToOneLowerPosition();
        }
    }

    bool CurrentShapeCanMoveAside(string side)
    {
        int sideIndicator = 1;
        int maximumSidePosition = 8;

        if(side == "left")
        {
            sideIndicator = -1;
            maximumSidePosition = 0;
        }

        foreach (Block block in currentShape.blocks)
        {
            if (block.position[0] == maximumSidePosition || isOcupiedNetScheme[block.position[0] + sideIndicator, block.position[1]] == true)
            {
                return false;
            }
        }
        return true;
    }

    void MoveShapeAside(string side)
    {
        int sideIndicator = 1;

        if (side == "left")
        {
            sideIndicator = -1;
        }

        foreach(Block block in currentShape.blocks)
        {
            block.position[0] += sideIndicator;
            SetPositionOfBlock(block);
        }
    }

    void EndGame()
    {
        Application.Quit();
    }

    void ClearFullRows()
    {
        foreach (Block block in currentShape.blocks)
        {
            if(CheckIfRowIsFull(block.position[1]))
            {
                for (int i = 0; i < 9; i++)
                {
                    isOcupiedNetScheme[i, block.position[1]] = false;
                }

                foreach (Block blockToDestroy in allPlacedBlocks.FindAll(n => n.position[1] == block.position[1]))
                {
                    Destroy(blockToDestroy.gameObjectBlock);
                }
                allPlacedBlocks.RemoveAll(n => n.position[1] == block.position[1]);
                DescentRowAfterClearing(block.position[1]);
            }
        }
    }

    bool CheckIfRowIsFull(int row)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!isOcupiedNetScheme[i, row])
            {
                return false;
            }
        }
        return true;
    }

    void DescentRowAfterClearing(int indexOfClearedRow)
    {
        allPlacedBlocks.Sort((a, b) => a.position[1].CompareTo(b.position[1]));

        foreach (Block block in allPlacedBlocks)
        {
            if(block.position[1] > indexOfClearedRow)
            {
                isOcupiedNetScheme[block.position[0], block.position[1]] = false;
                block.position[1] -= 1;
                isOcupiedNetScheme[block.position[0], block.position[1]] = true;
                SetPositionOfBlock(block);
            }
        }
    }
}
