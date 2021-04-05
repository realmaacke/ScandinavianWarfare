using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{


	public GameObject FPS;
	public Transform spawnpoint;

	public int health;

	private void Start()
	{
		health = 100;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
			takedamage(10);

		if (health <= 0)
		{
			Die();
		}
	}

	void takedamage(float damage)
	{
		damage -= health;

	}
	public void Die()
	{
		StartCoroutine(Respawn());
	}

	IEnumerator Respawn()
	{
		yield return new WaitForSeconds(5f);
		Instantiate(FPS, spawnpoint.position, Quaternion.identity); // Spawning Prefab at RespawnPoint
		Debug.Log("Respawned");
	}





}
