using UnityEngine;
using System.Collections;

public class monster : MonoBehaviour {

	// Public Variables

	public int health;
	public GameObject player;
	public float walkSpeed;
	public float runSpeed;
	public int xSide = 0;
	public int zSide = 0;
	public float myXPos;
	public float myZPos;
	public float targetXPos;
	public float targetZPos;
	public float distanceToTarget;
	public float xRatio;
	public float zRatio;
	public float xVar1;
	public float xVar2;
	public float xVar3;
	public float zVar1;
	public float zVar2;
	public float zVar3;
	public float xZVar1;
	public Transform aiTarget;
	public bool idle;
	public bool chasePlayer;
	public bool runAway;

	//void AiMathToTarget();
	//void AiMoveToPoint();

	// Private Variables

	// Use this for initialization
	void Start ()
	{
		aiTarget = player.transform;
		idle = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (idle) 
		{
			Idle();
		}

		if (chasePlayer) 
		{
			ChasePlayer();
		}

		if (runAway) 
		{
			RunAway();
		}
	}

	void OnTriggerEnter (Collider trigger)
	{
		if (trigger.transform.tag == "Player" && idle == true)
		{
			chasePlayer = true;
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.transform.tag == "Weapon") 
		{
			runAway = true;
		}
	}

	void ChasePlayer()
	{
		Debug.Log ("Chase Player");
		AiMathToTarget();
		AiMoveToPoint();
	}

	void RunAway()
	{
		Debug.Log ("Run from Player");
	}

	void Idle()
	{
		Debug.Log ("Idle Walk");
	}

	void AiMoveToPoint()
	{
		gameObject.transform.rigidbody.velocity = new Vector3 (xRatio * runSpeed, 0, zRatio * runSpeed);
		//gameObject.transform.rigidbody.velocity.z = zRatio*runSpeed;

		Debug.Log ("Moving Player");
	}

	void AiMathToTarget()
	{
		xSide = 0;
		zSide = 0;
		myXPos = transform.position.x;
		myZPos = transform.position.z;
		targetXPos = aiTarget.position.x;
		targetZPos = aiTarget.position.z;
		
		if (transform.position.x > aiTarget.transform.position.x)
		{ 
			xSide--;
		}
		
		if (transform.position.x < aiTarget.transform.position.x)
		{
			xSide++;
		}
		
		if (transform.position.y > aiTarget.transform.position.y)
		{
			zSide--; 
		}
		
		if (transform.position.y < aiTarget.transform.position.y)
		{
			zSide++;
		}
		
		xVar1 = myXPos;
		
		if (transform.position.x < 0)
		{
			xVar1 = xVar1 * -1;
		}
		
		xVar2 = targetXPos;
		
		if (aiTarget.transform.position.x < 0)
		{
			xVar2 = xVar2 * -1;
		}
		
		xVar3 = xVar1 + xVar2;
		
		zVar1 = myZPos;
		
		if (transform.position.y < 0)
		{
			zVar1 = zVar1 * -1;
		}
		
		zVar2 = targetZPos;
		
		if (aiTarget.transform.position.y < 0)
		{
			zVar2 = zVar2 * -1;
		}
		
		zVar3 = zVar1 + zVar2;
		xZVar1 = xVar3 + zVar3;
		xRatio = xVar3 / xZVar1;
		zRatio = zVar3 / xZVar1;
		xRatio = xRatio * xSide;
		zRatio = zRatio * zSide;
		distanceToTarget = zVar3 * zVar3 + xVar3 * xVar3;
		distanceToTarget = distanceToTarget / distanceToTarget;
		
		if (xSide == 0 || zSide == 0)
		{
			if (xSide == 0)
			{
				xRatio = 0;
				distanceToTarget = zVar3;
			} 
			
			if (zSide == 0)
			{
				zRatio = 0;
				distanceToTarget = xVar3;
			}
		}

		Debug.Log ("Calculation Complete");
	}
}
