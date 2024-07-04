using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract class Item:ScriptableObject
{
    [SerializeField]
    protected int price;
    protected bool isEquiped;
    [SerializeField]
    protected string itemName;

    public bool isBought = false;

    [SerializeField]
    protected string description;
    public bool IsEquiped { get { return isEquiped; } }
    public int Price { get { return price; } }
    public string Name { get { return itemName; } }

    public string Desc { get => description; }

    public abstract void Equip(Player player);

    public virtual void BuyItem(Player player)
    {
        isBought = true;
    }
}

