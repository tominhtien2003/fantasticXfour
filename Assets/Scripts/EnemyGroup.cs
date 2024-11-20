using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    private EndGame endGame;
    public List<BasePiece> enemyList = new List<BasePiece>();
    private bool hasEnemyLoseSound = false;
    private void Start()
    {
        endGame = FindFirstObjectByType<EndGame>();
        BasePiece[] enemys = GetComponentsInChildren<BasePiece>(true);
        foreach (var enemy in enemys)
        {
            enemyList.Add(enemy);
        }
    }
    private void Update()
    {
        if (!hasEnemyLoseSound && enemyList.Count == 0)
        {
            endGame.Winn();
            AudioManager.Instance.PlaySFX("Win");
            hasEnemyLoseSound = true;
        }
    }
    public BasePiece FindEnemyBetter(out Block targetBlock)
    {
        foreach (var enemy in enemyList)
        {
            Block block = enemy.GetCurrentBlock();
            Vector3Int startPos = block.GetPositionInBoard();

            List<Vector2Int> directions = DirectionOfPieces.GetDirectionOfPiece(enemy);

            foreach (Vector2Int dir in directions)
            {
                Vector3Int temp = startPos;

                bool isSingleStep = enemy.pieceType == PieceType.King || enemy.pieceType == PieceType.Knight;

                do
                {
                    temp.x += dir.x;
                    temp.y += dir.y;

                    bool check = false;
                    for (int height = 0; height <= 3; height++)
                    {
                        temp.z = height;

                        Block nextBlock = Board.Instance.GetBlockAtPosition(temp.x, temp.y, temp.z);

                        if (nextBlock == null)
                        {
                            break;
                        }

                        if (nextBlock.tag != "CanNotMove")
                        {
                            BasePiece currentPiece = nextBlock.GetCurrentPiece();
                            if (currentPiece != null)
                            {
                                if (currentPiece.chessSide == ChessSide.Player)
                                {
                                    targetBlock = nextBlock;
                                    return enemy;
                                }
                                else
                                {
                                    check = true;
                                }
                            }
                            break;
                        }
                    }

                    if (isSingleStep || check)
                    {
                        break;
                    }

                } while (Board.Instance.GetBlockAtPosition(temp.x, temp.y, temp.z) != null);
            }
        }
        targetBlock = null;
        return null; 
    }

}
