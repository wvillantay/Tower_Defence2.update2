using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    private Vector3 start_position; // vector3 for the start position
    private Vector3 end_position; // vector3 for the end position
    private float lerp_time = 0.65f; // how long it takes to lerp
    private float current_lerp_time = 0f; // how long the lerp has been going for 
    private bool should_move; // bool to determine whether or not we should move
    private float random; // float to hold a temporary random number
    private bool chosen; // bool to determine whether the random number has been chosen
    public void Move_HitMarker(Vector3 _start_pos) // function to start moving the hitmarker
    {
        start_position = _start_pos; // set the start position to the position passed in
        should_move = true; // set should move to true
    }

    private void Update() // every frame
    {
        if (should_move) // if we should move
        {
            if (!chosen) // if the position has not been chosen
            {
                random = Random.Range(1, 3); // set random to a random number between 1 and 2 (last number excluded)
                switch (random) // choose a different outcome depending on the value of random
                {
                    case 1:
                        random = Random.Range(-0.05f, 0.05f); // choose a random number between -0.05f an 0.05f
                        end_position = new Vector3(start_position.x - 0.5f, start_position.y + random, 0); // move the hitmarker left and up or down diagonally
                        break;
                    case 2:
                        random = Random.Range(-0.05f, 0.05f); // choose a random number between -0.05f an 0.05f
                        end_position = new Vector3(start_position.x + 0.5f, start_position.y + random, 0); // move the hitmarker right and up or down diagonally
                        break;
                }
                chosen = true; // set chosen to true
            }
                       
            current_lerp_time += Time.deltaTime; // increment the current lerp time by delta time
            if (current_lerp_time >= lerp_time) // if the current lerp time is greater than or equal to the lerp time
            {
                current_lerp_time = lerp_time; // set the current lerp time to the lerp time 
            }

            float percentage = current_lerp_time / lerp_time; // declare a float equal to the current lerp time divided by the lerp time
            transform.position = Vector3.Lerp(start_position, end_position, percentage); // lerp the hitmarkers position via the start position end position and float we declared above

            if (transform.position == end_position) // if the hitmarker has reached it's destination
            {
                Destroy(transform.gameObject); // destroy the hitmarker
            }
        }
    }
}
