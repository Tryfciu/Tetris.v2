using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlockSpawner : MonoBehaviour
{
    public bool isMainShapeAlive = false;
    public bool[,] isOcupiedNetScheme = new bool[9, 16];
    BlockShape currentShape;
    float timeFromLastMoveDown = 0;
    float fallingSpeedInSeconds = 0.1f;

	
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
            //ClearFullrows();
        }

        ShapeControl();

	}

    void RenderNewShape()
    {
        currentShape = new BlockShape();

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
        }

        isMainShapeAlive = false;
    }

    void ShapeControl()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) == true && CurrentShapeCanMoveAside("left"))
        {
            moveShapeAside("left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) == true && CurrentShapeCanMoveAside("right"))
        {
            moveShapeAside("right");
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

    void moveShapeAside(string side)
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

    //void ClearFullRows()
    //{
    //    foreach (Block block in currentShape.blocks)
    //    {
    //        CheckIfRowIsFull(block.position[1]);
    //    }
    //}


    // bool CheckIfRowIsFull(int row)
    //{
    //    for (int i = 0; i < 9; i++)
    //    {
    //        if(!isOcupiedNetScheme[row, i])
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
}
