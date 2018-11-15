using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossView : MonoBehaviour {

    public static readonly string TAG = "BossView";

    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedInSeconds = 0.2f;
    private BossController controller;
    private int maxHp;


    void OnEnable() { // future brandon test if this works so you dont need to call onDestory
     //   if( controller != null ){
            //controller.OnHealthChange += UpdateHealthBar;
       // }
    }

    void OnDisable(){
        //if( controller != null ){
           // controller.OnHealthChange -= UpdateHealthBar;
       // }
    }

    void OnDestroy(){
        controller.OnHealthChange -= UpdateHealthBar;
    }

    public void Init( BossController controller, int maxHp ){
        this.controller = controller;
        this.maxHp = maxHp;
        controller.OnHealthChange += UpdateHealthBar;
    }

    public void SetActive( bool isActive ){
        gameObject.SetActive( isActive );
    }

    private void UpdateHealthBar( int currentHp ){
        Debug.Log( currentHp );
        Debug.Log( maxHp );
        float currentHpPct = (float) currentHp / (float) maxHp;
        StartCoroutine( ChangeHpPct( currentHpPct ) );
    }


    private IEnumerator ChangeHpPct( float pct ){
        Debug.Log( pct );
        float pre = foregroundImage.fillAmount;
        float elapsed = 0f;

        while( elapsed < updateSpeedInSeconds ){
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp( pre, pct, elapsed / updateSpeedInSeconds );
            yield return null;
        }

        foregroundImage.fillAmount = pct;
        OnChangeHpPctComplete( pct );
    }

    private void OnChangeHpPctComplete( float pct ){
        StopCoroutine( ChangeHpPct( pct) );
    }
}