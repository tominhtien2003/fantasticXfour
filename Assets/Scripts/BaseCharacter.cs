using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public LayerMask groundMask;
    public float speed;
    public float jumpForce;
    protected enum Direction
    {
        Left,           // Trái
        Right,          // Phải
        Forward,        // Trước
        Backward,       // Sau
        ForwardLeft,    // Chéo lên trái
        ForwardRight,   // Chéo lên phải
        BackwardLeft,   // Chéo xuống trái
        BackwardRight   // Chéo xuống phải
    }
    protected Dictionary<Direction, Vector3> directionDictionary = new Dictionary<Direction, Vector3>();
    protected Transform currentCharacter;
    protected Vector3 destination;
    protected Collider _collider;
    private void Start()
    {
        Debug.Log("aa");
        InitDirectionDictionary();
    }
    private void InitDirectionDictionary()
    {
        directionDictionary = new Dictionary<Direction, Vector3>
        {
            { Direction.Left, Vector3.left },
            { Direction.Right, Vector3.right },
            { Direction.Forward, Vector3.forward },
            { Direction.Backward, Vector3.back },
            { Direction.ForwardLeft, Vector3.forward + Vector3.left },
            { Direction.ForwardRight, Vector3.forward + Vector3.right },
            { Direction.BackwardLeft, Vector3.back + Vector3.left },
            { Direction.BackwardRight, Vector3.back + Vector3.right }
        };
    }
    public void MoveForward()
    {

    }
    public void MoveBackward()
    {

    }
    public void MoveLeft()
    {

    }
    public void MoveRight()
    {

    }
    public void MoveForwardLeft()
    {

    }
    public void MoveForwardRight()
    {

    }
    public void MoveBackwardLeft()
    {

    }
    public void MoveBackwardRight()
    {

    }
    public void Jump()
    {

    }
    public void SetCollider(Collider colliderTarget)
    {
        _collider = colliderTarget;
    }
    public void SetCharacter(Transform transformTarget)
    {
        currentCharacter = transformTarget;
    }
    public virtual void PredictionDirectionPlayer()
    {

    }
    public GameObject GetGameobjectCurrent()
    {
        if (Physics.Raycast(currentCharacter.position, Vector3.down, out RaycastHit hitInfo, 10f, groundMask))
        {
            if (hitInfo.collider != null)
            {
                return hitInfo.collider.gameObject;
            }
            else
            {
                Debug.LogWarning("currentBlock is null");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("currentBlock is null");
            return null;
        }
    }
}
