using System.Collections.Generic;
using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
    public List<BasePiece> playerList = new List<BasePiece>();
    private void Start()
    {
        BasePiece[] players = GetComponentsInChildren<BasePiece>(true);
        foreach (var player in players)
        {
            playerList.Add(player);
        }
    }
}
