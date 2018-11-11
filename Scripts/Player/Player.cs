using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

	private static readonly System.Random random = new System.Random(); 
	private static readonly object syncLock = new object();

	private enum Job { WARRIOR, THIEF, MAGE } // w +move&Sword : t +jump&Whip : m +shield&Shoot
	private Job job;

	public int Jump{get;set;}
	public int Block{get;set;}
	public int Whip{get;set;}
	public int Speed{get;set;}
	public int Sword{get;set;}
	public int Shoot{get;set;}
	public int Blood{get;set;}

	public Player(){
		int roll = DM.RandomNumber( 1, 4 );
		job = GetJob( roll );
		SetStartingJob( job );
		Debug.Log( "Green " + job + " needs food badly." );
	}

	private Job GetJob( int roll ){
		Job job = Job.WARRIOR;

		if( roll == 1 ){
			job = Job.WARRIOR;
		}
		else if( roll == 2 ){
			job = Job.THIEF;
		} 
		else if( roll == 3 ){
			job = Job.MAGE;
		}

		return job;
	}


	private void SetStartingJob( Job job ){
		if( job == Job.WARRIOR ){
			Jump = 1;
			Whip = 1;
			Speed = 1;
			Sword = 2;
			Shoot = 1;
			Block = 2;
		}
		else if ( job == Job.THIEF ){
			Jump = 2;
			Whip = 2;
			Speed = 1;
			Sword = 1;
			Shoot = 1;
			Block = 1;
		}
		else if( job == Job.MAGE ){
			Jump = 1;
			Whip = 1;
			Speed = 2;
			Sword = 1;
			Shoot = 2;
			Block = 1;
		}

		Blood = 3;	
	}

	
}


