using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float xSpeed = 0.5f;
	public int penetrations = 0;
	public GameObject player; 
	// Use this for initialization
	void Start () {
		player=GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x + xSpeed, transform.position.y, 0);
	}
	void OnCollisionEnter( Collision collision )
 	{
 	 GameObject hitObject = collision.contacts[0].otherCollider.transform.gameObject;
 	 if ( hitObject.tag == "Enemy" )
	  {
 	  GameObject.Destroy ( collision.gameObject );
			if(penetrations <=0){
				GameObject.Destroy (gameObject);
			}
			player.SendMessage ("AddPoints");
 	 }
	 }
}
