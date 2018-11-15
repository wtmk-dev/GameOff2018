using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModel {

	private int hp;
	private int maxHp;
	public int MaxHp{get{return maxHp;}}
	public int HP{get{ return hp; } set{ hp = value; }}

	public BossModel( int hp ){
		this.hp = hp; maxHp = hp;
	}





}