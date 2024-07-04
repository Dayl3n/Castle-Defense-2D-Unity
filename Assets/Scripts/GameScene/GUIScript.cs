using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour
{
    [SerializeField] private Player playerData;
    [SerializeField] private UpgradeSystem upgradeSystem;

    private Slider sliderHealth,sliderExp;

    [field: SerializeField] public GameObject HealthBar {  get; private set; }
    [field: SerializeField] public GameObject ExpBar { get; private set; }
    [SerializeField] private TMP_Text coinsTxt, DeathTxt;
    private int coins;
    // Start is called before the first frame update
    void Awake()
    {
        DeathTxt.enabled = false;
        sliderHealth = transform.Find("HealthBar").gameObject.GetComponent<Slider>();
        sliderHealth.maxValue = playerData.Stats.CurrentHp;
        sliderHealth.value = playerData.Stats.CurrentHp;

        sliderExp = transform.Find("ExpBar").gameObject.GetComponent<Slider>();
        sliderExp.maxValue = upgradeSystem.ExpRequire;
        sliderExp.value = 0;

        coins = playerData.Coins;
        coinsTxt.text = $"Coins: {coins}";
    }

    void Update()
    {
        if (playerData != null && upgradeSystem != null)
        {
            if(sliderHealth.value != playerData.Stats.CurrentHp)
            {
                Debug.Log("Hp Changed");
                sliderHealth.value =playerData.Stats.CurrentHp;
            }
            if(coins != playerData.Coins)
            {
                coins = playerData.Coins;
                coinsTxt.text = $"Coins: {coins}";
            }

            if(sliderExp.value != playerData.Exp)
            {
                sliderExp.value = playerData.Exp;
            }

            if(sliderExp.maxValue != upgradeSystem.ExpRequire)
            {
                sliderExp.maxValue = upgradeSystem.ExpRequire;
            }
        }
        if (playerData == null || playerData.isDead)
        {
            DeathTxt.enabled = true;
        }
        else
        {
            DeathTxt.enabled = false;
        }
    }

    public void ShowGUI()
    {
        FindObjectOfType<TextMeshProUGUI>().enabled =true;
        HealthBar.SetActive(true);
        ExpBar.SetActive(true);
    }

    public void HideGUI()
    {
        FindObjectOfType<TextMeshProUGUI>().enabled = false;
        HealthBar.SetActive(false);
        ExpBar.SetActive(false);
    }
}
