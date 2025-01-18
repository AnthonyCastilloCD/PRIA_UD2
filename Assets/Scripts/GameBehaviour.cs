using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class GameBehaviour : MonoBehaviour
{
    //Variable TextUI
    public int MaxItems = 4;

    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;

    public Button LoseButton;

    void Start()
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;
    }

    public Button WinButton;

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }
    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected; }
        set {
            _itemsCollected = value;
            Debug.LogFormat("Items: {0}", _itemsCollected);
            ItemText.text = "Items Collected: " + Items;
            if(_itemsCollected >= MaxItems)
            {
                //ProgressText.text = "You've found all the items!";
                //Time.timeScale = 0f;
                WinButton.gameObject.SetActive(true);
                UpdateScene("You've found all the items!");

            }
            else 
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected)
                + " more!";
            }
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

     private int _playerHP = 10;
    public int HP
    {
        get { return _playerHP; }
        set {
            _playerHP = value;
            HealthText.text = "Player Health: " + HP;
            Debug.LogFormat("Lives: {0}", _playerHP);

            if (_playerHP <= 0)
            {
                //ProgressText.text = "You want another life with that?";
                //Time.timeScale = 0;
                LoseButton.gameObject.SetActive(true);
                UpdateScene ("You want another life with that?");
            }
            else
            {
                ProgressText.text = "Ouch... That's got hurt.";
            }
        }
    }
}
