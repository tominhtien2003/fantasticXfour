using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public bool isTrapOpened;
    private static TrapManager instance;    
    public static TrapManager Instance { get { return instance; } }
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
    public void OpenAllTraps()
    {
        isTrapOpened = true;
        foreach (var trap in BaseTrap.traps)
        {
            Block block = trap.GetComponentInParent<Block>();
            if (block!=null && block.GetCurrentPiece() != null)
            {
                block.GetCurrentPiece().SetUpWhenIsTarget();
                block.GetCurrentPiece()?.TurnOffSelf2(.5f);
                block.SetCurrentPiece(null);

                float timeDelay = 1f;
                if (GameLogic.Instance.GetTurn() == Turn.Player)
                {
                    GameLogic.Instance.Invoke("AutomaticChangeTurn", timeDelay);
                }
                else
                {
                    GameLogic.Instance.Invoke("AutomaticChangeTurn", 0.5f);
                }
            }
            block.tag = "CanNotMove";
            trap.Open();
        }
    }

    public void CloseAllTraps()
    {
        isTrapOpened = false;
        foreach (var trap in BaseTrap.traps)
        {
            Block block = trap.GetComponentInParent<Block>();
            block.tag = "Untagged";
            trap.Close();
        }
    }
}
