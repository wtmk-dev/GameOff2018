using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackPattern : MonoBehaviour
{
    [SerializeField]
    private List<Attack> Attacks;
    private bool attackComplete = true;

    private List<Projectile> projectiles;
    private List<Melee> melees;

    private GameObject thePlayer;
    public float rotateSpeed = 5; // rotate speed
    public float heightVariance = 2; // height float amount
    public float heightRate = 1; // speed of height changes
    [SerializeField]
    public List<Location> locationList;

    public float waitTime = 3f;
    public float movingrate = 1; // for time math

    private float moveTimer;

    private float timeGoes = 0; // track time
    private Transform thisT; // what this is - why did i do this ?
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

    private bool isInit;

    enum facingDirections
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    };

    // Use this for initialization
    void Start()
    {
        print("DERP");
        //Attacks = new List<Attack>();
        projectiles = new List<Projectile>();
        melees = new List<Melee>();
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
        isInit = false;
        print(startPosition);
        print(nextPosition);
    }

    void OnArrive()
    {
        print("arrived !");
        print(GetPlayerDirection());
        startPosition = thisT.transform.localPosition;
        ChooseNewPosition();
        AttackChoices();
        moveTimerElapsed = false;
        movingPosition = false;
    }

    void AttackChoices()
    {
        attackComplete = false;
        if ( true )
        {
            thePlayer.GetComponent<Renderer>().material.color = Color.red;
            ChooseNextAttack();
        }else
        {
            attackComplete = true;
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
        print(Attacks.Count);
        foreach (Attack item in Attacks)
        {
            StartCoroutine(spawnAttack(item));
        }
        attackComplete = true;
    }
	
    //this needs to notify about sending a spin
	IEnumerator spawnAttack(Attack item)
    {
        Vector3 overshoot = (startPosition + 3 * (thePlayer.transform.position - startPosition));

        bool canAttackSpot = false;
        if (item.anyLocation)
        {
            canAttackSpot = true;
        }
        else
        {
            foreach (GameObject spot in item.validLocations)
            {
                if (spot.transform.position == thisT.transform.localPosition)
                {
                    canAttackSpot = true;
                }
            }
        }

        if (canAttackSpot)
        {
            if (item.ranged && EnemyWithinRange(item.projectile.range) || item.ranged && !item.projectile.rangeCheck)
            {
                print("DOING RANGED");
                foreach (Vector3 angle in item.projectileCount)
                {

                    string bullet = JsonUtility.ToJson(item.projectile);
                    Projectile newBullet = JsonUtility.FromJson<Projectile>(bullet);
                    newBullet.visual = Instantiate(item.projectile.visual);
                    newBullet.visual.transform.position = startPosition = thisT.transform.localPosition;
                    newBullet.targetPosition = RotatePointAroundPivot(overshoot, startPosition, angle);
                    projectiles.Add(newBullet);
                }
            }
            else
            {
                print("DOING MELEE");
                if (!item.ranged && EnemyWithinRange(item.melee.range) || !item.ranged && !item.melee.rangeCheck)
                {
                    if(item.melee.rangeCheck && EnemyWithinRange(item.melee.range))
                    {
                        string bullet = JsonUtility.ToJson(item.melee);
                        Melee newBullet = JsonUtility.FromJson<Melee>(bullet);
                        newBullet.visual = Instantiate(item.melee.visual);
                        facingDirections testFor = GetPlayerDirection();
                        Vector3 offset = new Vector3(0, 0, 0);
                        switch (testFor)
                        {
                            case facingDirections.LEFT:
                                offset.x -= 1;
                                break;
                            case facingDirections.RIGHT:
                                offset.x += 1;
                                break;
                            case facingDirections.UP:
                                offset.y += 1;
                                break;
                            case facingDirections.DOWN:
                                offset.y -= 1;
                                break;
                            default:
                                break;
                        }

                        newBullet.visual.transform.position = startPosition = thisT.transform.localPosition + offset;
                        melees.Add(newBullet);
                    }
                }
            }
        }

        yield return new WaitForSeconds(item.frequency);
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
        
        for (int i = 0; i < locationList.Count; i++){
            if (val <= (locationList[i].priority / 100) ) { 
                nextPosition = locationList[i].location.transform.localPosition;
                break;    // and stop the loop
            }else{
                val -= (locationList[i].priority / 100);
            }
        }
    }

    void OnEnable()
    {
       StartBossTrigger.OnBossStart += Init;
    }

    void OnDisable()
    {
        StartBossTrigger.OnBossStart -= Init;
        isActive = false;
        firstTime = false;
        isInit = false;
    }

    private void Init( GameObject player ){
        if( !isInit ){
            thePlayer = player;
            BossController controller = this.GetComponentInParent<BossController>();
            BossModel boss = new BossModel( 100 );
            controller.Init( boss );
            isInit = true;
        }
        
    }

    void Update()
    {
        if( isInit  ){
            projectileUpdate();
            meleeUpdate();

            if (!moveTimerElapsed)
            {
                moveTimer += Time.deltaTime;
                if (moveTimer > waitTime && attackComplete)
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
       

        

    }

    void projectileUpdate()
    {
        if(projectiles.Count > 0)
        {
            //print(projectiles.Count);
            foreach (Projectile bullet in projectiles)
            {
                bullet.visual.transform.position = Vector3.MoveTowards(bullet.visual.transform.position, bullet.targetPosition, bullet.velocity * Time.deltaTime);
                bullet.spawnTime += Time.deltaTime;
                if (bullet.spawnTime >= bullet.lifespan)
                {
                    Destroy(bullet.visual);
                    bullet.complete = true;
                }
            }

            projectiles.RemoveAll(x => x.complete == true);
        }
    }

    void meleeUpdate()
    {
        if(melees.Count > 0)
        {
            foreach (Melee bullet in melees)
            {
                //bullet.visual.transform.position = Vector3.MoveTowards(bullet.visual.transform.position, bullet.targetPosition, bullet.velocity * Time.deltaTime);
                bullet.spawnTime += Time.deltaTime;
                if (bullet.spawnTime >= bullet.lifespan)
                {
                    Destroy(bullet.visual);
                    bullet.complete = true;
                }
            }

            melees.RemoveAll(x => x.complete == true);
        }
    }


    facingDirections GetPlayerDirection()
    {
        facingDirections playerDirection;

        if (thisT.transform.localPosition.x > thePlayer.transform.position.x)
        {
            playerDirection = facingDirections.LEFT;
        }
        else
        {
            playerDirection = facingDirections.RIGHT;
        }

        // add check for overriding left / right
        if(getDistance(thisT.transform.localPosition.y , thePlayer.transform.position.y) > getDistance(thisT.transform.localPosition.x, thePlayer.transform.position.x))
        {
            if(thisT.transform.localPosition.y > thePlayer.transform.position.y)
            {
                playerDirection = facingDirections.DOWN;
            }
            else
            {
                playerDirection = facingDirections.UP;
            }
            
        }

        return playerDirection;
    }

    float getDistance(float x1, float x2)
    {
        float distance = Vector3.Distance(new Vector3( x1,0,0), new Vector3(x2, 0, 0));
        return distance;
    }

    [System.Serializable]
    public class Location
    {
        //new int[] { 00,01,02,03,04},
        public GameObject location;
        public float priority;
        public bool onWall;
        public Vector3 facingDirection;
        public bool leadsTo;
        public GameObject nextLocation;
        
    }

    [System.Serializable]
    public class Projectile
    {
        //new int[] { 00,01,02,03,04},
        public GameObject visual;
        public Vector3 startPosition; // where did i start from
        public Vector3 targetPosition; // where am i going 
        public int lifespan;
        public float spawnTime;
        public float velocity;
        public bool rangeCheck;
        public float range;
        public bool complete = false;

        
    }

    [System.Serializable]
    public class Melee
    {
        public GameObject visual;
        public Vector3 startPosition; // where did i start from
        public int lifespan;
        public float spawnTime;
        public Vector3 spawnOffset;
        public bool rangeCheck;
        public float range;
        public bool complete = false;

    }

    [System.Serializable]
    public class Attack
    {
        public bool ranged;
        public bool towardsPlayer;
        public Vector3 awayFromWallDirection;
        public int count;
        public float frequency;
        public List<Vector3> projectileCount;
        public Projectile projectile;
        public Melee melee;
        public int damage;
        public bool anyLocation;
        public List<GameObject> validLocations;
        public bool complete = false;
    }

    
}