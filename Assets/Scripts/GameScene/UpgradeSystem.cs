using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] Player playerData;

    private int expRequire=100, lvl;

    public int avaiablePoints;

    [SerializeField] private TMP_Text APTxt;

    private void Awake()
    {
        APTxt.text = "Avaiable Points";
    }

    private void Update()
    {

        APTxt.text = $"Avaiable Points: {avaiablePoints}";

    }

    public int LvL 
    {
        get { return lvl; }
        set 
        {
            avaiablePoints++;
            lvl = value;
            expRequire = (lvl + 1) * 100;
        }
    }


    public int AvaiablePoints { get => avaiablePoints; }
    public int ExpRequire { get => expRequire; }

    private bool CheckLvlUp()
    {
        Debug.Log("check");
        if(avaiablePoints>0)
        {
            avaiablePoints--;
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void UpgradeStrenght()
    {
        if(CheckLvlUp()) playerData.Stats.UpgradeStrenght();
    }

    public void UpgradeHealth()
    {
        if (CheckLvlUp()) playerData.Stats.UpgradeHealth();
    }

    public void UpgradeAgility()
    {
        if (CheckLvlUp()) playerData.Stats.UpgradeAgility();
    }

    public void UpgradeCrit()
    {
        if (CheckLvlUp()) playerData.Stats.UpgradeCrit();
    }
    




}

