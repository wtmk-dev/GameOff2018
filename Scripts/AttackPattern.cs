using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackPattern : MonoBehaviour
{
    public GameObject projectileTest;
    private List<Projectile> projectiles;
    public GameObject thePlayer;
    public float rotateSpeed = 5; // rotate speed
    public float heightVariance = 2; // height float amount
    public float heightRate = 1; // speed of height changes
    public GameObject[] locationList;
    public float[] locationPriority;

    public float waitTime = 1f;
    public float movingrate = 1; // for time math

    private float moveTimer;

    private float timeGoes = 0; // track time
    private Transform thisT; // what this is
    private bool upDown = true; // toggle for up vs down
    private float rate; // for time math
    
    private bool moveTimerElapsed = false;
    private bool movingPosition = false;
    private float index = 0; // track point a to b
    private float moveindex = 0;
    private Vector3 startPosition; // where did i start from
    private Vector3 nextPosition; // where am i going

    bool isActive = false;
    bool firstTime = true;

    // Use this for initialization
    void Start()
    {
        projectiles = new List<Projectile>();
        thisT = transform;
        if (firstTime)
        {
            startPosition = thisT.transform.localPosition;
        }
        rate = 1 / heightRate;
        isActive = true;
        ChooseNewPosition();
        movingPosition = false;
        moveTimerElapsed = false;
        print(startPosition);
        print(nextPosition);
    }

    void OnArrive()
    {
        print("arrived !");
        startPosition = thisT.transform.localPosition;
        ChooseNewPosition();
        AttackChoices();
        moveTimerElapsed = false;
        movingPosition = false;
    }

    void AttackChoices()
    {
        if ( EnemyWithinRange(5f) )
        {
            thePlayer.GetComponent<Renderer>().material.color = Color.red;
            ChooseNextAttack();
        }
    }

    bool EnemyWithinRange( float range)
    {
        print(Vector3.Distance(thePlayer.transform.position, thisT.transform.localPosition));
        if (Vector3.Distance(thePlayer.transform.position, thisT.transform.localPosition) <= range)
        {
            return true;
        }
    
        return false;
    }

    void ChooseNextAttack()
    {
        //for now just fire a projectile
        //(start + percent * (end - start))
        Vector3 overshoot = (startPosition + 3 * (thePlayer.transform.position - startPosition));
        Vector3 plus20 = new Vector3(0, 0, 20);
        Vector3 minus20 = new Vector3(0, 0, -20);
        var bullet = new Projectile();
        bullet.visual = Instantiate(projectileTest);
        bullet.visual.transform.position = startPosition = thisT.transform.position;
        
        bullet.targetPosition = overshoot;// thePlayer.transform.position;
        projectiles.Add(bullet);

        var bullet1 = new Projectile();
        bullet1.visual = Instantiate(projectileTest);
        bullet1.visual.transform.position = startPosition = thisT.transform.position;
        bullet1.targetPosition = RotatePointAroundPivot(overshoot, startPosition, minus20);
        projectiles.Add(bullet1);

        var bullet2 = new Projectile();
        bullet2.visual = Instantiate(projectileTest);
        bullet2.visual.transform.position = startPosition = thisT.transform.position;
        bullet2.targetPosition = RotatePointAroundPivot(overshoot, startPosition, plus20);
        projectiles.Add(bullet2);

    }

    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
       Vector3 dir = point - pivot; // get point direction relative to pivot
       dir = Quaternion.Euler(angles) * dir; // rotate it
       point = dir + pivot; // calculate rotated point
       return point; // return it
     }

void ChooseNewPosition()
    {
        float val = Random.value;
        print(val + "random chance");
        
        for (int i = 0; i < locationPriority.Length; i++){
            if (val <= (locationPriority[i] / 100) ) { 
                nextPosition = locationList[i].transform.position;
                print("nextPosition");
                print(nextPosition);
                break;    // and stop the loop
            }else{
                print("else");
                val -= (locationPriority[i] / 100);
            }
        }
    }

    void OnEnable()
    {
        Start();
    }
    void OnDisable()
    {
        isActive = false;
        firstTime = false;
    }
    // Update is called once per frames
    void Update()
    {
        foreach (Projectile bullet in projectiles)
        {
            bullet.visual.transform.position = Vector3.MoveTowards(bullet.visual.transform.position, bullet.targetPosition, bullet.velocity * Time.deltaTime);
            bullet.spawnTime += Time.deltaTime;
            if(bullet.spawnTime >= bullet.lifespan)
            {
                Destroy(bullet.visual);
                bullet.visual = null;
            }
        }

        projectiles.RemoveAll(x => x.visual == null);

        if (!moveTimerElapsed)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer > waitTime)
            {
                print("move Timer is done");
                moveTimer = 0f;
                moveTimerElapsed = true;
                moveindex = 0;
                movingPosition = true;
                thePlayer.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        if (movingPosition)
        {
            if (moveindex < 1.0){
                thisT.transform.localPosition = Vector3.Lerp(startPosition, nextPosition,  moveindex);
                moveindex += movingrate * Time.deltaTime;
            }else{
                print("move position is done");
                moveindex = 0;
                movingPosition = false;
                OnArrive();
            }
        }

        if (isActive && moveTimerElapsed && !movingPosition)
        {
            
            // get time to start over at the specified interval for up / down
            if (timeGoes >= heightRate)
            {
                timeGoes = 0f;
                upDown = !upDown;
            }
            else
            {
                timeGoes += Time.deltaTime;
            }

            // lerp between our two posistions
            if (index < 1.0)
            {
                if (upDown)
                {
                    thisT.transform.localPosition = Vector3.Lerp(startPosition, new Vector3(startPosition.x, startPosition.y + heightVariance, startPosition.z), index);
                    index += rate * Time.deltaTime;
                }
                else
                {
                    thisT.transform.localPosition = Vector3.Lerp(new Vector3(startPosition.x, startPosition.y + heightVariance, startPosition.z), startPosition, index);
                    index += rate * Time.deltaTime;
                }
            }
            else
            {
                index = 0;
            }

            thisT.Rotate((Vector3.up * rotateSpeed * 35) * Time.deltaTime);
        }

    }

    private class Projectile
    {
        //new int[] { 00,01,02,03,04},
        public GameObject visual;
        public Vector3 startPosition; // where did i start from
        public Vector3 targetPosition; // where am i going 
        public int lifespan;
        public float spawnTime;
        public float velocity;
        
        public Projectile() // IM THE DEFAULTS
        {
            this.visual = new GameObject();
            this.startPosition = new Vector3(0,0,0); // where did i start from
            this.targetPosition = new Vector3(8, 0, 0); // where am i going 
            this.lifespan = 3;
            this.spawnTime = 0;
            this.velocity = 2;
        }
    }
}
