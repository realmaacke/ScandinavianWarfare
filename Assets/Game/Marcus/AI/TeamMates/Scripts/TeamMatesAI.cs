using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TeamMatesAI : MonoBehaviour
{
	[Header("EnemyAI")]
	public NavMeshAgent agent;

	[Header("Transforms")]
	public Transform Enemys;

	[Header("Layers")]
	public LayerMask whatIsGround, whatIsEnemy;

	[Header("Animations")]
	public Animator anim;

	[Header("Patroling the Map Variables ")]
	public Vector3 walkPoint;
	bool walkPointSet;
	public float walkPointRange;

	[Header("Attacking Enemys")]
	public float timeBetweenAttacks;

	public float fireRate = 1f;
	private float firecountdown = 0f;
	public GameObject Bullet;
	public float BulletSpeed = 100f;
	public Transform FirePoint;

	[Header("Respawn")]
	public float RespawnDelay;
	public Transform RespawnPoint;
	public GameObject TeamMatesPrefab;
	public bool isDead;
	public bool isRespawned;

	[Header("Walk AI")]
	public float sightRange, attackRange;
	public bool playerInSightRange, playerInAttackRange;

	private void Awake()
	{       //Declaring Variables
		Enemys = GameObject.Find("Enemys").transform;
		agent = GetComponent<NavMeshAgent>();
	}

	public void Start()
	{
		isDead = false;
	}

	private void Update()
	{
		//checking if enemy is in sight for attacking or sighting the player
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

		// the 3 diffrent states of the enemy AI and what Void to call!
		if (!playerInSightRange && !playerInAttackRange && !isDead) Patroling();
		if (playerInSightRange && !playerInAttackRange && !isDead) ChaseEnemy();
		if (playerInAttackRange && playerInSightRange && !isDead) AttackEnemy();

		firecountdown -= Time.deltaTime;
	}

	private void Patroling()
	{
		if (!walkPointSet)
		{
			SearchWalkPoint();
			anim.SetBool("TeamWalk", true);
			anim.SetBool("TeamShoot", false);
		}

		if (walkPointSet)
		{
			agent.SetDestination(walkPoint);
			anim.SetBool("TeamWalk", true);
			anim.SetBool("TeamShoot", false);
		}

		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		// If enemy reach walkpoint bool is turned off
		if (distanceToWalkPoint.magnitude < 1f)
		{
			anim.SetBool("TeamWalk", false);
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

	private void ChaseEnemy()
	{
		if (!isDead)
		{
			agent.SetDestination(Enemys.position);
			anim.SetBool("TeamWalk", true);
			anim.SetBool("TeamShoot", false);
		}
	}

	private void AttackEnemy()
	{
		agent.SetDestination(transform.position);
		anim.SetBool("TeamWalk", true);
		anim.SetBool("TeamShoot", true);
		transform.LookAt(Enemys);

		if (firecountdown <= 0)
		{
			Shoot();
			firecountdown = 1f / fireRate;
		}
	}

	void Shoot()
	{
		GameObject instBullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
		Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
		instBulletRigidBody.AddForce(Vector3.forward * BulletSpeed);
	}

	public void Die()
	{
		isDead = true;
		Debug.Log("Died");
		anim.SetBool("TeamDead", true);   // Setting the animations values on or off
		anim.SetBool("TeamWalk", false);
		anim.SetBool("TeamShoot", false);
		StartCoroutine(Respawn());  // Starting the WaitFor Second Function
		Destroy(gameObject, 4.1f); // Waiting 4 seconds to Respawn
	}



	IEnumerator Respawn()
	{
		yield return new WaitForSeconds(RespawnDelay);
		Instantiate(TeamMatesPrefab, RespawnPoint.position, Quaternion.identity); // Spawning Prefab at RespawnPoint
		isDead = false;
		Debug.Log("Respawned");
	}
}
