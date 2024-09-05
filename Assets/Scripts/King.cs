using System;
using System.Collections.Generic;
using UnityEngine;

public class King : BaseCharacter
{
    private List<GameObject> objectChose;
    private List<Vector3> directionKing = new List<Vector3>();
    private void Start()
    {
        SetCharacter(transform);
        InitDirection();
        PredictionDirectionPlayer();
    }
    private void InitDirection()
    {
        Debug.Log(directionDictionary.Count);
        foreach (var direction in directionDictionary)
        {
            Debug.Log(direction.Value);
            //directionKing.Add(direction.Value);
        }
    }
    public override void PredictionDirectionPlayer()
    {
        base.PredictionDirectionPlayer();
        foreach (Vector3 directionPlayer in directionKing)
        {
            GameObject currentObject = GetGameobjectCurrent();
            Vector3 direction = directionPlayer;
            while (currentObject != null)
            {
                Debug.Log("" + currentObject.name);
                if (Physics.Raycast(currentObject.transform.position, direction, out RaycastHit hitInfo, 2f))
                {
                    if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        currentObject = hitInfo.collider.gameObject;
                    }
                    else
                    {
                        if (Physics.Raycast(currentObject.transform.position + Vector3.up, direction, out RaycastHit hitInfoUp, 2f))
                        {
                            currentObject = hitInfoUp.collider.gameObject;
                        }
                        else if (Physics.Raycast(currentObject.transform.position - Vector3.up, direction, out RaycastHit hitInfoDown, 2f))
                        {
                            currentObject = hitInfoDown.collider.gameObject;
                        }
                        else
                        {
                            currentObject = null;
                        }
                    }
                }
                else
                {
                    currentObject = null;
                }
            }
        }
    }
}
