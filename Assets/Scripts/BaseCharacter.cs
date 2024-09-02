using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private enum Direction
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
    private Dictionary<Direction, Vector3> directionDictionary;
    private void Start()
    {
        InitDirection();
    }
    private void InitDirection()
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
    public void MoveForward(Vector3 destination)
    {

    }
    public void MoveBackward(Vector3 destination)
    {

    }
    public void MoveLeft(Vector3 destination)
    {

    }
    public void MoveRight(Vector3 destination)
    {

    }
    public void MoveForwardLeft(Vector3 destination)
    {

    }
    public void MoveForwardRight(Vector3 destination)
    {

    }
    public void MoveBackwardLeft(Vector3 destination)
    {

    }
    public void MoveBackwardRight(Vector3 destination)
    {

    }
    public void Jump(Vector3 Start, Vector3 destination)
    {

    }
}
