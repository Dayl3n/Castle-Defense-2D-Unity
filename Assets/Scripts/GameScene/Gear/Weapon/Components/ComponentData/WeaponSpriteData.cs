using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponSpriteData : ComponentData//<AttackSprites>
{
    public WeaponSpriteData()
    {
        ComponentDependency = typeof(WeaponSprite);
    }

    [field: SerializeField] public AttackSprites[] sprites {  get; private set; }



    public Sprite[] CurrentSprites() //Sprite[]
    {
        if (PlayerMovementState.Up)
        {
            return sprites[0].Sprites;
        }
        if (PlayerMovementState.Left)
        {
            return sprites[1].Sprites;
        }
        if (PlayerMovementState.Down)
        {
            return sprites[2].Sprites;
        }
        if (PlayerMovementState.Right)
        {
            return sprites[3].Sprites;
        }

        return sprites[2].Sprites;
    }
}

