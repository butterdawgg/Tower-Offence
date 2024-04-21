using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private GameObject settingsWindow;

    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button settingsBackButton;

    [SerializeField] private TextMeshProUGUI mainMenuText;

    private void Awake()
    {
        mainWindow.SetActive(true);
        settingsWindow.SetActive(false);

        startButton.onClick.AddListener(OnStartButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        settingsBackButton.onClick.AddListener(OnSettingsBackButtonClick);
    }

    private void Update()
    {
        mainMenuText.rectTransform.localScale = Vector3.one + (new Vector3(Mathf.Sin(Time.time * 1.5f), Mathf.Sin(Time.time * 1.5f), Mathf.Sin(Time.time * 1.5f)) * 0.05f);
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    private void OnSettingsButtonClick()
    {
        mainWindow.SetActive(false);
        settingsWindow.SetActive(true);
    }

    private void OnSettingsBackButtonClick()
    {
        mainWindow.SetActive(true);
        settingsWindow.SetActive(false);
    }
}
