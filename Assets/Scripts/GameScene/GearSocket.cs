using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour
{

    public Animator MyAnimator { get; set; }

    private SpriteRenderer spriteRenderer;

    private Animator parentAnimator;

    private AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentAnimator = GetComponentInParent<Animator>();
        MyAnimator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);
        MyAnimator.runtimeAnimatorController = animatorOverrideController;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip(AnimationClip[] animations)
    {
        spriteRenderer.color = Color.white;
        animatorOverrideController["Player_Walk_Down"] = animations[2];
        animatorOverrideController["Player_Walk_Up"] = animations[0];
        animatorOverrideController["Player_Walk_Left"] = animations[1];
        animatorOverrideController["Player_Walk_Right"] = animations[3];
    }

    public void DeEquip()
    {
        animatorOverrideController["Player_Walk_Down"] = null ;
        animatorOverrideController["Player_Walk_Up"] = null;
        animatorOverrideController["Player_Walk_Left"] = null;
        animatorOverrideController["Player_Walk_Right"] = null;

        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }
}
