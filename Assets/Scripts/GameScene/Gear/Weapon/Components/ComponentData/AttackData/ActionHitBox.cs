using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Progress;

public class ActionHitBox : WeaponComponent<ActionHitBoxData,AttackData>
{
    private event Action<Collider2D[]> OnDetectedCollider2D;

    [SerializeField]
    private Player player;

    private Vector2 offset;

    private Collider2D[] detected;
    private void HandleAttackAction()
    { 
        offset.Set(
        transform.position.x + (data.AttackHitBoxPlayer().center.x),
        transform.position.y + (data.AttackHitBoxPlayer().center.y)
        );
        detected = Physics2D.OverlapBoxAll(offset,data.AttackHitBoxPlayer().size,0f,data.DetectableLayers);

        if (detected.Length == 0) return;


        Debug.Log("Attack actions");
        OnDetectedCollider2D?.Invoke(detected);

        foreach (var collider in detected)
        {
            if(collider.TryGetComponent(out Enemy enemy))
            {
                if (enemy.TakeDamage(player,weapon.WeaponData))
                {
                    player.Exp += enemy.ExpValue;
                    player.Coins += enemy.CoinsValue;
                    Debug.Log(player.Exp);
                }

                Debug.Log(enemy.Hp);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
    }


    protected override void HandleEnter()
    {
        base.HandleEnter();

    }


    protected override void OnDisable()
    {
        base.OnDisable();

        animationEventHendler.OnAttackAction -= HandleAttackAction;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        animationEventHendler.OnAttackAction += HandleAttackAction;

    }

    private void OnDrawGizmosSelected()
    {
        if (data != null) return;
        Gizmos.DrawWireCube(transform.position + (Vector3)data.AttackHitBoxPlayer().center, data.AttackHitBoxPlayer().size);
    }
}

