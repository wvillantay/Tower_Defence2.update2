using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance; // making this class a singleton

    private void Awake() // before the script is started
    {
        Instance = this; // instance equals this class in this script
    }
    public void Play() // function to send the player to the game
    {
        SceneManager.LoadScene(1); // load the game scene (whatever scene is in index 1)
    }
}
