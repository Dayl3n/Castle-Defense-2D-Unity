using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;




[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/New Weapon", order = 1)]
public class WeaponSO : Item
{
    [field: SerializeReference]public List<ComponentData> componentDatas {  get; private set; }

    [field: SerializeField] public RuntimeAnimatorController animatorController { get; private set; }
    

    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackValue;


    public T GetData<T>()
    {
    return componentDatas.OfType<T>().FirstOrDefault();
    }

    public void AddData(ComponentData data)
    {
        componentDatas.Add(data);
    }

    public float AttackRange
    {
        get { return attackRange; }
    }

    public float AttackValue
    {
        get { return attackValue; }
    }

    public override void Equip(Player player)
    {
        isEquiped = !isEquiped;
        Debug.Log($"Weapon {itemName} is equiped: {isEquiped}");

        player.weaponGenerator.GenerateWeapon(this);
    }

    public List<Type> GetAllDependencies()
    {
        return componentDatas.Select(component => component.ComponentDependency).ToList();
    }

    private void Awake()
    {
        isBought = false;
    }

}

