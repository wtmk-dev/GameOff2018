using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {


	private AudioSource audioSource;
	[SerializeField]
	private List<AudioClip> audioClips;

	void OnEnable(){
		DM.OnGameStarted += GameStart;
		StartBossTrigger.OnBossStart += MainGameLoop;
		PlayerController.OnPlayerKilled += GameOver;
		BossController.OnBossKilled += WinMusic;
	}

	void OnDisable(){
		DM.OnGameStarted -= GameStart;
		StartBossTrigger.OnBossStart -= MainGameLoop;
		PlayerController.OnPlayerKilled -= GameOver;
		BossController.OnBossKilled -= WinMusic;
	}

	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

	void Start(){
		PlayIntro();
	}

	private void GameStart(){
		SetAudioClip( audioClips[ 1 ] );
	}

	private void MainGameLoop( GameObject player ){
		audioSource.volume = .7f;
		SetAudioClip( audioClips[ 2 ] );
	}

	private void PlayIntro(){
		if( audioSource != null ){
			SetAudioClip( audioClips[ 0 ] );
		}
	}

	private void GameOver(){
		audioSource.volume = .5f;
		SetAudioClip( audioClips[ 3 ] );
	}

	private void WinMusic(){
		audioSource.Stop();
		//SetAudioClip( audioClips[ 4 ] );
	}

	private void SetAudioClip( AudioClip ac ){
		StopAudio();
		audioSource.clip = ac;
		PlayAudio();
	}

	private void StopAudio(){
		audioSource.Stop();
	}

	private void PlayAudio(){
		audioSource.Play();
	}

	

}
