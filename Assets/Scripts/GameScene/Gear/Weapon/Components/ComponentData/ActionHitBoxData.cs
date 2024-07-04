using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ActionHitBoxData : ComponentData
{

    [field: SerializeField] public LayerMask DetectableLayers { get; private set; }
    [field: SerializeField] private Rect[] hitBox;

    public bool Debug;
    public ActionHitBoxData()
    {
        ComponentDependency = typeof(ActionHitBox);
    }

    public Rect AttackHitBoxPlayer()
    {
        if (PlayerMovementState.Up)
        {
            return hitBox[0];
        }
        if (PlayerMovementState.Left)
        {
            return hitBox[1];
        }
        if (PlayerMovementState.Down)
        {
            return hitBox[2];
        }
        if (PlayerMovementState.Right)
        {
            return hitBox[3];
        }

        return hitBox[2];
    }

    public Rect AttackHitBoxEnemy(float x, float y)
    {
        if(y>0) { return hitBox[0]; } //up attack
        if(x<0) { return hitBox[1]; } //left attack
        if(y<0) { return hitBox[2]; } //down attack
        if(x>0) { return hitBox[3]; } //right attack

        return hitBox[2];
    }

}

