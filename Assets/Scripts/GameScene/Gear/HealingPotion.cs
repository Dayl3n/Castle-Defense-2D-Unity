using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "ScriptableObjects/Potion/HealthPotion", order = 1)]
public class HealingPotion : Potion
{
    [SerializeField]
    private float healValue;
    protected override void Drink(Player player)
    {
        player.Stats.ChangeHp(healValue);
    }
}

