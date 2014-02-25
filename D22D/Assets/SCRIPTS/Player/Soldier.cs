using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour {
	
	public GameObject opponent;

	public int health = 100;
	private int maxHealth = 100;

	public static int strength = 10;
	public static int dexteriry = 10;
	public static int magic = 10;

	public static int vitality = 20;
	public static int intellect = 10;


	private UISlider healthBar;
	
	public float attackSpeed;
	public double impactTime;

	private bool impacted;

	private float randomAttackNumber;
	public AnimationClip attack;
	public AnimationClip attack2;
	public AnimationClip die;

	public float meleeRange;

	public float projectileOffset;

	// Use this for initialization
	void Start () 
	{
		health = vitality*10;
		maxHealth = health;
		animation[attack.name].speed = attackSpeed/5;
		animation[attack2.name].speed = attackSpeed/10;
		healthBar = transform.FindChild("HealthBar").GetComponent<UISlider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	


		dieMethod ();
	}

	public void attackAbility(string abilityKey, AnimationClip anim, int stat, int scaling, bool isRanged, Transform projectile)
	{
		if(Input.GetButton (abilityKey))
		{
			animation.Play(anim.name);
			
			ClickToMove.attack = true;

			if(opponent!=null)
			{
				if(!isRanged&&inRange ())
				{
					transform.LookAt (opponent.transform.position);
				}
			}
				
		}
		
		if(animation[anim.name].time>0.8*animation[anim.name].length)
		{
			ClickToMove.attack = false;
			impacted = false;
		}
		
		impact(anim, strength*scaling, isRanged, projectile);

	}
	/**
	public void attackAbility(string abilityKey, AnimationClip anim, int stat, int scaling, bool isRanged)
	{
		if(Input.GetKey(KeyCode.LeftShift) && Input.GetButton (abilityKey))
		{
			animation.Play(anim.name);
			
			ClickToMove.attack = true;
			
			if(opponent!=null)
			{
				if(!isRanged&&inRange ())
				{
					transform.LookAt (opponent.transform.position);
				}
			}
			
		}
		
		if(animation[anim.name].time>0.8*animation[anim.name].length)
		{
			ClickToMove.attack = false;
			impacted = false;
		}
		
		impact(anim, strength*scaling, isRanged);
		
	}
	**/

	void impact(AnimationClip anim, int damage, bool isRanged, Transform projectile)
	{
		if(animation.IsPlaying(anim.name)&&!impacted)
		{
			if(animation[anim.name].time>animation[anim.name].length*impactTime&&(animation[anim.name].time<0.8*animation[anim.name].length))
			{
				if(isRanged)
				{
					Instantiate (projectile, new Vector3(transform.position.x, transform.position.y+projectileOffset, transform.position.z), transform.rotation);
				}
				if(opponent!=null)
				{
					if(!isRanged&&inRange())
					{
						opponent.GetComponent<Mob>().getHit (damage);
					}
				}
				impacted = true;
			}
		}
	}

	bool inRange()
	{

			if(Vector3.Distance (opponent.transform.position, transform.position)<=meleeRange)
			{
				return true;
			}
			else
			{
				return false;
			}
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

	bool isDead()
	{
		if(health<=0)
		{
			return true;
		}
		else
		{
			return false;
		}

	}

	void dieMethod()
	{
		if(isDead())
		{
			animation.CrossFade(die.name);
		}
	}
}
