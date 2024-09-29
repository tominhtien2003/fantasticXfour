using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public LayerMask groundMask;
    public float speed;
    public float jumpForce;
    protected enum Direction
    {
        Left,
        Right,
        Forward,
        Backward,
        ForwardLeft,
        ForwardRight,
        BackwardLeft,
        BackwardRight
    }
    protected Dictionary<Direction, Vector3> directionDictionary = new Dictionary<Direction, Vector3>();
    protected Transform currentCharacter;
    protected Vector3 destination;
    public void InitDirectionDictionary()
    {
        directionDictionary = new Dictionary<Direction, Vector3>
        {
            { Direction.Left, Vector3.left },
            { Direction.Right, Vector3.right },
            { Direction.Forward, Vector3.forward },
            { Direction.Backward, Vector3.back },
            { Direction.ForwardLeft, (Vector3.forward + Vector3.left).normalized },
            { Direction.ForwardRight, (Vector3.forward + Vector3.right).normalized },
            { Direction.BackwardLeft, (Vector3.back + Vector3.left).normalized },
            { Direction.BackwardRight, (Vector3.back + Vector3.right).normalized }
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
    public void SetCharacter(Transform transformTarget)
    {
        currentCharacter = transformTarget;
    }
    public virtual void PredictionDirectionPlayer()
    {

    }
    public GameObject GetGameobjectStart()
    {
        if (currentCharacter == null)
        {
            return null;
        }
        if (Physics.Raycast(currentCharacter.position, Vector3.down, out RaycastHit hitInfo, 1f, groundMask))
        {
            if (hitInfo.collider != null)
            {
                return hitInfo.collider.gameObject;
            }
            else
            {
                Debug.LogWarning("GetGameobjectStart is null");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("GetGameobjectStart is null");
            return null;
        }
    }
    public GameObject GetObjectFromMousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {
                GameObject selectedObject = hit.collider.gameObject;
                return selectedObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            Debug.Log("You muse click mouse left");
            return null;
        }
    }
    public Vector3 GetMoveDirection()
    {
        Vector3 moveDirection = Vector3.zero;

        GameObject objectClicked = GetObjectFromMousePosition();
        GameObject startObject = GetGameobjectStart();
        if (startObject != null && objectClicked != null)
        {
            Debug.Log(objectClicked.name);
            moveDirection = objectClicked.transform.position - startObject.transform.position;
        }
        return moveDirection.normalized;
    }
    public void Move()
    {

    }
}
