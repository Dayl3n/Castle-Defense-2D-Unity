using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
    [SerializeField]
    private float maxHp,strenght,agility,critChance,critDamge,attackValue,critModifire,agilityScalling;

    private float currentHp;

    public float Speed
    {
        get { return agility / 5; }
    }
    public float CurrentHp
    {
        get { return currentHp; }
    }

    public float AttackValue
    {
        get { return attackValue; }
    }

    public float CritChance
    {
        get
        {
            return critChance;
        }
    }



    public float CritDamge
    {
        get { return critDamge; }
    }


    public void ChangeHp(float value)
    {
        currentHp += value;
    }

    public Stats(float maxHp, float strenght, float agility, float critChance)
    {
        this.maxHp = maxHp;
        this.strenght = strenght;
        this.agility = agility;
        this.critChance = critChance;
        critModifire = 1.5f;
        agilityScalling = 0.7f;
        

        currentHp = this.maxHp;
        attackValue = this.agility * agilityScalling + this.strenght;
        critDamge = attackValue * critModifire;
    }

    public void UpgradeStrenght()
    {
        strenght++;
        attackValue = this.agility * agilityScalling + this.strenght;
        critDamge = attackValue * critModifire;
    }

    public void UpgradeAgility()
    {
        agility++;
        critChance += 2;
        attackValue = this.agility * agilityScalling + this.strenght;
        critDamge = attackValue * critModifire;
    }

    public void UpgradeHealth()
    {
        maxHp += 20;
        currentHp += 20;
    }

    public void UpgradeCrit()
    {
        critChance += 5;
        critModifire += 0.1f;
        critDamge = attackValue * critModifire;
    }




}
