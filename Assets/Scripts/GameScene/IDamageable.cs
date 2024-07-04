using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public interface IDamageable
{
    public float Hp{ get;}
    public bool TakeDamage(IDamageDealer dealer, WeaponSO optionalWeapon = null);
}

