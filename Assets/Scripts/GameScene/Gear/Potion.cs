using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Potion : Item
{
    public override void Equip(Player player)
    {
        Drink(player);
    }

    protected abstract void Drink(Player player);

    public override void BuyItem(Player player)
    {
        Drink(player);
    }


}

