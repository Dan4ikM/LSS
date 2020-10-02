
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float TimeBeforeMoving = 5f;
    [SerializeField]
    private float TimeDriving = 3f;
    [SerializeField]
    private float Size = 5f;
    [SerializeField]
    private float SizeMultiplier = 0.8f;
    [SerializeField]
    private Light HintLight;
    [SerializeField]
    private Vector3 Distance = new Vector3(0f,3f,0f);

    private float _areaDiameter;
    private Vector3 _startpoint;
    
    private GameLoop _gameLoop;

    public void Initialize(float areaDiameter, Vector3 startPoint, GameLoop gameLoop)
    {
        _areaDiameter = areaDiameter;
        _startpoint = startPoint;
        _gameLoop = gameLoop;
        Initialize();
    }

    private void Initialize()
    {
        float radiusAccommodation = (_areaDiameter - Mathf.Sqrt(2) * Size) / 2f;
        float randX = UnityEngine.Random.Range(-radiusAccommodation, radiusAccommodation);
        float randZArea = Mathf.Sqrt(radiusAccommodation * radiusAccommodation - randX * randX);
        float randZ = UnityEngine.Random.Range(-randZArea, randZArea);

        transform.position = new Vector3(randX,0,randZ) + _startpoint;
        transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0, 360f), 0f);

        StartCoroutine(Cycle());
    }

    private void ChangeSize()
    {
        transform.localScale = new Vector3(transform.localScale.x * SizeMultiplier, transform.localScale.y, transform.localScale.z * SizeMultiplier);
        HintLight.spotAngle *= SizeMultiplier;
        Size *= SizeMultiplier;
    }

    private IEnumerator Cycle()
    {
        yield return new WaitForSeconds(TimeBeforeMoving / 2);
        HintLight.enabled = true;
        yield return new WaitForSeconds(TimeBeforeMoving / 2);
        HintLight.enabled = false;

        Vector3 deltaShift = Distance / TimeDriving;
        yield return Movement(deltaShift);
        yield return Movement(-deltaShift);

        ChangeSize();
        Initialize();
    }


    private IEnumerator Movement(Vector3 direction)
    {
        float currentTimer = 0;
        while (currentTimer <= TimeDriving)
        {
            transform.Translate(direction * Time.deltaTime);
            currentTimer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Sheep>() != null)
        {
            _gameLoop.SheepDestroying(other.GetComponent<Sheep>());
        }
    }
}
