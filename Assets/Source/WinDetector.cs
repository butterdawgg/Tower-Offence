using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UnitManager.Instance.SellEverything();

        SceneManager.LoadScene(2);
    }
}
