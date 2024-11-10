using UnityEngine;

public class PieceGFX : MonoBehaviour
{
    private BasePiece logicPiece;

    private void Awake()
    {
        logicPiece = GetComponentInParent<BasePiece>();
    }

    private void OnMouseDown()
    {
        if (GameLogic.Instance.GetTurn() == Turn.Enemy)
        {
            return;
        }

        BasePiece currentPiece = GameLogic.Instance.GetCurrentPiece();
        if (currentPiece != logicPiece)
        {
            if (CanPress())
            {
                GameLogic.Instance.SetCurrentPiece(logicPiece);
                RunSelectPieceAsync(logicPiece); 
            }
        }
    }

    private bool CanPress()
    {
        return ((GameLogic.Instance.GetTurn() == Turn.Player && logicPiece.chessSide == ChessSide.Player)
                || (GameLogic.Instance.GetTurn() == Turn.Enemy && logicPiece.chessSide == ChessSide.Enemy));
    }

    private async void RunSelectPieceAsync(BasePiece piece)
    {
        await GameLogic.Instance.SelectPiece(piece);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (logicPiece.rb.isKinematic == false)
        {
        }
    }
}
