using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	public int health;
	private int maxHealth;
	public float speed;
	public float range;
	public float aggroRange;
	public CharacterController controller;

	private UISlider healthBar;

	public float projectileOffset;

	public int damage;
	public double impactTime = 0.5;

	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip die;
	public AnimationClip attackClip;

	public Transform deathReplacement;
	private bool impacted;

	public bool bIsRanged;
	public Transform projectilePrefab;

	public Transform player;
	private Soldier opponent;



	// Use this for initialization
	void Start () 
	{
		maxHealth = health;
		opponent = player.GetComponent<Soldier> ();

		healthBar = transform.FindChild("HealthBar").GetComponent<UISlider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isDead ())
		{
			if(!inRange() && inAggroRange())
			{
				chase ();
			}
			else
			{
				animation.CrossFade(idle.name);
			}
			if(inRange()) 
			{
				//animation.CrossFade(idle.name);
				animation.Play(attackClip.name);
				attack();

				if(animation[attackClip.name].time > 0.9*animation[attackClip.name].length)
				{
					impacted = false;
				}
			}
		}
		else
		{
			dieMethod ();
		}
	}

	void attack()
	{
		if(animation[attackClip.name].time > animation[attackClip.name].length*impactTime && !impacted && animation[attackClip.name].time<0.9*animation[attackClip.name].length)
		{

			transform.LookAt(player.position);
			if(bIsRanged)
			{
				Instantiate (projectilePrefab, new Vector3(transform.position.x, transform.position.y+projectileOffset, transform.position.z), transform.rotation);
			}
			else
			{
				opponent.getHit(damage);
			}
			impacted = true;
		}
	}

	bool inRange()
	{

		if(Vector3.Distance (transform.position, player.position)<range)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool inAggroRange()
	{
		
		if(Vector3.Distance (transform.position, player.position)<aggroRange)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void chase()
	{
		transform.LookAt(player.position);
		controller.SimpleMove(transform.forward*speed);
		animation.Play(run.name);
	}
	void dieMethod()
	{
		Destroy(gameObject);

		//Transform dead = Instantiate (deathReplacement, transform.position, transform.rotation);
		/** animation.CrossFade(die.name);

		if(animation[die.name].time>0.9*animation[die.name].length)
		{
			Destroy (gameObject);
		} **/
	}

	bool isDead()
	{
		if (health <= 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void OnMouseOver()
	{
		player.GetComponent<Soldier>().opponent = gameObject;

	}

	public void getHit(int damage)
	{
		health = health - damage;

		if(health<0)
		{
			health = 0;
		}
		healthBar.sliderValue = ((float) health) / ((float) maxHealth);
		
		healthBar.ForceUpdate();
	}

}
