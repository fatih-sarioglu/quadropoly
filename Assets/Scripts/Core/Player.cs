using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    [Header("Identity")]
    public int PlayerID; // Unique ID for networking 
    public string PlayerName;
    public string TokenName; // "Scottie Dog", "Top Hat"

    [Header("Finances")]
    public int Balance = 1500; // Starting money
    public bool IsBankrupt = false;

    [Header("Position & Movement")]
    public int CurrentPosition = 0; // 0 is GO
    public int ConsecutiveDoubles = 0; // Track for "3 doubles = Jail" rule

    [Header("Jail Status")]
    public bool IsInJail = false;
    public int TurnsInJail = 0; // Maximum 3 turns
    public int GetOutOfJailCards = 0; // From Chance/Community Chest

    [Header("Assets")]
    // We store IDs or indices of properties to keep the network payload light
    public List<int> OwnedPropertyIndices = new List<int>();

    // Indices of properties currently mortgaged
    public List<int> MortgagedPropertyIndices = new List<int>();

    // Constructor to initialize a new player
    public Player(int id, string name, string token)
    {
        this.PlayerID = id;
        this.PlayerName = name;
        this.TokenName = token;
        this.Balance = 1500;
        this.CurrentPosition = 0;
        this.IsInJail = false;
        this.IsBankrupt = false;
    }

    /// <summary>
    /// Updates balance and checks for bankruptcy.
    /// </summary>
    public void AdjustBalance(int amount)
    {
        Balance += amount;
        if (Balance < 0)
        {
            // Logic for handling potential bankruptcy will go here
        }
    }

    /// <summary>
    /// Moves the player. Logic for passing GO ($200) should be handled by the BoardManager
    /// using this position update.
    /// </summary>
    public void Move(int spaces)
    {
        if (IsInJail) return; // Cannot move normally while in Jail

        CurrentPosition = (CurrentPosition + spaces) % 40; // 40 total spaces
    }

    /// <summary>
    /// Sends player directly to jail without passing GO.
    /// </summary>
    public void GoToJail()
    {
        CurrentPosition = 10; // Jail space index
        IsInJail = true;
        TurnsInJail = 0;
        ConsecutiveDoubles = 0;
    }
}
