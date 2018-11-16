using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour {

    private Player player;

    private int speedExp;
    private int jumpExp;

    public void Init( Player player ){
        this.player = player;
        speedExp = 0; jumpExp = 0;
    }

    public float GetSpeed(){
        float speed = 3f;
        speed += (float) (player.Speed / 2f);
        if( speed > 8f ){
            speed = 8f;
        }
        return speed;
    }

    public float GetJump(){
        float jump = 4.5f;
        jump += (float) (player.Jump / 10f);
        if( jump > 10f ){
            jump = 10f;
        }
        return jump;
    }

    public float GetSword(){
        float sword = 5f;
        sword -= (float) (player.Sword / 2f);
        if( sword < 1.5f ){
            sword = 1.5f;
        }
        return sword;
    }

    public float GetWhip(){
        float whip = .2f;
        whip += (float) (player.Whip / 10f);
        if( whip > 1.5f ){
            whip = 1.5f;
        }
        return whip;
    }

    public void SpeedExp(){
        speedExp++;
        if( speedExp >= player.Speed * 2 ){
            player.Speed++;
            //play Speed lvl up
            Debug.Log( "Speed lvl up" );
        }
        
    }

    public void JumpExp(){
        jumpExp++;
        if( jumpExp >= player.Jump * 2 ){
            player.Jump++;
            //play jump lvl up
//            Debug.Log( "Jump lvl up" );
        }
        
    }
}