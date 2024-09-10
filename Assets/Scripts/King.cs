using System;
using System.Collections.Generic;
using UnityEngine;

public class King : BaseCharacter
{
    private List<GameObject> objectChose = new List<GameObject>();
    private List<Vector3> directionKing = new List<Vector3>();
    private void Start()
    {
        InitDirectionDictionary();
        SetCharacter(transform);
        InitDirection();
        PredictionDirectionPlayer();
    }
    private void InitDirection()
    {
        foreach (var direction in directionDictionary)
        {
            directionKing.Add(direction.Value);
        }
    }
    public override void PredictionDirectionPlayer()
    {
        base.PredictionDirectionPlayer();
        GameObject startObject = GetGameobjectStart();

        objectChose.Clear();

        Collider[] colliders = Physics.OverlapSphere(startObject.transform.position, 1f, groundMask);
        Debug.Log(colliders.Length);

        foreach (Collider collider in colliders)
        {
            if (collider != null && collider.gameObject != startObject)
            {
                objectChose.Add(collider.gameObject);
                Debug.Log("" + collider.gameObject.name + "");
            }
        }
    }
    #if DrawTriiger
    private void OnDrawGizmos()
    {
        GameObject startObject = GetGameobjectStart();
        if (startObject != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(startObject.transform.position, 1f);
        }
    }
    #endif
}
