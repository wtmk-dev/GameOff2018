using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour {

	public Image background;
	public Button RetryButton;


	public void Retry(){
		SceneManager.LoadScene( "SampleScene" );
	}

}
