using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageDealer, IDamageable
{
    private Animator animator;
    private PlayerStateMachine stateMachine;
    private Rigidbody2D rb;
    
    public WeaponScript weaponSC { get; private set; }
    public WeaponGenerator weaponGenerator { get; private set; }

    public Stats Stats {  get; private set; }   

    
    public PlayerInputHandler inputHandler;
    public PlayerMovementState movement;
    public PlayerCombatState combat;

    private int coins = 2000;
    private int exp;

    public bool isDead { get; private set; }

    public int Exp
    {
        get
        {
            return exp;
        }
        set
        {
            if (exp + value > upgradeSystem.ExpRequire)
            {
                upgradeSystem.LvL = upgradeSystem.LvL + 1;
                exp = 0;
                Debug.Log("level: " + upgradeSystem.LvL);
            }
            else
                exp += value;
        }
    }

    [field: SerializeField]public UpgradeSystem upgradeSystem { get; private set; }

    public int Coins 
    {
        get { return coins; }
        set { if(value!=0) coins = value; }
    }
    public float Hp { get { return Stats.CurrentHp; } }
    public float Speed { get { return Stats.Speed; } }

    [SerializeField]
    private int startingHp = 100, startingStrenght = 10, startingAgility = 10, startingCritChance = 10;

    public float CurrentHp
    {
        get
        {
            return Stats.CurrentHp;
        }
    }

    public Rigidbody2D RbPlayer
    {
        get { return rb; }
    }

    public Rigidbody2D Rb { get => rb; }


    // Start is called before the first frame update
    void Awake()
    {
        isDead = false;
        weaponSC = transform.Find("PrimaryWeapon").GetComponent<WeaponScript>();
        weaponGenerator = transform.Find("PrimaryWeapon").GetComponent<WeaponGenerator>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();

        Stats = new Stats(startingHp, startingStrenght, startingAgility, startingCritChance);

        stateMachine = new PlayerStateMachine();
        
        //states
        movement = new PlayerMovementState(this,stateMachine,animator);
        combat = new PlayerCombatState(this, stateMachine, weaponSC);

        stateMachine.Initialize(movement);
    }

    private void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.CurrentState.UpdateFunction();
    }

    private void FixedUpdate()
    {
       stateMachine.CurrentState.FixedUpdateFunction();
    }

    public float Damage()
    {
        int randomCrit = Random.Range(0, 100);
        if(Stats.CritChance > randomCrit) 
        {
           return Stats.AttackValue;
        }
        else
        {
            return Stats.CritDamge;
        }
    }
    private void SwitchWeapon()
    {
        /*WeaponSO helper = currentWeapon;
        currentWeapon = secondaryWeapon;
        secondaryWeapon = helper;*/
    }

    public bool TakeDamage(IDamageDealer dealer, WeaponSO optionalWeapon = null)
    {
        Stats.ChangeHp(-dealer.Damage());
        Debug.Log("current hp: " + Stats.CurrentHp);
        if (Stats.CurrentHp <= 0)
        {
            Die();
            return true;
        }
        else return false;
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("player is Dead");
        Destroy(gameObject, 2);
        Time.timeScale = 0;      
    }

}
