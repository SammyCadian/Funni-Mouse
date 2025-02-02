using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private KeyCode attackKey;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    private float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldownTimer >= attackCooldown && Input.GetKeyDown(attackKey))
        {
            Instantiate(effect).GetComponent<PlayerAttackInstance>().AttachMovement(GetComponent<PlayerMovement>(), damage);
            cooldownTimer = 0f;
        }else
        {
            cooldownTimer += Time.deltaTime;
        }
    }
    public float GetDamage(){return damage;}
}
