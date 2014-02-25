using UnityEngine;
using System.Collections;

public class TestAbility : MonoBehaviour {

	public string abilityKeybind;
	public AnimationClip animation;
	public string damageStat;
	public int damageScaling;
	public Transform projectilePrefab;
	public bool bIsRanged;
	public Transform player;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		player.GetComponent<Soldier>().attackAbility(abilityKeybind, animation, Soldier.strength, damageScaling, bIsRanged, projectilePrefab);
	}
}
