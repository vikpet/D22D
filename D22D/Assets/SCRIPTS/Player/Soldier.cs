using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour {

	public Transform projectilePrefab;
	public GameObject opponent;

	public int health = 100;
	private int maxHealth = 100;

	private UISlider healthBar;

	public int damage;
	public float attackSpeed;
	public double impactTime;

	private bool impacted;

	private float randomAttackNumber;
	public AnimationClip attack;
	public AnimationClip attack2;
	public AnimationClip die;

	public float meleeRange;

	public float projectileOffset;
	public bool isRanged;

	// Use this for initialization
	void Start () 
	{
		maxHealth = health;
		animation[attack.name].speed = attackSpeed/10;
		healthBar = transform.FindChild("HealthBar").GetComponent<UISlider>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))
		{
			animation.Play(attack.name);

			ClickToMove.attack = true;
				
			if(opponent!=null&&!isRanged&&inRange ())
			{
				transform.LookAt (opponent.transform.position);
			}
		}

		if(animation[attack.name].time>0.8*animation[attack.name].length)
		{
			ClickToMove.attack = false;
			impacted = false;
		}

		impact();
		dieMethod ();
	}


	void impact()
	{
		if(animation.IsPlaying(attack.name)&&!impacted)
		{
			if(animation[attack.name].time>animation[attack.name].length*impactTime&&(animation[attack.name].time<0.8*animation[attack.name].length))
			{
				if(isRanged)
				{

				Instantiate (projectilePrefab, new Vector3(transform.position.x, transform.position.y+projectileOffset, transform.position.z), transform.rotation);
				}
				if(!isRanged&&inRange())
				{
				opponent.GetComponent<Mob>().getHit (damage);
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
