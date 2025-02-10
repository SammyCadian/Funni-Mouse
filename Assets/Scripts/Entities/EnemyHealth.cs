using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float graceTime;
    private float health;
    public float GetMaxHP(){return maxHealth;}
    public float GetCurHP(){return health;}
    private Color standardColor;
    [SerializeField] private Color hitColor;
    [SerializeField] private HealthBarScript healthbar;
    private float colorChangeTimer;
    // Start is called before the first frame update
    void Start()
    {  
        colorChangeTimer = 0f;
        standardColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        graceTime = 0f;
        health = maxHealth;
        healthbar.Setup(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTakingDamage();
    }
    protected void ProcessTakingDamage()
    {
        if(graceTime >= 0){graceTime -= Time.deltaTime;}
        if(colorChangeTimer > 0){transform.GetChild(0).GetComponent<SpriteRenderer>().color = hitColor; colorChangeTimer -= Time.deltaTime;}else{transform.GetChild(0).GetComponent<SpriteRenderer>().color = standardColor;}
        if(IsDead()){Die();}
    }
    public bool IsDead(){return health <= 0;}
    public abstract void Die();
    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("hi");
        if(other.gameObject.layer == 10)
        {
            health -= other.gameObject.GetComponent<PlayerAttack>().GetDamage();
        }
    }
    public void TakeDamage(float dmg)
    {
        if(graceTime <= 0f)
        {
            colorChangeTimer = 0.2f;
            graceTime += 0.5f;
            Debug.Log(health);
            health -= dmg;
            healthbar.UpdateBar(health);
        }
    }
}
