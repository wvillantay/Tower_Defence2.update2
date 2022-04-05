using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTurrent : MonoBehaviour
{

	private Transform target;
	private Enemy targetEnemy;

	[Header("General")]

	public float range = 5f; //the range of the turrent

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;   //bullet
	public float fireRate = 1f;  //bullet shoots after 1 second
	private float fireCountdown = 0f;
	
	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy"; 

	//rotate towards the enemy
	public Transform partToRotate;
	public float turnSpeed = 10f; //speed that it turns towards enemy

	public Transform firePoint; //shootingpoint

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			//aims towards the enemy
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			
			//shoots whats closer towards teh turrent
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		}
		else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		
	}

	void LockOnTarget()
	{
		//position of enemy and our position will be calculated in position
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir); //looks to our enemy thats close to our torrent
		
		//torrent should be looking towards the enemy which is clsoer towards teh turrent
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}
	
	void Shoot()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		bullet Bullet = bulletGO.GetComponent<bullet>();

		if (Bullet != null)
			Bullet.Seek(target); //bullet gets the position of the enemy.
	}

	
}

