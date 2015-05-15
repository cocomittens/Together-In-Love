using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

 public GUIStyle guiStyle;
 
 // Use this for initialization
 void Start () {
 
 }
 
 // Update is called once per frame
 void Update () {
 
 }
 
 void OnGUI ()
 {
  GUILayout.BeginArea( new Rect( Screen.width/4, Screen.height/4,Screen.width/2, Screen.height/2 ) );
  {
   GUILayout.BeginVertical();
   {
    if ( Input.anyKey)
    {
     Application.LoadLevel("level1"); 
    }
    if ( Input.GetKey(KeyCode.Escape))
    {
     Application.Quit (); 
    }
    //int highScore = PlayerPrefs.GetInt ( "highScore" );
    //if ( highScore > 0 )
    //{
     //if ( PlayerPrefs.GetInt ( "newHighScore" ) == 1 )
     //{
     // GUILayout.Box ("NEW HIGH SCORE ACHIEVED!", guiStyle);
     // PlayerPrefs.SetInt ( "newHighScore", 0 );
    // }
     
    // GUILayout.Box ( "HIGH SCORE:" + highScore, guiStyle ); 
   // }
  // }
   GUILayout.EndVertical();
  }
  GUILayout.EndArea ();
		}
	}
}