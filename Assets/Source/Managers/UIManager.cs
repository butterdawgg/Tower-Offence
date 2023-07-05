using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausedWindow;
    [SerializeField] private GameObject settingsWindow;
    [SerializeField] private GameObject exitWindow;

    [SerializeField] private Button settingsButton;
    [SerializeField] private Button settingsBackButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button exitConfirmButton;
    [SerializeField] private Button exitBackButton;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        Resume();

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
    }

    private void Pause()
    {
        IsPaused = true;

        Time.timeScale = 0f;

        pausedWindow.SetActive(true);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(false);
    }

    private void Resume()
    {
        IsPaused = false;

        Time.timeScale = 1f;

        pausedWindow.SetActive(false);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(false);
    }

    private void OnSettingsButtonClick()
    {
        pausedWindow.SetActive(false);
        settingsWindow.SetActive(true);
        exitWindow.SetActive(false);
    }

    private void OnSettingsBackButtonClick()
    {
        pausedWindow.SetActive(true);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(false);
    }

    private void OnExitButtonClick()
    {
        pausedWindow.SetActive(false);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(true);
    }

    private void OnExitConfirmButtonClick()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    private void OnExitBackButtonClick()
    {
        pausedWindow.SetActive(true);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(false);
    }
}