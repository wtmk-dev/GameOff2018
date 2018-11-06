using UnityEngine;
using System.Collections;

public class Map_Gizmo : MonoBehaviour {
	
	public enum _displayType{Air, Ground}
	public _displayType displayType = _displayType.Air;
	
	public enum _displayMode{Red,Green,Blue,Orange,Yellow,Purple,Grey,Black,Cyan}
	public _displayMode displayMode = _displayMode.Green;
		
	public float size = .3f;
	private Color tempColor;
	// Use this for initialization
	void Start () {
		
		
	}
	
	void OnDrawGizmos() {
		switch(displayMode){
		case _displayMode.Green:{
			Gizmos.color = Color.green;			
			break;}
		case _displayMode.Red:{
			Gizmos.color = Color.red;			
			break;}
		case _displayMode.Cyan:{
			Gizmos.color = Color.cyan;			
			break;}
		case _displayMode.Blue:{
			Gizmos.color = Color.blue;			
			break;}
		case _displayMode.Orange:{			
			Gizmos.color = new Color(1f,.5f,0f,1f);			
			break;}
		case _displayMode.Yellow:{
			Gizmos.color = new Color(1f,1f,0f,1f);			
			break;}
		case _displayMode.Purple:{
			Gizmos.color = new Color(1f,0f,1f,1f);			
			break;}
		case _displayMode.Black:{
			Gizmos.color = Color.black;			
			break;}
		case _displayMode.Grey:{
			Gizmos.color = Color.grey;			
			break;}
		default:
			break;
		}
		
		if (displayType == _displayType.Ground) {
			Gizmos.DrawCube(transform.position, new Vector3(size*1.3f,size*1.3f,size*1.3f));
		}else
		{
        	Gizmos.DrawSphere(transform.position, size);
		}
    }
	// Update is called once per frame
	void Update () {
	
	}
}
