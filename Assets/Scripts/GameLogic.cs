using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public event EventHandler OnTurnChange;
    private static GameLogic instance;
    public bool pauseGame = false;
    public static GameLogic Instance { get { return instance; } }

    private BasePiece currentPiece;
    private Turn turn;

    public List<Block> blocksSelected = new List<Block>();

    public EnemyGroup enemyGroup;
    public PlayerGroup playerGroup;

    public int countNumberOfTimes;
    public TextMeshProUGUI txtCountNumberOfTimes;
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
        enemyGroup = FindFirstObjectByType<EnemyGroup>();
        playerGroup = FindFirstObjectByType<PlayerGroup>();
    }
    private void Start()
    {
        OnTurnChange += GameLogic_OnTurnChange;
        turn = Turn.Player;
    }

    private async void GameLogic_OnTurnChange(object sender, EventArgs e)
    {
        if (GetTurn() == Turn.Player)
        {
            return;
        }
        Block targetBlock;
        BasePiece piece = enemyGroup.FindEnemyBetter(out targetBlock);
        if (enemyGroup.enemyList.Count == 0) return;
        if (piece != null)
        {
            SetCurrentPiece(piece);
            Board.Instance.SetCurrentBlock(targetBlock);
            targetBlock.GetCurrentPiece()?.SetUpWhenIsTarget();
            piece.HandleMovement();
        }
        else
        {
            int indexEnemyGroup = UnityEngine.Random.Range(0, enemyGroup.enemyList.Count);
            SetCurrentPiece(enemyGroup.enemyList[indexEnemyGroup]);
            await SelectPiece(enemyGroup.enemyList[indexEnemyGroup]);

            Block block = RandomSelectBlock();
            Board.Instance.SetCurrentBlock(block);

            if (block != null && block.GetCurrentPiece() != null)
            {
                block.GetCurrentPiece()?.SetUpWhenIsTarget();
            }
            enemyGroup.enemyList[indexEnemyGroup].HandleMovement();
        }
    }

    public void SetTurn(Turn newTurn)
    {
        turn = newTurn;
        OnTurnChange?.Invoke(this, EventArgs.Empty);
    }
    public Turn GetTurn()
    {
        return turn;
    }
    public void AutomaticChangeTurn()
    {
        
        if (turn == Turn.Player)
        {
            SetTurn(Turn.Enemy);
        }
        else
        {
            if (txtCountNumberOfTimes != null)
            {
                countNumberOfTimes++;

                countNumberOfTimes %= 4;
                txtCountNumberOfTimes.text = "" + countNumberOfTimes;
                if (countNumberOfTimes == 2)
                {
                    TrapManager.Instance.OpenAllTraps();
                }
                else if (countNumberOfTimes == 0)
                {
                    TrapManager.Instance.CloseAllTraps();
                }
            }
            SetTurn(Turn.Player);
        }
    }
    public ChessSide GetCurrentChessSide()
    {
        return currentPiece.chessSide;
    }
    public BasePiece GetCurrentPiece()
    {
        return currentPiece;
    }
    public void SetCurrentPiece(BasePiece newPiece)
    {
        currentPiece = newPiece;
    }
    public async Task SelectPiece(BasePiece piece)
    {
        await ClearListBlockSelected();
        PredictionMoveContext context = new PredictionMoveContext();
        switch (piece.pieceType)
        {
            case PieceType.King:
                context.SetStrategy(new KingPredictMoveStrategy());
                break;
            case PieceType.Queen:
                context.SetStrategy(new QueenPredictMoveStrategy());
                break;
            case PieceType.Bishop:
                context.SetStrategy(new BishopPredictMoveStrategy());
                break;
            case PieceType.Rook:
                context.SetStrategy(new RookPredictMoveStrategy());
                break;
            case PieceType.Knight:
                context.SetStrategy(new KnightPredictMoveStrategy());
                break;
            default:
                break;
        }
        context.ExcuteStrategy();
    }
    public async Task ClearListBlockSelected()
    {
        foreach (Block block in blocksSelected)
        {
            block.blockState = BlockState.Normal;
            block.GetPanelUIConfirm().SetActive(false);
        }
        blocksSelected.Clear();
        await Task.Yield(); // Đợi đến khung hình tiếp theo để đảm bảo mọi thứ đã hoàn thành
    }
    public void ClearListBlockSelected(bool? type = true)
    {
        foreach (Block block in blocksSelected)
        {
            block.blockState = BlockState.Normal;
            block.GetPanelUIConfirm().SetActive(false);
        }
        blocksSelected.Clear();
    }
    private Block RandomSelectBlock()
    {
        if (blocksSelected.Count == 0) return null;
        foreach (Block block in blocksSelected)
        {
            BasePiece currentPiece = block.GetCurrentPiece();
            if (currentPiece != null && currentPiece.chessSide == ChessSide.Player)
            {
                return block;
            }
        }
        int id = UnityEngine.Random.Range(0, blocksSelected.Count);
        return blocksSelected[id];
    }
}
