using UnityEngine;
using System.Collections;

public class FloatAndRotate : MonoBehaviour {
    
	public float moveVariance = 2; // height float amount
	public float moveRate = 1; // speed of height changes
		
	private float timeGoes = 0; // track time
	private Transform thisT; // what this is
	private bool upDown = true; // toggle for up vs down
	private float rate; // for time math

    public bool LeftRight = false;
	
	private float index = 0; // track point a to b
	
	private Vector3 startPosition; // where did i start from
	
	bool isActive = false;
	bool firstTime = true;

	// Use this for initialization
	void Start () {
		thisT=transform;
		if (firstTime){
			startPosition = thisT.transform.localPosition;}
		rate = 1/moveRate;
		isActive = true;
	}

	void OnEnable(){
		Start ();
	}
	void OnDisable(){
		isActive = false;
		firstTime = false;
	}
	// Update is called once per frames
	void Update () {
		
		if (isActive) {
			// get time to start over at the specified interval for up / down
			if(timeGoes >= moveRate){
				timeGoes = 0f;
				upDown = !upDown;			
			}else
			{
				timeGoes+=Time.deltaTime;			
			}

            Vector3 moveVector;
            if (LeftRight)
            {
                moveVector = new Vector3(startPosition.x + moveVariance, startPosition.y, startPosition.z);
            }
            else
            {
                moveVector = new Vector3(startPosition.x, startPosition.y + moveVariance, startPosition.z);
            }
			
			// lerp between our two posistions
			if( index < 1.0 )
			   {
                    if(upDown){
                        thisT.transform.localPosition = Vector3.Lerp( startPosition, moveVector, index );				
                        index += rate * Time.deltaTime;	        
                    }else{
	                    thisT.transform.localPosition = Vector3.Lerp(moveVector, startPosition, index );
	                    index += rate * Time.deltaTime;	        
                    }			
			   }else{
				index = 0;			
			}	
		}
	}
}
