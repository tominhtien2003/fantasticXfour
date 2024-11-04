using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private static Board instance;
    public static Board Instance { get { return instance; } }
    [SerializeField] Block rootBlock;
    [SerializeField] LayerMask groundMask;
    public Block[,,] boardStoreBlock;
    [SerializeField] int maxRows = 50;
    [SerializeField] int maxCols = 50;
    [SerializeField] int maxHeight = 5;

    private Block currentBlock;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        boardStoreBlock = new Block[maxRows, maxCols, maxHeight];
        BuildBoardBFS();
    }
    private void Start()
    {

    }

    private void BuildBoardBFS()
    {
        Queue<(Block block, int row, int col, int height)> queue = new Queue<(Block, int, int, int)>();
        HashSet<Block> visited = new HashSet<Block>();

        queue.Enqueue((rootBlock, 0, 0, 0));
        visited.Add(rootBlock);
        SetBlockAtPosition(0, 0, 0, rootBlock);
        rootBlock.SetPositionInBoard(new Vector3Int(0, 0, 0));

        Vector3[] directions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back, Vector3.up, Vector3.down };
        (int, int, int)[] directionOffsets = { (0, 1, 0), (0, -1, 0), (1, 0, 0), (-1, 0, 0), (0, 0, 1), (0, 0, -1) };

        while (queue.Count > 0)
        {
            var (currentBlock, currentRow, currentCol, currentHeight) = queue.Dequeue();
            for (int idx = 0; idx < directions.Length; idx++)
            {
                int newRow = currentRow + directionOffsets[idx].Item1;
                int newCol = currentCol + directionOffsets[idx].Item2;
                int newHeight = currentHeight + directionOffsets[idx].Item3;

                if (newRow < 0 || newCol < 0 || newHeight < 0 || newRow >= maxRows || newCol >= maxCols || newHeight >= maxHeight)
                    continue;

                RaycastHit hit;
                if (Physics.Raycast(currentBlock.transform.position, directions[idx], out hit, 1f, groundMask))
                {
                    Block adjacentBlock = hit.collider.GetComponentInParent<Block>();
                    if (adjacentBlock != null && !visited.Contains(adjacentBlock))
                    {
                        visited.Add(adjacentBlock);
                        queue.Enqueue((adjacentBlock, newRow, newCol, newHeight));
                        SetBlockAtPosition(newRow, newCol, newHeight, adjacentBlock);
                    }
                }
            }
        }
    }

    private void SetBlockAtPosition(int row, int col, int height, Block block)
    {
        boardStoreBlock[row, col, height] = block;
        block?.SetPositionInBoard(new Vector3Int(row, col, height));
    }

    public Block GetBlockAtPosition(int row, int col, int height)
    {
        if (row < 0 || col < 0 || height < 0 || row >= maxRows || col >= maxCols || height >= maxHeight)
        {
            //Debug.Log("Position not valid " + row + " " + col + " " + height);
            return null;
        }
        return boardStoreBlock[row, col, height];
    }

    public Block GetCurrentBlock()
    {
        return currentBlock;
    }

    public void SetCurrentBlock(Block newBlock)
    {
        currentBlock = newBlock;
    }
}