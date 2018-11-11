using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    private GameObject blackTile;
    private GameObject pinkTile;

    void Awake(){
        blackTile = Resources.Load( "BlackTile" ) as GameObject;
        pinkTile = Resources.Load( "PinkTile" ) as GameObject;
    }

    public void BuildStartingZone(){
        var count = 0;
        var minX = -40f;
        var maxX = 80f;
        
        // do{
        //     Instantiate( blackTile, new Vector3( minX + count, -2f, 0f ), Quaternion.identity );
        //    // Instantiate( pinkTile, new Vector3( minX + count + 1f, -2f, 0f ), Quaternion.identity );
        //     count++;
        // }while( count < maxX );

    }
}