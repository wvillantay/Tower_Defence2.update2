using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static Tower Instance; // making tower a singleton
    private int health; // int to keep track of the tower's health
    public GameObject heart_1; // reference to the first heart
    public GameObject heart_2; // reference to the second heart
    public GameObject heart_3; // reference to the third heart
    private void Awake() // before the script is started
    {
        Instance = this; // setting instance equal to this class
    }
    private void Start() // when the script is started
    {
        health = 3; // set health to 3
    }
    public void OnTriggerEnter2D(Collider2D collision) // when the enemy reaches the tower
    {
        Destroy(collision.gameObject); // get rid of the enemy
        RemoveHealth(1); // remove 1 health from the tower
    }
    public void RemoveHealth(int amount) // function to remove health from the tower
    {
        health -= amount; // setting health equal to health minus the amount passed in
        if (health <= 0) // if health is less than or equal to 0
        {
            health = 0; // set health to 0
            heart_1.SetActive(false); // disable the first heart
            EventManager.Instance.End_Game(); // end the game
            Destroy(transform.gameObject); // destroy the player
        }
        else if (health == 2) // if the player has 2 health left
        {
            heart_3.SetActive(false); // disable the third heart
        }
        else if (health == 1) // if the player has 1 health left
        {
            heart_2.SetActive(false); // disable the second heart
        }
    }
}
