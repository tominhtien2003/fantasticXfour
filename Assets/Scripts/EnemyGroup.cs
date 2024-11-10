using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public List<BasePiece> enemyList = new List<BasePiece>();
    private void Start()
    {
        BasePiece[] enemys = GetComponentsInChildren<BasePiece>(true);
        foreach (var enemy in enemys)
        {
            enemyList.Add(enemy);
        }
    }
}
