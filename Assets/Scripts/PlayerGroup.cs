using System.Collections.Generic;
using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
    private EndGame endGame;
    public List<BasePiece> playerList = new List<BasePiece>();
    private bool hasPlayerLoseSound = false; 

    private void Start()
    {
        endGame = FindFirstObjectByType<EndGame>();
        BasePiece[] players = GetComponentsInChildren<BasePiece>(true);
        foreach (var player in players)
        {
            playerList.Add(player);
        }
    }

    private void Update()
    {
        if (!hasPlayerLoseSound && playerList.Count == 0)
        {
            endGame.Losee();
            AudioManager.Instance.PlaySFX("Lose");
            hasPlayerLoseSound = true; 
        }
    }
}
