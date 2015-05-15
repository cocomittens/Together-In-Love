using UnityEngine;
using System.Collections;

public class GameWorld : MonoBehaviour {
	public int notes = 0;
	public int ammo = 20;
	public float distance = 100f;
	public GameObject player1;
	public GameObject player2;
	public GameObject playerConnected;
    public GUIStyle guiStyle;
	public GameObject aloneTrack;
	public GameObject togetherTrack0;
	public GameObject togetherTrack1;
	public GameObject togetherTrack2;
	public GameObject togetherTrack3;
	public GameObject togetherTrack4;
	public GameObject togetherTrack5;
	public GameObject finaleTrack;
	public GameObject stingTrack1;
	public GameObject stingTrack2;
	public GameObject stingTrack3;
	public GameObject stingTrack4;
	public GameObject stingTrack5;
	public GameObject bird;
	public static float xRange = Random.Range (-25.0f, 30.0f);
	public static float yRange = Random.Range (-25.0f, 30.0f);
	public Vector3 position = new Vector3 (xRange, yRange, 0);
	public Quaternion rotation = Quaternion.Euler(0, 90, 90);
	private float togetherDistance = 6;
	public Material togetherClosestMaterial;
	public Material togetherCloseMaterial;
	public Material togetherAlmostMaterial;
	public Material BirdMat1;
	public Material BirdMat2;
	public Material BirdMat3;
	public Material BirdMat4;
	public Material BirdMat5;
	private static int num_of_each_bird = 2;
	private static float birdVolumeIncrement = 1/(num_of_each_bird+1);
	private int numBird1 = num_of_each_bird;
	private int numBird2 = num_of_each_bird;
	private int numBird3 = num_of_each_bird;
	private int numBird4 = num_of_each_bird;
	private int numBird5 = num_of_each_bird;
	public GameObject endCredits;
	private bool gameEnd = false;
	

	private enum ClosenessType {
		Lonely,
		Close,
		Closer,
		Connected
	};
	private ClosenessType closeness;

	// Use this for initialization
	void Start () {
		aloneTrack.audio.loop = true;
		aloneTrack.audio.volume = 1;
		// Begin alone track
		aloneTrack.audio.Play();
		togetherTrack0.audio.loop = true;
		togetherTrack0.audio.volume = 1;	// Baseline track has full volume
		togetherTrack1.audio.loop = true;
		togetherTrack1.audio.volume = birdVolumeIncrement;	
		togetherTrack2.audio.loop = true;
		togetherTrack2.audio.volume = birdVolumeIncrement;	
		togetherTrack3.audio.loop = true;
		togetherTrack3.audio.volume = birdVolumeIncrement;	
		togetherTrack4.audio.loop = true;
		togetherTrack4.audio.volume = birdVolumeIncrement;	
		togetherTrack5.audio.loop = true;
		togetherTrack5.audio.volume = birdVolumeIncrement;	
		
		// Player 1 and player 2 can't collect birds, only playerConnected
		player1.collider.enabled = false;
		player2.collider.enabled = false;
		// Don't enable playerConnected here, wait until P1 & P2 are close
		playerConnected.renderer.enabled = false; // Can't see playerConnected yet
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey (KeyCode.Escape)){
			Application.LoadLevel ("MainMenu");
		}
		if ( !gameEnd ) {
		
		distance = Vector3.Distance(player1.transform.position, player2.transform.position);
		if(distance <= 0.5*togetherDistance){
			// CONNECTED
			if (closeness != ClosenessType.Connected ) {	// We weren't connected, now we are
				closeness = ClosenessType.Connected; 				// Set the closeness val	
				playerConnected.renderer.material = togetherClosestMaterial;	// Show correct material
			}
		}
		else if(distance <= 0.75*togetherDistance){
			// CLOSER
			if ( closeness != ClosenessType.Closer ) {
				closeness = ClosenessType.Closer; 				// Set the closeness val	
				playerConnected.renderer.material = togetherCloseMaterial; 	// Show correct material
			}
		}
		else if(distance <= 0.875*togetherDistance){
			// CLOSE
			if ( closeness != ClosenessType.Close ) {	// We weren't close before
				if ( closeness == ClosenessType.Lonely ) {	// We were lonely
					// Change which music we play
					// Pause Alone music
					aloneTrack.audio.Pause();
					// Play together music
					PlayTogether();
					// Stop rendering player 1 & 2, start rendering playerConnected
					player1.renderer.enabled = false;
					player2.renderer.enabled = false;
					playerConnected.renderer.enabled = true;
					playerConnected.collider.enabled = true; // Now we can start collecting things.
				}
				closeness = ClosenessType.Close;  				// Set the closeness val	
				playerConnected.renderer.material = togetherAlmostMaterial; 	// Show correct material
			}
		}
		else if(distance >= togetherDistance){
			// LONELY
			if ( closeness != ClosenessType.Lonely ) {	// We weren't lonely before
				closeness = ClosenessType.Lonely;
				// Change the music we play
				// Pause Together music
				PauseTogether();
				// Play alone track
				aloneTrack.audio.Play();
				// Revert back to the two broken hearts icons
				player1.renderer.enabled = true;
				player2.renderer.enabled = true;
				playerConnected.renderer.enabled = false;
				playerConnected.collider.enabled = false;	// Turn off collider				
			}
		}
		
		// Find out where the together Player icon should be
		// It's at the mid-point of P1 and P2's position
		Vector3 newP3position = Vector3.Lerp(player1.transform.position, player2.transform.position, 0.5f);
		playerConnected.transform.position = new Vector3( newP3position.x,newP3position.y,0);
		}
	

 	}
	public void SpawnBird(){
		if ( !gameEnd ) {
		if (numBird1 > 0 || numBird2 > 0 || numBird3 > 0 || numBird4 > 0 || numBird5 > 0 ) {
			xRange = Random.Range (-30.0f, 40.0f);
			yRange = Random.Range (-30.0f, 40.0f);
			position = new Vector3 (xRange, yRange, 0);
			// Choose random number
			GameObject GO = null;
			bool createdBird = false;
			while (createdBird == false ) {
				float ranNumFloat = Random.Range (1,6);
				int ranNum = Mathf.FloorToInt(ranNumFloat);
				if (ranNum ==1 && numBird1 > 0 ) {
					createdBird = true;
					bird.renderer.material = BirdMat1;
					GO = Instantiate (bird, position, rotation) as GameObject;
					numBird1 --;
					stingTrack1.audio.Play();
					// Increase volume of audio track
						togetherTrack1.audio.volume += birdVolumeIncrement;
					if(togetherTrack1.audio.volume <1) {
						togetherTrack1.audio.volume = 1;
					}
						
				}
				else if (ranNum ==2 && numBird2 > 0 ) {
					// Create type 2 bird
					createdBird = true;
					bird.renderer.material = BirdMat2;
					GO = Instantiate (bird, position, rotation) as GameObject;
					numBird2 --;
					stingTrack2.audio.Play();
					// Increase volume of audio track
					togetherTrack2.audio.volume += birdVolumeIncrement;
					if(togetherTrack2.audio.volume <1) {
						togetherTrack2.audio.volume = 1;
					}
				}
				else if (ranNum ==3 && numBird3 > 0 ) {
					// Create type 3 bird
					createdBird = true;
					bird.renderer.material = BirdMat3;
					GO = Instantiate (bird, position, rotation)as GameObject;
					numBird3 --;
					stingTrack3.audio.Play();
					// Increase volume of audio track
					
					togetherTrack3.audio.volume += birdVolumeIncrement;
					if(togetherTrack3.audio.volume <1) {
						togetherTrack3.audio.volume = 1;
					}
				}
				else if (ranNum ==4 && numBird4 > 0 ) {
					// Create type 4 bird
					createdBird = true;
					bird.renderer.material = BirdMat4;
					GO = Instantiate (bird, position, rotation)as GameObject;
					numBird4 --;
					stingTrack4.audio.Play();
					// Increase volume of audio track			
						togetherTrack4.audio.volume += birdVolumeIncrement;
					if(togetherTrack4.audio.volume >1) {
						togetherTrack4.audio.volume = 1;
					}
				}
				else if (ranNum ==5 && numBird5 > 0 ) {
					// Create type 5 bird
					createdBird = true;
					bird.renderer.material = BirdMat5;
					GO = Instantiate (bird, position, rotation)as GameObject;
					numBird5 --;
					stingTrack5.audio.Play();
					// Increase volume of audio track
						togetherTrack5.audio.volume+= birdVolumeIncrement;
					if(togetherTrack5.audio.volume > 1) {
						togetherTrack5.audio.volume = 1;
					}
				}
				if(GO!= null) {
					GO.transform.eulerAngles = new Vector3(0, 0, 180);
				}
				
			}
		}
		else {
			// Run out of birds
			// Trigger end
			PauseTogether();
			aloneTrack.audio.Stop();
			finaleTrack.audio.Play();
			endCredits.renderer.enabled = true;
			gameEnd = true;
		}
		}
	}
	public void PauseTogether() {
		togetherTrack0.audio.Pause();
		togetherTrack1.audio.Pause();
		togetherTrack2.audio.Pause();
		togetherTrack3.audio.Pause();
		togetherTrack4.audio.Pause();
		togetherTrack5.audio.Pause();
	}
	public void PlayTogether() {
		togetherTrack0.audio.Play();
		togetherTrack1.audio.Play();
		togetherTrack2.audio.Play();
		togetherTrack3.audio.Play();
		togetherTrack4.audio.Play();
		togetherTrack5.audio.Play();

	}
}