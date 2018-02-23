using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block {

    public int[] position = { 4, 11 };
    public float spaceBetweenBlocksX = 0.038f;
    public float blocksWidth = 0.78f;
    public float spaceBetweenBlocksY = 0.05f;
    public float blocksHeight = 0.74f;
    public GameObject gameObjectBlock;
    
    public Block(int[] position)
    {
        this.position = position;
    }
}
