using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventManager : MonoBehaviour
{
    public static EventManager Instance; // make eventmanager a singleton
    public Camera cam; // get a reference to our camera
    private bool damage_enemy_cooldown; // boolean to determine whether or not we are on cooldown for damaging the enemy
    public GameObject HitMarker_Holder; // gameobject that the hitmarkers are instantiated under
    public GameObject HitMarker; // gameobject reference to the hitmarker prefab
    public GameObject Game_Over; // reference to the gameover screen
    private bool is_game_over; // bool to determine whether the game is over or not
    private void Awake() // before the script is started
    {
        Instance = this; // setting instance equal to this class
    }
    private void Update() // every frame
    {
        if (!is_game_over) // if the game isn't over
        {
            bool left_click = Input.GetMouseButton(0); // bool to determine when we left click

            if (left_click && !damage_enemy_cooldown) // if we left click and we aren't on cooldown for damaging the enemy
            {
                StartCoroutine(Damage_Enemy_Cooldown()); // start the damage enemy cooldown
                Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // vector3 to get the position of the mouse on the screen
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y); // convert the mouse position to a vector2 (2D)

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero); // shoot a raycast at the mousepos2D and send the data to a Raycasthit2d 

                if (hit.collider != null) // if we hit something
                {
                    if (hit.transform.gameObject.tag == "Enemy") // if we hit an enemy
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().RemoveHealth(10); // remove 10 health from the enemy
                        GameObject _hit_marker = Instantiate(HitMarker, HitMarker_Holder.transform); // instantiate a new hitmarker and get a reference to it
                        _hit_marker.transform.gameObject.GetComponent<HitMarker>().Move_HitMarker(hit.transform.position); // call the move_hitmarker function inside the hit marker we just instantiated and pass in it's start position
                    }
                }
            }
        }       
    }
    private IEnumerator Damage_Enemy_Cooldown() // coroutine for the damage enemy cooldown
    {
        damage_enemy_cooldown = true; // enable the cooldown
        yield return new WaitForSeconds(0.25f); // wait 1/4 of a second
        damage_enemy_cooldown = false; // disable the cooldown
    }

    public void End_Game() // function to enable the gameover screen
    {
        Game_Over.SetActive(true); // enable the gameover screen
        is_game_over = true; // set the is game over function to true
    }

    public void Quit() // function to load back into the menu scene
    {
        SceneManager.LoadScene(0); // load into the menu scene
    }
}
