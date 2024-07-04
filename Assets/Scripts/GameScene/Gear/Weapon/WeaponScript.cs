using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class WeaponScript : MonoBehaviour
{
    public WeaponSO WeaponData { get; private set; }


    public event Action OnExit;
    public event Action OnEnter;

    private Animator anim;
    private GameObject baseGameObject;


    private WeaponAnimationEventHendler eventHendler;

    public Animator Anim { get => anim; }
    public GameObject BaseGameObject { get => baseGameObject;}
    public GameObject WeaponSpriteGameObject { get; private set; }

    private void Awake()
    {

        baseGameObject = transform.Find("Base").gameObject;
        anim = baseGameObject.GetComponent<Animator>();
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

        eventHendler = baseGameObject.GetComponent<WeaponAnimationEventHendler>();
    }

    private void Start()
    {
        baseGameObject = transform.Find("Base").gameObject;
        anim = baseGameObject.GetComponent<Animator>();
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
    }

    public void Enter()
    {
        anim.SetBool("active", true);

        OnEnter?.Invoke();
    }

    private void Exit()
    {
        anim.SetBool("active", false);
        OnExit?.Invoke();
    }

    private void OnEnable()
    {
        eventHendler.OnFinish += Exit;
    }

    private void OnDisable()
    {
        eventHendler.OnFinish -= Exit;
    }
    public string GetWeaponInfo()
    {
        return $" attacked";
    }


    public void SetData(WeaponSO data)
    {
        WeaponData = data;
    }

}

