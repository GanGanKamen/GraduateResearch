using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BloodDecalPool : MonoBehaviour
{
    [Header("Decal prefabs")]
    public GameObject[] BloodPrefabs;
    [Header("Autodestroy time")]
    public float DecalsKillTime;
    [Header("Sizes")]
    public float decalSizeMin;
    public float decalSizeMax;

    private ParticleDecalData[] _particleData;
    private List<ParticleDecalData> _particleDataList;
    private ParticleSystem.Particle[] _particles;

    private int _countOfDecals;
  
    private List<GameObject> _spawnedDecals;

    private bool _isDecalsDestroyed = true;
    private int _particleHitCallCount;
    private int _currentParticleDecalDataIndex;
    private float _timeToDestroyDecals;

    /// <summary>
    /// How many decals to spawn
    /// </summary>
    /// <param name="count"></param>
    public void SetCount(int count)  
    {
        _countOfDecals = count;
        UpdateData();
    }

    //Reinitialize fields
    private void UpdateData()
    {
        _particles = new ParticleSystem.Particle[_countOfDecals];
        _particleData = new ParticleDecalData[_countOfDecals];
        _spawnedDecals = new List<GameObject>();

        for (int i = 0; i < _countOfDecals; i++)
        {
            _particleData[i] = new ParticleDecalData();
        }

        _particleDataList = new List<ParticleDecalData>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="particleCollisionEvent"></param>
    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent)
    {
        _particleHitCallCount++;
        if (_particleHitCallCount > _countOfDecals)
            return;
        SetParticleData(particleCollisionEvent);
        SpawnPrefabs();
    }

    private void SetParticleData(ParticleCollisionEvent particleCollisionEvent)
    {
        if (_currentParticleDecalDataIndex >= _countOfDecals)
        {
            _currentParticleDecalDataIndex = 0;
        }

        Vector3 collidedObjectRotation = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        collidedObjectRotation.z = Random.Range(0, 360);

        _particleData[_currentParticleDecalDataIndex].position = particleCollisionEvent.intersection;
        _particleData[_currentParticleDecalDataIndex].rotation = collidedObjectRotation;
        _particleData[_currentParticleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);

        _particleDataList.Add(_particleData[_currentParticleDecalDataIndex]);

        _currentParticleDecalDataIndex++;
    }

    private void SpawnPrefabs()
    {

        for (int i = _particleDataList.Count - 1; i >= 0; i--)
        {
            GameObject debugObject = Instantiate(GetRandomBloodObject());
            debugObject.transform.position = _particleDataList[i].position;
            debugObject.transform.eulerAngles = _particleDataList[i].rotation;
            debugObject.transform.localScale = new Vector3(_particleDataList[i].size, _particleDataList[i].size, _particleDataList[i].size);

            _spawnedDecals.Add(debugObject);

        }

        _particleDataList.Clear();
        _isDecalsDestroyed = false;
        ResetDestroyTime();

    }

    /// <summary>
    /// Returns random object from all prefabs array
    /// </summary>
    /// <returns></returns>
    private GameObject GetRandomBloodObject()
    {
        int randomNumber = Random.Range(0, BloodPrefabs.Length-1);
        return BloodPrefabs[randomNumber];
    }

    /// <summary>
    /// Reset the autodestroy timer
    /// </summary>
    private void ResetDestroyTime()
    {
        _timeToDestroyDecals = DecalsKillTime;
    }

    private void Update()
    {
        if (!_isDecalsDestroyed)
        {
            _timeToDestroyDecals -= Time.deltaTime;
            if(_timeToDestroyDecals < 0f)
            {
                DestroyDecals();
            }
        }
    }

    /// <summary>
    /// Destroy spawned decals gameobjects
    /// </summary>
    private void DestroyDecals()
    {
        for (int i = _spawnedDecals.Count-1; i >= 0 ; i--)
        {
            Destroy(_spawnedDecals[i]);
        }
        _spawnedDecals.Clear();
        _isDecalsDestroyed = true;
    }
    
}
