using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Economy : MonoBehaviour
{
    public static Economy Instance; // making economy a singleton
    public TMP_Text Amount; // getting a reference to the text that shows the amount of coins the player has
    private int coins; // int to keep track of the amount of coins a player has

    private void Awake() // before the script is started
    {
        Instance = this; // setting instance equal to this class
    }
    public void Add(int amount) // function to add coins to the player's "purse"
    {
        coins += amount; // adding the passed in int to the amount of coins
        Amount.text = coins.ToString(); // updating the amount text
    }

    public void Remove(int amount) // function to remove coins from the player's "purse"
    {
        coins -= amount; // removing the passed in int from the amount of coins

        if (coins < 0) // if there are less than 0 coins
            coins = 0; // set coins to 0

        Amount.text = coins.ToString(); // update the text
    }

    public bool TryPlaceTurret (bool value)
    {
        if (coins >= 100) // if the players has 100 or more coins
        {
            coins -= 100; // remove 100 coins from the player 
            if (coins < 0) // if the player has less than 0 coins
            {
                coins = 0; // set their coins to 0
            }

            value = true; // return true
        }

        Amount.text = coins.ToString(); // update the coin text
        return value; // return the final value
    }
}
