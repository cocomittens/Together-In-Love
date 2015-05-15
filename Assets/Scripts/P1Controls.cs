using UnityEngine;
using System.Collections;

public class P1Controls: MonoBehaviour {
	
	public float xSpeed = 0.1f;
	public float ySpeed = 0.1f;
	public int points = 0;
	public GameObject shooterProjectile;
	public int ammo = 20;
    public GUIStyle guiStyle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.D))
		{
			transform.position = new Vector3(transform.position.x + xSpeed, transform.position.y, 0);
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			transform.position = new Vector3(transform.position.x - xSpeed, transform.position.y, 0);
		}
		if(Input.GetKey(KeyCode.W))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + ySpeed, 0);
		}
		if(Input.GetKey(KeyCode.S))
		{ 
			transform.position = new Vector3(transform.position.x, transform.position.y - ySpeed, 0);
		}

	}
	void Fire ()
	{
		if(ammo > 0){
		Instantiate (shooterProjectile, transform.position, transform.rotation);
		ammo -= 1;
		}
		
		
	}
	
	 void AddPoints ()
	{
 	 points++;
	  ammo++;
	  PlayerPrefs.SetInt("currentPoints", points);
	 }
	
	void Hit ()
 	{
 	 AddHighScore(points);
 	 Application.LoadLevel( "MainMenu" ); 	 }
 
	 void AddHighScore( int points )
 	{
  	if ( points > PlayerPrefs.GetInt ( "highScore" ) )
  	{
   PlayerPrefs.SetInt ( "highScore", points ); 
   PlayerPrefs.SetInt ( "newHighScore", 1 );
	}
	}
	void OnGUI ()
 	{
  		//GUI.Box ( new Rect( 20,20,200,100 ), "NOTES:" + points, guiStyle );
 	 	//GUI.Box ( new Rect( 70,70,200,100 ), "AMMO:" + ammo, guiStyle );
 	}
}