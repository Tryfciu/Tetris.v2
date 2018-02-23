using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlockSpawner : MonoBehaviour
{
    public bool isMainShapeAlive = false;
    public bool[,] isOcupiedNetScheme = new bool[9, 12];
    BlockShape currentShape;
    float timeFromLastMoveDown = 0;
    float timeBetweenMoves = 0.5f;
	
	void Update ()
    {
        if (isMainShapeAlive == false)
        {
            renderShape();
            isMainShapeAlive = true;
        }

        if(isPossibleToMoveShapeDown())
        {
            shapeDefultLowerMove();
        }
        else
        {
            isMainShapeAlive = false;
        }
	}

    void renderShape()
    {
        currentShape = new BlockShape();

        foreach (Block block in currentShape.blocks)
        {
            block.gameObjectBlock = Instantiate(currentShape.prefab);
            setPositionOfBlock(block);
        }
    }

    bool isPossibleToMoveShapeDown()
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

    void moveShapeToOneLowerPosition()
    {
        foreach (Block block in currentShape.blocks)
        {
                block.position[1] -= 1;
                setPositionOfBlock(block);
        }
    }

    void setPositionOfBlock(Block block)
    {
        float positionX = -3.31f + (block.spaceBetweenBlocksX * (block.position[0] + 1) + block.blocksWidth * block.position[0]);
        float positionY = -4.10f + (block.spaceBetweenBlocksY * (block.position[1] + 1) + block.blocksHeight * block.position[1]);
        block.gameObjectBlock.transform.position = new Vector3(positionX, positionY, 0);
    }

    void shapeDefultLowerMove()
    {
        if(timeFromLastMoveDown >= timeBetweenMoves)
        {
            moveShapeToOneLowerPosition();
            timeFromLastMoveDown = 0;
        }
        else
        {
            timeFromLastMoveDown += Time.deltaTime;
        }
    }
}
