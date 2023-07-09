using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private GameObject startLevelWindow;
    [SerializeField] private GameObject stopLevelWindow;
    [SerializeField] private GameObject shopWindow;
    [Space]
    [SerializeField] private GameObject pausedWindow;
    [SerializeField] private GameObject pausedMainWindow;
    [SerializeField] private GameObject settingsWindow;
    [SerializeField] private GameObject exitWindow;
    [Space]
    [SerializeField] private Button startLevelButton;
    [SerializeField] private Button stopLevelButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button shopBackButton;
    [Space]
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button settingsBackButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button exitConfirmButton;
    [SerializeField] private Button exitBackButton;
    [Space]
    [SerializeField] private TextMeshProUGUI moneyText;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        Resume();

        mainWindow.SetActive(true);
        startLevelWindow.SetActive(true);
        stopLevelWindow.SetActive(false);
        shopWindow.SetActive(false);

        pausedWindow.SetActive(false);
        pausedMainWindow.SetActive(true);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(false);

        startLevelButton.onClick.AddListener(OnStartLevelButtonClick);
        stopLevelButton.onClick.AddListener(OnStopLevelButtonClick);
        shopButton.onClick.AddListener(OnShopButtonClick);
        shopBackButton.onClick.AddListener(OnShopBackButtonClick);

        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        settingsBackButton.onClick.AddListener(OnSettingsBackButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
        exitConfirmButton.onClick.AddListener(OnExitConfirmButtonClick);
        exitBackButton.onClick.AddListener(OnExitBackButtonClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
                Pause();
            else
                Resume();
        }

        moneyText.text = "balance: " + SerializeManager.GetFloat(FloatType.Money);
    }

    private void Pause()
    {
        IsPaused = true;

        Time.timeScale = 0f;

        mainWindow.SetActive(false);
        pausedWindow.SetActive(true);
    }

    private void Resume()
    {
        IsPaused = false;

        Time.timeScale = 1f;

        mainWindow.SetActive(true);
        pausedWindow.SetActive(false);
    }

    private void OnStartLevelButtonClick()
    {
        if (UnitManager.Instance.CanStartLevel())
        {
            UnitManager.Instance.StartLevel();

            startLevelWindow.SetActive(false);
            stopLevelWindow.SetActive(true);
            shopWindow.SetActive(false);
        }
    }
    private void OnStopLevelButtonClick()
    {
        UnitManager.Instance.StopLevel();

        startLevelWindow.SetActive(true);
        stopLevelWindow.SetActive(false);
        shopWindow.SetActive(false);
    }
    private void OnShopButtonClick()
    {
        startLevelWindow.SetActive(false);
        stopLevelWindow.SetActive(false);
        shopWindow.SetActive(true);
    }
    private void OnShopBackButtonClick()
    {
        startLevelWindow.SetActive(true);
        stopLevelWindow.SetActive(false);
        shopWindow.SetActive(false);
    }
    private void OnSettingsButtonClick()
    {
        pausedMainWindow.SetActive(false);
        settingsWindow.SetActive(true);
        exitWindow.SetActive(false);
    }
    private void OnSettingsBackButtonClick()
    {
        pausedMainWindow.SetActive(true);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(false);
    }
    private void OnExitButtonClick()
    {
        pausedMainWindow.SetActive(false);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(true);
    }
    private void OnExitConfirmButtonClick()
    {
        UnitManager.Instance.SellEverything();

        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }
    private void OnExitBackButtonClick()
    {
        pausedMainWindow.SetActive(true);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(false);
    }
}