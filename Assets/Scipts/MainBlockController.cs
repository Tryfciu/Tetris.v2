using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlockController : MonoBehaviour
{
    Rigidbody2D Rigid;
    int[] position = {4,11};
    float timeFromLastMove = 0f;
    float movingBlockSpeed = 5f;
    float spaceBetweenBlocksX = 0.038f;
    float blocksWidth = 0.78f;
    float spaceBetweenBlocksY = 0.05f;
    float blocksHeight = 0.74f;
    MainBlockSpawner spawner;

    // Use this for initialization
    void Start ()
    {
        startupMainBlockConfig();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeFromLastMove += Time.deltaTime;
        if (timeFromLastMove >= 1 / movingBlockSpeed)
        {
            moveBlockDown();
        }
        mainBlockMovement();
    }

    private void moveBlockDown()
    {
            if (position[1] <= 0 || spawner.blocksArray[position[0], position[1] - 1] == true)
            {
                spawner.isMainBlockAlive = false;
                spawner.blocksArray[position[0], position[1]] = true;
                GetComponent<MainBlockController>().enabled = false;
            }
            else
            {
                position[1] -= 1;
                moveBlockToPosition(position);
                timeFromLastMove = 0f;
            }
    }

    void startupMainBlockConfig()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Rigid.gravityScale = 0;
        moveBlockToPosition(position);
        spawner = GameObject.Find("MainBlocksSpawner").GetComponent<MainBlockSpawner>();
    }

    void mainBlockMovement()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(position[0]<=0 || spawner.blocksArray[position[0]-1, position[1]] == true)
            {
                //TODO can't move sound
            }
            else
            {
                position[0] -= 1;
                moveBlockToPosition(position);
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(position[0]>=8 || spawner.blocksArray[position[0]+1, position[1]] == true)
            {
                //TODO can't move
            }
            else
            {
                position[0] += 1;
                moveBlockToPosition(position);
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            for(int i=position[1];i>0;i--)
            {
                moveBlockDown();
            }
        }
    }

    void moveBlockToPosition(int [] position)
    {
        float positionX = -3.31f + (spaceBetweenBlocksX * (position[0] + 1) + blocksWidth * position[0]);
        float positionY = -4.10f + (spaceBetweenBlocksY * (position[1] + 1) + blocksHeight * position[1]);
        this.transform.position = new Vector3(positionX, positionY, 0);
    }
}
