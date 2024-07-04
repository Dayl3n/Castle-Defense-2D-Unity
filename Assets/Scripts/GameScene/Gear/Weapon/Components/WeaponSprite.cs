using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : WeaponComponent<WeaponSpriteData,AttackSprites>
{
    private SpriteRenderer baseSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;


    private Sprite[] currentAttackSprites;
 

    private int currentWeaponSpriteIndex;

    protected override void Awake()
    {
        base.Awake();

        baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();

        data = weapon.WeaponData.GetData<WeaponSpriteData>();
        // baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        //weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        

        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);

        weapon.OnEnter += HandleEnter;

    }

    protected override void OnDisable()
    {
        base.OnDisable();

        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        //currentWeaponSpriteIndex = 0;
        weapon.OnEnter -= HandleEnter;
    }

    protected override void HandleEnter()
    {
        currentAttackSprites = data.CurrentSprites();
        base.HandleEnter();
    }

    protected override void HandleExit()
    {
        base.HandleExit();

        currentWeaponSpriteIndex = 0;
    }

    private void HandleBaseSpriteChange(SpriteRenderer spriteRenderer)
    {
        if (!isAttackActive)
        {
            weaponSpriteRenderer.sprite = null;
            return;
        }

        currentAttackSprites = data.CurrentSprites();
        if (currentAttackSprites.Length <= currentWeaponSpriteIndex)
        {
            currentWeaponSpriteIndex = 0;
        }

        weaponSpriteRenderer.sprite = currentAttackSprites[currentWeaponSpriteIndex];
        currentWeaponSpriteIndex++;
    }




}

[Serializable]
public class WeaponSprites
{
    [field: SerializeField] public Sprite[] Sprites;


}
