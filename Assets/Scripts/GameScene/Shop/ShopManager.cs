using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Linq;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public Item[] shopItems;
    public ShopTamplate[] shopTamplates;

    public TMP_Text coinsText;



    void Start()
    {
        for (int i = shopItems.Length; i < shopTamplates.Length; i++)
        {
            shopTamplates[i].gameObject.SetActive(false);
        }
        coinsText.text = $"Coins: {player.Coins}";
        LoadPanels();
    }

    // Update is called once per frame
    void Update()
    {

        if(player == null) return;
        if (coinsText.isActiveAndEnabled)
        {
            coinsText.text = $"Coins: {player.Coins}";
        }
        else
        {
            coinsText.enabled = true;
        }
        
    }



    public void LoadPanels()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopTamplates[i].nameTxt.text = shopItems[i].Name;
            shopTamplates[i].descriptionTxt.text = shopItems[i].Desc;
            shopTamplates[i].priceTxt.text = $"Coins: {shopItems[i].Price}";

            shopTamplates[i].btnTxt.text = shopItems[i].isBought ? "Equip" : "Buy";
        }
    }

    public void PurchesItem(int i)
    {
        if (player.Coins >= shopItems[i].Price)
        {
            shopItems[i].BuyItem(player);
            if (!shopItems[i].isBought)
                player.Coins -= shopItems[i].Price;
            player.Coins -= shopItems[i].Price;
            if (shopItems[i].GetType() == typeof(WeaponSO))
            {
                shopItems[i].Equip(player);

                LoadPanels();
            }
        }
    }
}
