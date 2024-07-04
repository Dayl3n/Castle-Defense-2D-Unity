using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum ArmorType {Helmet, Torso, Hands, Legs, Shoes}

[CreateAssetMenu(fileName = "Armor", menuName = "ScriptableObjects/New Armor", order = 1)]
public class ArmorSO : Item
{
    [SerializeField]
    private ArmorType armorType;

    public Sprite icon;

    [SerializeField]
    private float bonusHP;
    [SerializeField]
    private int dmgBlockedPercentage;

    [SerializeField]
    private AnimationClip[] animationClipsHelmet;
    [SerializeField]
    private AnimationClip[] animationClipsTorso;
    [SerializeField]
    private AnimationClip[] animationClipsHands;
    [SerializeField]
    private AnimationClip[] animationClipsLegs;
    [SerializeField]
    private AnimationClip[] animationClipsShoes;

    
    internal ArmorType MyArmorTyp
    {
        get
        {
            return armorType;
        }
    }
    public int DmgBlockedPercentage { get => dmgBlockedPercentage; }
    public float BonusHP { get => bonusHP;}

    public override void Equip(Player player)
    {
        isEquiped = !isEquiped;
        Debug.Log($"Armor {itemName} is equiped: {isEquiped}");
        
    }

}
