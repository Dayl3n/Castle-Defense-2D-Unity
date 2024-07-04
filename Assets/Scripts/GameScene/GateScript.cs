using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100;


    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D colider;

    public float Hp => health;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        colider = GetComponent<BoxCollider2D>();
    }
    public bool TakeDamage(IDamageDealer dealer, WeaponSO optionalWeapon = null)
    {
        health -= dealer.Damage();
        Debug.Log("Gate: current health " + health);
        if (health <= 0)
        {
            Die();
            return true;
        }
        else return false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        
    }

    private void Die()
    {
        sprite.enabled = false;
        colider.enabled = false;
        Destroy(rb);
        Destroy(gameObject);
    }
}
