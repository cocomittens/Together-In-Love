using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
 
 public float xSpeed = 0.1f;
 public float ySpeed = 0.1f;
 
 public float originX;
 public float originY;
 
 public float maxX = 20f;
 public float maxY = 20f;
 
 public bool movingUp = true;
 public bool movingDown = false;
 
 public GameObject bird;
 public GameObject player;	
 public GameWorld gameWorld;

	public GameObject stingTrack1;
	public GameObject stingTrack2;
	public GameObject stingTrack3;
	public GameObject stingTrack4;
	public GameObject stingTrack5;

 // Use this for initialization
 void Start () {
  originX = transform.position.x;
  originY = transform.position.y;
  player = GameObject.Find ("Player1");
  gameWorld = GameObject.Find ("GameWorld").GetComponent<GameWorld>();
 }
 

 // Update is called once per frame
 void Update () {
  if ( movingUp )
  {
   transform.position = new Vector3( transform.position.x, transform.position.y + ySpeed, 0 );
   float distance =  transform.position.y - originY;
   if ( distance >= maxY )
   {
    movingUp = false;
    movingDown = true;
   }
  }
		
  if ( movingDown )
  {
   transform.position = new Vector3( transform.position.x, transform.position.y - ySpeed, 0 );
   float distance =  originY - transform.position.y;
   if ( distance >= maxY )
   {
    movingUp = true;
    movingDown = false;
   }
  }


 }
	
 void OnCollisionEnter( Collision collision )
 {
  GameObject hitObject = collision.contacts[0].otherCollider.transform.gameObject;
  if ( hitObject.tag == "Player1" )
  {
   player.SendMessage("Hit");
  }
 }
 void OnTriggerEnter(Collider collider){
	Destroy(gameObject);
	gameWorld.SpawnBird ();
	}

}