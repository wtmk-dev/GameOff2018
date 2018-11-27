using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour, IProjectile {
    public bool Reflectable { get; set; }
    
    private AttackPattern.Projectile MoveData;

    public Transform getTransform(){
        return GetComponent<Transform>();
    }

    public void ReversePostions()
    {
        Vector3 reverse = MoveData.targetPosition;
        MoveData.targetPosition = MoveData.startPosition;
        MoveData.startPosition = reverse;
        MoveData.lifespan += 1;
    }

    public void SetBulletData( AttackPattern.Projectile bullet )
    {
        MoveData = bullet;
    }

    void Update()
    {
        
        this.transform.position = Vector3.MoveTowards(this.transform.position, MoveData.targetPosition, MoveData.velocity * Time.deltaTime);
        MoveData.spawnTime += Time.deltaTime;
        if (MoveData.spawnTime >= MoveData.lifespan)
        {
            MoveData.complete = true;
            Destroy(gameObject);
        }
           
    }


}
