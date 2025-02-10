using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoss : MonoBehaviour
{
    [SerializeField] private float attackRateP1;
    [SerializeField] private float attackRateP2;
    [SerializeField] private float attackRateP3;
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
    private int phase;
    private BossHealth bh;
    private float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        fistRate = 3f;
        debrisRate = 5f;
        laserRate = 9f;
        lasertimer = laserRate;
        fisttimer = fistRate;
        debristimer = debrisRate;
        attackTimer = attackRateP1;
        phase = 1;
        // currentAttackRate = attackRateP1;
        // currentAttackNum = attackNumP1;
        bh = GetComponent<BossHealth>();
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bh.IsDead())
        {
            phase = bh.GetPhase();
            ProcessAttack();
        }
        // if(attackTimer <= 0){ProcessAttack();}
        // attackTimer -= Time.deltaTime;
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
                if(Random.value > 0.5f){Instantiate(laserEyes);}else{Instantiate(laserEyesAlt);}
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
