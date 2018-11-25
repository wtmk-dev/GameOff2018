using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackPattern : MonoBehaviour
{
    [SerializeField]
    private List<TinyDict> Animations;
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
    private Animator bossAnimator;
    private int currentAnimation = 0;
    private bool isIdlingAnimation = false;

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
        //thisT = transform;
        bossAnimator = this.GetComponent<Animator>();
        if (firstTime)
        {
            startPosition = this.transform.localPosition;
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
        returnToIdle();
        startPosition = this.transform.localPosition;
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

    IEnumerator playerTest()
    {
        
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = transform;
        sphere.GetComponent<SphereCollider>().enabled = false;
        sphere.transform.localScale = new Vector3(1f, 1f, 1f);
        sphere.transform.position = thePlayer.transform.position;
        sphere.GetComponent<Renderer>().material.color = Color.red;
       
        
        GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = transform;
        sphere1.GetComponent<SphereCollider>().enabled = false;
        sphere1.transform.localScale = new Vector3(1f, 1f, 1f);
        sphere1.transform.position = this.transform.position;
        sphere1.GetComponent<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(2);

        Destroy(sphere);
        Destroy(sphere1);

    }

    bool EnemyWithinRange( float range)
    {
        StartCoroutine(playerTest());
        print(Vector3.Distance(thePlayer.transform.localPosition, this.transform.position));
        if (Vector3.Distance(thePlayer.transform.position, this.transform.position) <= range)
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
        Vector3 overshoot = (this.transform.position + (3 * (thePlayer.transform.position - this.transform.position)));

        bool canAttackSpot = false;
        if (item.anyLocation)
        {
            canAttackSpot = true;
        }
        else
        {
            foreach (GameObject spot in item.validLocations)
            {
                if (spot.transform.position == this.transform.position)
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
                    newBullet.visual.transform.position = this.transform.position;
                    newBullet.targetPosition = RotatePointAroundPivot(overshoot, this.transform.localPosition, angle); // thePlayer.transform.position is on target
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

                        newBullet.visual.transform.position = this.transform.position + offset;
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
            print(boss);
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
                SetPlayerAnimation(AnimationTranslator("walk"));
                if (nextPosition.x < this.transform.localPosition.x)
                {
                    this.transform.LookAt(new Vector3(this.transform.position.x - 20, this.transform.position.y, this.transform.position.z));
                }
                else
                {
                    this.transform.LookAt(new Vector3(this.transform.position.x + 20, this.transform.position.y, this.transform.position.z));
                }
                if (moveindex < 1.0){
                    this.transform.localPosition = Vector3.Lerp(startPosition, nextPosition,  moveindex);
                    moveindex += movingrate * Time.deltaTime;
                }else{
                    print("move position is done");
                    moveindex = 0;
                    movingPosition = false;
                    OnArrive();
                }
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

        if (this.transform.localPosition.x > thePlayer.transform.position.x)
        {
            playerDirection = facingDirections.LEFT;
        }
        else
        {
            playerDirection = facingDirections.RIGHT;
        }

        // add check for overriding left / right
        if(getDistance(this.transform.localPosition.y , thePlayer.transform.position.y) > getDistance(this.transform.localPosition.x, thePlayer.transform.position.x))
        {
            if(this.transform.localPosition.y > thePlayer.transform.position.y)
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

    void returnToIdle()
    {
        if (!isIdlingAnimation)
        {
            isIdlingAnimation = true;
            SetPlayerAnimation(AnimationTranslator("idle"));
        }
    }

    private void SetPlayerAnimation(int i)
    {
        if (i > 1)
        {
            isIdlingAnimation = false;
        }

        if (i != currentAnimation)
        {
            bossAnimator.SetInteger("animation", i);
        }
    }

    private int AnimationTranslator( string name)
    {
        foreach (var item in Animations)
        {
            if(item.name == name)
            {
                return item.index;
            }
        }

        return 1;
    }
    //idle : 1
    //damage : 2
    //death : 3
    //walk : 4
    //run : 5
    // get attack from data

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

    [System.Serializable]
    public class TinyDict
    {
        public string name;
        public int index;
    }


}