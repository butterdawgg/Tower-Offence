using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;

    private void Awake()
    {
        backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
    }

    private void OnBackToMenuButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
