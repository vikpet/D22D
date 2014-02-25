using UnityEngine;
using System.Collections;


public class Player_Projectiles : MonoBehaviour {

	public int speed;
	public int damage;
	public float lifeTime;
	public string attackTag;
	public GameObject enemyImpactEffect;
	public bool bDieOnCollision;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.forward*speed*Time.deltaTime);

		Destroy (gameObject,lifeTime);
	}
	

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			other.GetComponent<Mob>().getHit (damage);
			Instantiate (enemyImpactEffect, other.transform.position, other.transform.rotation);
			if(bDieOnCollision)
			{
			Destroy (gameObject);
			}
		}
		if(other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy")
		{
			Destroy(gameObject);
		}
	}
}
