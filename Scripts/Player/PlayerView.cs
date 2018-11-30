using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour {

	public delegate void DamageComplete();
	public event DamageComplete OnDamageComplete;

	public static readonly string TAG = "PlayerView";
	[SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedInSeconds = 0.2f;
	private Player player;
	private PlayerController playerController;
	private bool isActive = false;

	private void OnDestroy(){
		UnSubEvents();
	}

	public void Init( Player player, PlayerController pc ){
		playerController = pc;
		this.player = player;
		isActive = true;

		SubEvents();
	}

	private void SubEvents(){
		playerController.OnHealthChange += UpdateHealthBar;
	}

	private void UnSubEvents(){
		playerController.OnHealthChange -= UpdateHealthBar;
	}

	 private void UpdateHealthBar( int currentHp ){
        Debug.Log( currentHp );
	    Debug.Log( player.maxHp );
        Debug.Log( "IM HIT" );
		float currentHpPct = (float) currentHp / (float) player.maxHp;
		Debug.Log( currentHpPct );
        StartCoroutine( ChangeHpPct( currentHpPct ) );
    }


    private IEnumerator ChangeHpPct( float pct ){
        float pre = foregroundImage.fillAmount;
        float elapsed = 0f;

        while( elapsed < updateSpeedInSeconds ){
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp( pre, pct, elapsed / updateSpeedInSeconds );
            yield return null;
        }

        foregroundImage.fillAmount = pct;
		yield return new WaitForSecondsRealtime( 1f );
        OnChangeHpPctComplete( pct );
    }

    private void OnChangeHpPctComplete( float pct ){
        StopCoroutine( ChangeHpPct( pct) );
		if( OnDamageComplete != null ){
			OnDamageComplete();
		}
    }
}
