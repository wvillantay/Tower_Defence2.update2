using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] Points; // public gameobject array to hold all of the points
    public GameObject enemy; // gameobject to get a reference to the enemy prefab
    private void Start() // when the script starts
    {
        StartCoroutine(SpawnEnemy()); // start the spawn enemy coroutine
    }

    private IEnumerator SpawnEnemy() // coroutine to spawn an enemy every [NUMBER] seconds
    {
        while (true) // infinite loop
        {
            yield return new WaitForSeconds(1.5f); // wait 1.5 seconds
            GameObject _enemy = Instantiate(enemy); // instantiate an enemy and get a reference to it
            _enemy.GetComponent<Enemy>().ReceivePoints(Points); // get the enemy script on the gameobject we just instantiated and call the receive points function inside of it to give it the array of points
            _enemy.name = "Enemy"; // rename the gameobject to "Enemy"
        }
    }
}
