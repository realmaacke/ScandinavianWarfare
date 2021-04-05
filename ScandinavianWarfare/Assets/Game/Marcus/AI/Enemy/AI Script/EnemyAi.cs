using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
	[Header("EnemyAI")]
	public NavMeshAgent agent;

	[Header("Transforms")]
	public Transform player;

	[Header("Layers")]
	public LayerMask whatIsGround, whatIsPlayer;

	 [Header("Animations")]
	public Animator anim;
	public GameObject Enemy;

	 [Header("Patroling the Map Variables ")]
	public Vector3 walkPoint;
	bool walkPointSet;
	public float walkPointRange;

	[Header("Attacking Player")]
	public float timeBetweenAttacks;
	public float fireRate = 1f;
	private float firecountdown = 0f;
	bool isAttacking;

	public Transform BulletPrefab;
	public Transform FirePoint;
	public float BulletSpeed;

	[Header("Enemy Health")]
	public int EnemyHealth = 100;
	bool isDead;

	public GameObject impactEffect; // Blood Effect When hit
	public GameObject HitPoint;

	[Header("Respawn")]
	public float RespawnDelay;
	public Transform RespawnPoint;
	public GameObject EnemyPrefab;
	public bool isRespawned;

	[Header("Walk AI")]
	public float sightRange, attackRange;
	public bool playerInSightRange, playerInAttackRange;

	private void Awake()
	{		//Declaring Variables
		player = GameObject.Find("FPS").transform;
		agent = GetComponent<NavMeshAgent>();
	}

	public void Start()
	{
		isDead = false;
		isAttacking = false;
	}

	private void Update()
	{
		//checking if enemy is in sight for attacking or sighting the player
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
				// the 3 diffrent states of the enemy AI and what Void to call!
		if (!playerInSightRange && !playerInAttackRange && !isDead) Patroling();
		if (playerInSightRange && !playerInAttackRange && !isDead) ChasePlayer();
		if (playerInAttackRange && playerInSightRange && !isDead) AttackPlayer();

		firecountdown -= Time.deltaTime;

		if(EnemyHealth <= 0)
			Die();

		if (isDead)
			EnemyHealth = 100;
		
	}

	private void Patroling()
	{
		if (!walkPointSet) 
		{
			SearchWalkPoint();
			anim.SetBool("isWalking", true);
			anim.SetBool("isShooting", false);
		}

		if (walkPointSet)
		{
			agent.SetDestination(walkPoint);
			anim.SetBool("isWalking", true);
			anim.SetBool("isShooting", false);
		}
		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		// If enemy reach walkpoint bool is turned off
		if (distanceToWalkPoint.magnitude < 1f)
		{
			anim.SetBool("isWalking", false);
			walkPointSet = false;
		}
	}
	private void SearchWalkPoint()
	{
		float randomZ = Random.Range(-walkPointRange, walkPointRange); //Randomating the Search Walk points
		float randomX = Random.Range(-walkPointRange, walkPointRange);

		walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

		if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
			walkPointSet = true;
	}

	private void ChasePlayer()
	{
		if (!isDead)
		{
			agent.SetDestination(player.position);
			anim.SetBool("isWalking", true);
			anim.SetBool("isShooting", false);
		}
	}

	private void AttackPlayer()
	{
		agent.SetDestination(transform.position);
		anim.SetBool("isWalking", true);
		anim.SetBool("isShooting", true);
		transform.LookAt(player);

		if (!isAttacking)
		{
			Rigidbody Bullet;
			Bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation).GetComponent<Rigidbody>();
			Bullet.AddForce(transform.forward * 32f * BulletSpeed);
			isAttacking = true;
			Invoke(nameof(ResetAttack), 2f);
		}

		if (firecountdown <= 0)
	    {
			firecountdown = 1f / fireRate;
		}
	}


	private void ResetAttack()
	{
		isAttacking = false;
	}

	public void TakeDamage(int Damage)
	{
		EnemyHealth -= Damage;

		GameObject effectinst = (GameObject)Instantiate(impactEffect, HitPoint.transform);
		Destroy(effectinst, 2f);
	}

	public void Die()
	{
		isDead = true;
		Debug.Log("Died");
		anim.SetBool("isDead", true);	// Setting the animations values on or off
		anim.SetBool("isWalking", false);
		anim.SetBool("isShooting", false);
		StartCoroutine(Respawn());	// Starting the WaitFor Second Function
		Destroy(gameObject, 4.1f); // Waiting 4 seconds to Respawn
	}

	IEnumerator Respawn()
	{
		yield return new WaitForSeconds(RespawnDelay);
		Instantiate(EnemyPrefab, RespawnPoint.position, Quaternion.identity); // Spawning Prefab at RespawnPoint
		isDead = false;
		Debug.Log("Respawned");
	}
}

