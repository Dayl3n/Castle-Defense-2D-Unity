using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IDamageDealer
{ 
    [SerializeField]
    private int expValue=20, coinsValue = 20;

    public bool isDead {  get; private set; }   

    [field: SerializeField] public ActionHitBoxData hitBoxData { get; private set; }

    private EnemyMovement movement;

    private bool isAttacking;


    [SerializeField]
    private Transform gateTarget,playerTarget;
    private Rigidbody2D rb;
    private Animator anim;
    public BoxCollider2D collider { get; private set; }

    [field: SerializeField] public float AttackRadius {  get;private set; }


    [SerializeField]
    private int startingHp = 100, startingStrenght = 10, startingAgility = 10, startingCritChance = 10;

    public Stats stats { get; private set; }
    public float Speed { get { return stats.Speed; } }
    public float Hp { get { return stats.CurrentHp; } }

    public Rigidbody2D Rb { get => rb; }
    public Animator Anim { get => anim; }
    public Transform GateTarget { get => gateTarget; }
    public Transform PlayerTarget { get => playerTarget; }
    public LayerMask TargetsLayers { get => hitBoxData.DetectableLayers; }
    public int ExpValue { get => expValue; }
    public int CoinsValue { get => coinsValue; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(GameObject.FindGameObjectsWithTag("Gate").Length == 0)
        {
            gateTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
        gateTarget = GameObject.FindGameObjectWithTag("Gate").transform;

        }
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        collider = GetComponent<BoxCollider2D>();

        

        stats = new Stats(startingHp, startingStrenght, startingAgility, startingCritChance);
        movement = new EnemyMovement(this, gateTarget, playerTarget);
    }

    public virtual float Damage()
    {
        int randomCrit = Random.Range(0, 100);
        if (stats.CritChance > randomCrit)
        {
            return stats.AttackValue;
        }
        else
        {
            return stats.CritDamge;
        }
    }

    public virtual bool TakeDamage(IDamageDealer dealer, WeaponSO optionalWeapon = null)
    {
        float damage = dealer.Damage() + optionalWeapon.AttackValue;

        stats.ChangeHp(-damage);
        if(stats.CurrentHp <=0)
        {
            isDead = true;
            return true;
        }

        return false;

    }



    void Update()
    {
        if (gateTarget == null)
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            movement = new EnemyMovement(this, playerTarget, playerTarget);
        }

        if (!isDead)
        {
            Anim.SetBool("isAttacking", movement.isInAttackRange);

            if (movement.CheckAttackRange())
            {
                rb.velocity = Vector2.zero;
                if (!isAttacking)
                {
                    StartCoroutine(AttackCoroutine());
                }
            }
            else
            {
                movement.UpdateFunction();
            }
        }
        else
        {
            Die();
        }


    }

    private void FixedUpdate()
    {
        movement.FixedUpdateFunction();
    }

    private void Die()
    {
        rb.MovePosition(transform.position);
        anim.SetBool("isDead", true);
        collider.enabled = false;
        //rb.enabled = false;
        Destroy(gameObject,2);
    }


    private void OnDrawGizmosSelected()
    {
        if (movement == null) return;
        movement.OnDrawGizmosSelected();
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        foreach (var data in movement.detected)
        {
            if (data.TryGetComponent(out Player player))
            {
                player.TakeDamage(this);
            }
            if (data.TryGetComponent(out GateScript gate))
            {
                if (gate.TakeDamage(this))
                {
                    gateTarget = playerTarget;
                }

            }
        }

        // OpóŸnienie miêdzy atakami (zmieñ wartoœæ wed³ug w³asnych potrzeb)
        float attackCooldown = 1.0f;
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }




}
