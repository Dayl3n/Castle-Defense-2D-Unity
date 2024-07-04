using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopEnterLeaveScript : MonoBehaviour
{
    public static bool isMenuOpen;

    [SerializeField]
    private GUIScript gui;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject shopMenu,upgradeMenu;

    // Update is called once per frame

    private void Awake()
    {
        shopMenu.SetActive(false);
        upgradeMenu.SetActive(false);
    }
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOpen)
            {

                ResumeGame();

            }
            else
            {
                PauseGameShop();
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (isMenuOpen)
            {

                ResumeGame();

            }
            else
            {
                PauseGameUpgrade();
            }
        }
        
    }

    private void PauseGameShop()
    {
        shopMenu.SetActive(true);
        gui.HideGUI();
        isMenuOpen = true;
    }

    private void PauseGameUpgrade()
    {
        upgradeMenu.SetActive(true);
        gui.HideGUI();
        isMenuOpen = true;
    }

    private void ResumeGame()
    {
        shopMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        gui.gameObject.SetActive(true);
        gui.ShowGUI();
        Time.timeScale = 1.0f;
        isMenuOpen = false;
    }


}
