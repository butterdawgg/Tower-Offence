using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Diagnostics;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Unit[] unitPrototypes;
    [SerializeField] private SplineContainer sc;
    [Header("Boundary")]
    [SerializeField] private int minX;
    [SerializeField] private int minZ;
    [SerializeField] private int maxX;
    [SerializeField] private int maxZ;

    private List<Unit> units;
    private List<Enemy> enemies;

    private bool levelStarted = false;

    public static UnitManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        if (SerializeManager.GetFloat(FloatType.Money) < 100f)
            SerializeManager.SetFloat(FloatType.Money, 100f);

        units = new List<Unit>();

        foreach (Unit u in FindObjectsOfType<Unit>())
        {
            units.Add(u);
        }

        enemies = new List<Enemy>();

        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            enemies.Add(e);
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

    public bool CanStartLevel()
    {
        return !levelStarted & units.Count > 0;
    }

    public void StartLevel()
    {
        if (!levelStarted & units.Count > 0)
        {
            levelStarted = true;

            foreach (Unit u in units)
            {
                u.StartPosition = u.transform.position;
                u.OnLevelStart();
            }
        }
    }

    public void StopLevel()
    {
        if (levelStarted & units.Count > 0)
        {
            levelStarted = false;

            foreach (Unit u in units)
            {
                u.OnLevelStop();
                u.gameObject.SetActive(true);
                u.IsDead = false;
                u.Health = u.MaxHealth;

                u.transform.position = u.StartPosition;
            }

            foreach(Enemy e in enemies)
            {
                e.gameObject.SetActive(true);
                e.IsDead = false;
                e.Health = e.MaxHealth;
            }

            foreach(Projectile p in FindObjectsOfType<Projectile>())
            {
                Destroy(p.gameObject);
            }
        }
    }

    public void Buy(int unitID)
    {
        if (SerializeManager.GetFloat(FloatType.Money) - unitPrototypes[unitID].Price < 0f)
            return;

        int number = 0;
        foreach (Unit u in units)
        {
            if (u.Type == unitPrototypes[unitID].Type)
                number++;
        }
        if (number >= unitPrototypes[unitID].MaxNumber)
            return;

        Vector3Int random;
        bool canSpawn;
        do
        {
            canSpawn = true;
            random = new Vector3Int(Random.Range(minX, maxX + 1), 1, Random.Range(minZ, maxZ + 1));

            foreach (Unit u in units)
            {
                if (random == Vector3Int.RoundToInt(u.transform.position))
                {
                    canSpawn = false;
                }
            }
        }
        while (canSpawn == false);

        Unit unit = Instantiate(unitPrototypes[unitID].gameObject, random, Quaternion.Euler(0f, 90f, 0f), default).GetComponent<Unit>();
        unit.SC = sc;
        unit.MinX = minX;
        unit.MaxX = maxX;
        unit.MinZ = minZ;
        unit.MaxZ = maxZ;
        units.Add(unit);

        SerializeManager.SetFloat(FloatType.Money, SerializeManager.GetFloat(FloatType.Money) - unitPrototypes[unitID].Price);
    }

    public void Sell(int unitID)
    {
        List<Unit> unitsForSaleList = new List<Unit>();

        foreach (Unit u in units)
        {
            if (u.Type == unitPrototypes[unitID].Type)
                unitsForSaleList.Add(u);
        }

        if (unitsForSaleList.Count == 0)
            return;

        Unit[] unitsForSale = unitsForSaleList.ToArray();

        int random = Random.Range(0, unitsForSale.Length);

        units.Remove(unitsForSale[random]);

        SerializeManager.SetFloat(FloatType.Money, SerializeManager.GetFloat(FloatType.Money) + unitsForSale[random].Price);

        Destroy(unitsForSale[random].gameObject);
    }

    public void SellEverything()
    {
        float sum = 0f;

        foreach (Unit u in units)
        {
            sum += u.Price;
        }

        SerializeManager.SetFloat(FloatType.Money, SerializeManager.GetFloat(FloatType.Money) + sum);
    }

    public float GetPrice(int unitID)
    {
        return unitPrototypes[unitID].Price;
    }

    private void OnDestroy()
    {
        float sum = 0f;

        foreach (Unit u in units)
        {
            sum += u.Price;
        }

        SerializeManager.SetFloat(FloatType.Money, SerializeManager.GetFloat(FloatType.Money) + sum);
    }
}