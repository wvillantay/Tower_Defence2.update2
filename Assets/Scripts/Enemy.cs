using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private GameObject[] Points; // array of the different points in the map
    private Vector3 start_position; // vector3 to hold the start position
    private Vector3 end_position; // vector3 to hold the end position
    private float lerp_time = 3f; // amount of time it takes to lerp through the changes
    private float current_lerp_time = 0f; // track how far we are into the lerp
    private int index; // what point are we on
    private int points; // amount of points
    private bool moving; // are we moving?
    private bool _set; // bool to check if the start position has been set
    public int health; // int to keep track of the health
    private int random; // int that holds a temporary random number
    public Image Progress; // enemies health progress bar

    private void Start() // when the script starts
    {
        points = Points.Length; // set points equal to the amount of points 
        health = 50; // set the health to 50
    }
    private void Update() // every frame
    {
        if (!moving) // if the enemy is not moving
        {          

            if (index == 0) // if index is equal to 0
            {
                index++; // add 1 to index
                moving = true; // set moving to true
            }
            else if (index != 14 && index != 0) // if index is not equal to 0 or 14
            {
                index++; // add 1 to index
                moving = true; // set moving to true
                _set = false; // set _set to false
            }

            current_lerp_time = 0; // reset the current lerp time
        }

        if (moving) // if moving is true
        {
            if (!_set) // if the start position has not been set
            {
                start_position = transform.position; // set the start position to the enemies position
                _set = true; // set set to true
            }

            if (index != 14) // if index is not equal to 14
            {
                end_position = Points[index].transform.position; // set end position to index index in the points array's position
            }

            current_lerp_time += Time.deltaTime; // increment the current lerp time by delta time
            if (current_lerp_time >= lerp_time) // if the current lerp time is greater than or equal to the lerp time
            {
                current_lerp_time = lerp_time; // set the current lerp time equal to the lerp time
            }

            float percentage = current_lerp_time / lerp_time; // declaring a new float which is equal to the current lerp time divided by the lerp time
            transform.position = Vector3.Lerp(start_position, end_position, percentage); // lerping the enemies position via the start position, end position and the float we declared above

            if (transform.position == end_position) // if the enemy has reached the point
            {
                moving = false; // set moving to false
            }
        }
    }
    public void ReceivePoints(GameObject[] _points) // function to receive points
    {
        Points = _points; // setting the points array to the array of points via the event manager
        points = Points.Length; // setting points to the amount of points
    }

    public void RemoveHealth(int amount) // function to remove health
    {
        health -= amount; // setting health equal to health minus the amount passed in
        UpdateDisplay();
        if (health <= 0) // if health is less than or equal to 0
        {
            health = 0; // health = 0
            random = Random.Range(15, 26); // get a random number between 15 and 25
            Economy.Instance.Add(random); // add that amount to the player's purse
            Destroy(transform.gameObject); // destroy the enemy
        }
    }

    private void UpdateDisplay()
    {
        Progress.rectTransform.sizeDelta -= new Vector2(8, 0); // decrement the width of the enemies health bar
    }
}
