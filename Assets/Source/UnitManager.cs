using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.SceneManagement;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Unit[] unitPrototypes;
    [SerializeField] private SplineContainer sc;
    [Header("Boundary")]
    [SerializeField] private float minX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxX;
    [SerializeField] private float maxZ;

    private List<Unit> units;

    private bool levelStarted = false;

    public UnitManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        units = new List<Unit>();

        foreach (Unit u in FindObjectsOfType<Unit>())
        {
            units.Add(u);
        }

        foreach (Unit u in units)
        {
            u.SC = sc;
            u.MinX = minX;
            u.MaxX = maxX;
            u.MinZ = minZ;
            u.MaxZ = maxZ;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & !levelStarted)
        {
            levelStarted = true;

            foreach (Unit u in units)
                u.OnLevelStart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(1);
    }
}