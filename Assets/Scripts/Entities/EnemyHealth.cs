using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float graceTime;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        graceTime = 0f;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(graceTime >= 0){graceTime -= Time.deltaTime;}
        if(IsDead()){Die();}
    }
    public bool IsDead(){return health <= 0;}
    private void Die()
    {
        Destroy(gameObject);
    }
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
            graceTime += 0.5f;
            Debug.Log(health);
            health -= dmg;
        }
    }
}
