using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    [SerializeField] private OuchieMenu winUI;
    private float dyingTimer;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(IsDead() && dyingTimer < 1f){dyingTimer = 7f;}
        if(dyingTimer > 0){ProcessDying();}
        ProcessTakingDamage();
    }

    public override void Die()
    {

    }
    private void ProcessDying()
    {
        Debug.Log(dyingTimer);
        if(dyingTimer > 2f)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            dyingTimer -= Time.deltaTime;
        }else
        {
            winUI.Pause();
        }
    }
    public int GetPhase()
    {
        float precentHP = GetCurHP()/GetMaxHP();
        if(precentHP > 0.66f){return 1;}
        if(precentHP > 0.33f){return 2;}
        return 3;
    }
}
