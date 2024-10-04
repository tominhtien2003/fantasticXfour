using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private static GameLogic instance;
    public static GameLogic Instance { get { return  instance; } }

    private BasePiece currentPiece;
    private Turn turn;

    public List<Block> blocksSelected = new List<Block> ();
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
    }
    private void Start()
    {
        turn = Turn.Player;
    }
    public void SetTurn(Turn newTurn)
    {
        turn = newTurn;
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
            SetTurn(Turn.Enemy);
        }
    }
    public BasePiece GetCurrentPiece()
    {
        return currentPiece;
    }
    public void SetCurrentPiece(BasePiece newPiece)
    {
        currentPiece = newPiece;
    }
    public void SelectPiece(BasePiece piece)
    {
        ClearListBlockSelected();
        PredictionMoveContext context = new PredictionMoveContext();
        switch (piece.pieceType)
        {
            case PieceType.King:
                context.SetStrategy(new KingPredictMoveStrategy());
                break;
            case PieceType.Queen:
                break;
            case PieceType.Bishop:
                break;
            case PieceType.Rook:
                context.SetStrategy(new  RookPredictMoveStrategy());
                break;
            case PieceType.Knight:
                break;
            default:
                break;
        }
        context.ExcuteStrategy();
    }
    public void ClearListBlockSelected()
    {
        foreach(Block block in blocksSelected)
        {
            block.blockState = BlockState.Normal;
            block.GetPanelUIConfirm().SetActive(false);
        }
        blocksSelected.Clear();
    }
}
