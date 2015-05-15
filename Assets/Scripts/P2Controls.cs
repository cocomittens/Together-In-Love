using UnityEngine;
using System.Collections;

public class P2Controls : MonoBehaviour {
	
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
		if(Input.GetKey("right"))
		{
			transform.position = new Vector3(transform.position.x + xSpeed, transform.position.y, 0);
		}
		
		if(Input.GetKey("left"))
		{
			transform.position = new Vector3(transform.position.x - xSpeed, transform.position.y, 0);
		}
		if(Input.GetKey("up"))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + ySpeed, 0);
		}
		if(Input.GetKey("down"))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - ySpeed, 0);
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
 	 Application.LoadLevel( "MainMenu" ); 
	 }
 
	 void AddHighScore( int points )
 	{
  	if ( points > PlayerPrefs.GetInt ( "highScore" ) )
  	{
   PlayerPrefs.SetInt ( "highScore", points ); 
   PlayerPrefs.SetInt ( "newHighScore", 1 );
	}
	}

}