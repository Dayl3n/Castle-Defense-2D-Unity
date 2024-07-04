using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyMovement
{
    private Enemy enemy;
    private Transform firstTarget,secondTarget;

    public bool shouldRotate {  get; private set; }
    public bool isInAttackRange {  get; private set; }

    public Vector2 movement { get; private set; }
    private Vector2 dir;
    private Vector2 offset;
    public Collider2D[] detected { get; private set; }


 

    
    public EnemyMovement(Enemy enemy, Transform firstTarget, Transform secondTarget = null)
    {
        this.enemy = enemy;
        this.firstTarget = firstTarget;
        this.secondTarget = secondTarget;
        shouldRotate = true;
    }

    public void UpdateFunction()
    {
        if(firstTarget.IsDestroyed()) firstTarget = secondTarget;
        enemy.Anim.SetFloat("Speed", enemy.Speed);


        if (firstTarget == null) return;
        dir = firstTarget.position - enemy.transform.position;
        float anlge = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if (shouldRotate)
        {
            enemy.Anim.SetFloat("Horizontal", dir.x);
            enemy.Anim.SetFloat("Vertical", dir.y);
        }

        if(Vector2.Distance(enemy.transform.position, firstTarget.transform.position)<0.5f && firstTarget.IsDestroyed())
        {
            firstTarget = secondTarget;
        }
        
    
    }
    public void FixedUpdateFunction()
    {
        if (!ShopEnterLeaveScript.isMenuOpen)
        {
            if (!isInAttackRange||!enemy.isDead)
            {
                MoveCharacter(movement);
            }
            else
            {            
                enemy.Anim.SetFloat("Speed", 0);
                enemy.Rb.MovePosition(enemy.transform.position);
                enemy.Rb.velocity = Vector2.zero;
                enemy.collider.enabled = false;
            }
        }
    }

    private void MoveCharacter(Vector2 movement) 
    {
        enemy.Rb.MovePosition((Vector2)enemy.transform.position + (dir * enemy.Speed* Time.deltaTime));
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(enemy.transform.position + (Vector3)enemy.hitBoxData.AttackHitBoxEnemy(dir.x,dir.y).center, enemy.hitBoxData.AttackHitBoxEnemy(dir.x, dir.y).size);
    }


    public bool CheckAttackRange()
    {
        offset.Set(
        enemy.transform.position.x + (enemy.hitBoxData.AttackHitBoxEnemy(dir.x,dir.y).center.x),
        enemy.transform.position.y + (enemy.hitBoxData.AttackHitBoxEnemy(dir.x, dir.y).center.y)
        );
        detected = Physics2D.OverlapBoxAll(offset, enemy.hitBoxData.AttackHitBoxEnemy(dir.x, dir.y).size, 0f, (enemy.hitBoxData.DetectableLayers));

        if (detected.Length > 0)
        {
            isInAttackRange = true;
            return true;
        }
        else 
        {
            isInAttackRange = false;
            return false; 
        }
    }




    /*private IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("wait");
    }*/



}

