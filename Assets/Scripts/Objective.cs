using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
 
	public string levelToLoad = "level2";
 
 	// Use this for initialization
 	void Start () {
 
 	}
 
 	// Update is called once per frame
 	void Update () {
 
 	}
 
 	void OnTriggerEnter ( Collider collidee ) 
	{
  		if ( collidee.tag == "Player" )
  		{
   			Application.LoadLevel( levelToLoad );
  		}
	}
}