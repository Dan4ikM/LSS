using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private GameObject WinPanel;
    [SerializeField]
    private GameObject LosePanel;
    [SerializeField]
    private GameObject GamePanel;

    [SerializeField]
    private Sheep SheepPrefab;
    [SerializeField]
    private int SheepsCount = 10;
    [SerializeField]
    private Sheep PlayerSheepPrefab;
    [SerializeField]
    private FloatingJoystick PlayerJoystick;

    [SerializeField]
    private Platform PlatformPrefab;
    [SerializeField]
    private float AreaDiameter = 25f;


    private Sheep _player;
    private List<Sheep> _sheeps;
    private Platform _platform;

    // Start is called before the first frame update
    public void StartGame()
    {
        _sheeps = new List<Sheep>();
        SheepsSpawn();
        _platform = Instantiate(PlatformPrefab);
        _platform.Initialize(AreaDiameter, Vector3.zero, this);
        ///Change one of Sheep to player
        ///------
        ///Start ground generate platforms
    }

    public void SheepDestroying(Sheep sheep)
    {
        if (sheep.Equals(_player))
        {
            LosePanel.SetActive(true);
            EndGame();
        }
        else
        {
            _sheeps.Remove(sheep);
            if (_sheeps.Count == 0)
            {
                WinPanel.SetActive(true);
                EndGame();
            }
        }
        Destroy(sheep.gameObject);
    }

    private void SheepsSpawn()
    {
        Vector3 randPosition = GetRandomPositionInCircle((AreaDiameter - 1) / 2f);

        _player = Instantiate(PlayerSheepPrefab, randPosition, Quaternion.Euler(0f, Random.Range(0, 360f), 0f));
        _player.Initialize(PlayerJoystick);

        for (int i = 0; i < SheepsCount-1; i++)
        {
            randPosition = GetRandomPositionInCircle((AreaDiameter - 1) / 2f);

            Sheep newSheep = Instantiate(SheepPrefab, randPosition, Quaternion.Euler(0f, Random.Range(0, 360f), 0f));
            _sheeps.Add(newSheep);
        }
    }

    private Vector3 GetRandomPositionInCircle(float radius) //OnPlant
    {
        float randX = Random.Range(-radius, radius);
        float randZArea = Mathf.Sqrt(radius * radius - randX * randX);
        float randZ = Random.Range(-randZArea, randZArea);
        return new Vector3(randX, 0, randZ);
    }

    public void EndGame()
    {
        foreach (Sheep sheep in _sheeps)
        {
            Destroy(sheep.gameObject);
        }
        _sheeps.Clear();
        Destroy(_player.gameObject);
        Destroy(_platform.gameObject);
    }
}
