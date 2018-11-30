using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DMView : MonoBehaviour {

	public static readonly string TAG = "DMView";
	public GameObject startButton;
	public GameObject goIntroScreen;
	public GameObject goGameOverScreen;
	private DM controller;
	private Image startImg;
	[SerializeField]
	private TextMeshProUGUI startText;

	void OnEnable( ){
		PlayerController.OnPlayerKilled += DisplayGameOVerScreen;
	}

	void OnDisable( ){
		PlayerController.OnPlayerKilled -= DisplayGameOVerScreen;
	}

	public void Init( DM controller ){
		this.controller = controller;
		startButton.SetActive( false );
	}

	public void ActivateUI( string name, bool isActive ){
		if( name == "Start" ){
			startButton.SetActive( isActive );
		}
	}

	public void StartScreenFade(){
		goIntroScreen.SetActive( true );
		startImg = goIntroScreen.GetComponent<Image>();
		startText.text = "";
		StartCoroutine(FadeImage(true));
	}

	private void DisplayGameOVerScreen(){
		goGameOverScreen.SetActive( true );
	}

	IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                startImg.color = new Color(1, 1, 1, i);
                yield return new WaitForEndOfFrame();
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                startImg.color = new Color(1, 1, 1, i);
                yield return new WaitForEndOfFrame();
            }
        }

		OnFadeImageComplete();    	
	}

	private void OnFadeImageComplete(){
		goIntroScreen.SetActive( false );
		StopCoroutine( FadeImage( false ) ); 
	}
}
