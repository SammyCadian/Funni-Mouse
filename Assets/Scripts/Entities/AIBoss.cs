using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoss : MonoBehaviour
{
    [SerializeField] private Transform playerRef;
    [SerializeField] private GameObject laserEyes;
    [SerializeField] private GameObject laserEyesAlt;
    [SerializeField] private GameObject debrisSpawner;
    [SerializeField] private GameObject fist;
    private float fistRate;
    private float fisttimer;
    private float laserRate;
    private float lasertimer;
    private float debrisRate;
    private float debristimer;
    private bool moving;
    private float moveTimer, activeMovetimer;
    private Vector3 left, center, right, targetLocation;
    private float travelDistance;
    private int phase;
    private BossHealth bh;
    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        left = transform.position + new Vector3(-6, 0, 0);
        right = transform.position + new Vector3(6, 0, 0);
        fistRate = 3f;
        debrisRate = 5f;
        laserRate = 9f;
        lasertimer = laserRate;
        fisttimer = fistRate;
        debristimer = debrisRate;
        phase = 1;
        // currentAttackRate = attackRateP1;
        // currentAttackNum = attackNumP1;
        bh = GetComponent<BossHealth>();
        GetComponent<Rigidbody2D>().isKinematic = true;
        moveTimer = 0f;
        activeMovetimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bh.IsDead())
        {
            phase = bh.GetPhase();
            ProcessAttack();
            if(!moving && moveTimer <= 0)
            {
                float x = Random.value;
                if(x > 0.66f){targetLocation = left;}else if(x > 0.33f){targetLocation = center;}else{targetLocation = right;}
                travelDistance = targetLocation.x - transform.position.x;
                activeMovetimer = 1f;
                moving = true;
            }else if(moving)
            {
                ProcessMove();
            }
            moveTimer -= Time.deltaTime;
        }
        // if(attackTimer <= 0){ProcessAttack();}
        // attackTimer -= Time.deltaTime;
    }
    private void ProcessMove()
    {
        if(activeMovetimer >= 0)
        {
            transform.position += new Vector3(Time.deltaTime * travelDistance, 0, 0);
        }else
        {
            moveTimer = 10f + Random.value * 5f;
            moving = false;
        }
        activeMovetimer -= Time.deltaTime;
    }
    private void ProcessAttack()
    {
        if(phase > 0 && fisttimer <= 0)
        {
            Attack(1);
            fisttimer = fistRate + Random.value * 2;
        }
        if(phase > 1)
        {
            if(lasertimer <= 0)
            {
                Attack(0);
                lasertimer = laserRate + Random.value * 2;
            }
            lasertimer -= Time.deltaTime;
        }
        if(phase > 2)
        {
            if(debristimer <= 0)
            {
                Attack(2);
                debristimer = laserRate + Random.value * 2;
            }
            debristimer -= Time.deltaTime;
        }
        fisttimer -= Time.deltaTime;
    }

    private void Attack(int attac)
    {
        // Debug.Log(attac);
        switch(attac)
        {
            case 0:  
                if(Random.value > 0.5f){Instantiate(laserEyes, transform.position + Vector3.down*0.5f, transform.rotation);}else{Instantiate(laserEyesAlt, transform.position + Vector3.down*0.5f, transform.rotation);}
                break;
            case 1:
                int sideB;
                if(Random.value > 0.5f){sideB = 0;}else{sideB = 1;}
                Instantiate(fist, Camera.main.transform.position + new Vector3(8 - (16 * sideB), -8, 1), transform.rotation).GetComponent<EnemyFist>().SetTarget(playerRef);
                break;
            case 2:
                int side = Random.Range(0, 3);
                Instantiate(debrisSpawner, Camera.main.transform.position + new Vector3(-8 + 8*side, 8, 1), transform.rotation);
                break;
        }
    }
}
