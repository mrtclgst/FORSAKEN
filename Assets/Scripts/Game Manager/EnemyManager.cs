using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] GameObject _boarPrefab, _cannibalPrefab;
    [SerializeField] Transform[] _cannibalSpawnPoint, _boarSpawnPoints;
    [SerializeField] int _cannibalEnemyCount, _boarEnemyCount;
    int _initialCannibalCount, _initialBoarCount;
    [SerializeField] float _waitBeforeSpawnEnemy = 10f;

    private void Awake()
    {
        MakeInstance();
    }
    private void Start()
    {
        _initialBoarCount = _boarEnemyCount;
        _initialCannibalCount = _cannibalEnemyCount;
        SpawnEnemies();
        StartCoroutine(CheckToSpawnEnemies());
    }
    private void SpawnEnemies()
    {
        SpawnBoars();
        SpawnCannibal();
    }
    void SpawnCannibal()
    {
        int index = 0;
        for (int i = 0; i < _cannibalEnemyCount; i++)
        {
            if (index >= _cannibalSpawnPoint.Length)
                index = 0;
            else
                Instantiate(_cannibalPrefab, _cannibalSpawnPoint[i].position, Quaternion.identity);
            index++;
        }
        _cannibalEnemyCount = 0;
    }
    void SpawnBoars()
    {
        int index = 0;
        for (int i = 0; i < _boarEnemyCount; i++)
        {
            if (index >= _boarSpawnPoints.Length)
                index = 0;
            else
                Instantiate(_boarPrefab, _boarSpawnPoints[i].position, Quaternion.identity);
            index++;
        }
        _boarEnemyCount = 0;
    }
    public void EnemyDied(bool cannibal)
    {
        if (cannibal)
        {
            _cannibalEnemyCount++;
            ////daha fazla cannibal uretmesini engelliyoruz.
            if (_cannibalEnemyCount > _initialCannibalCount)
                _cannibalEnemyCount = _initialCannibalCount;

        }
        else
        {
            _boarEnemyCount++;
            //daha fazla domuz uretmesini engelliyoruz.
            if (_boarEnemyCount > _initialBoarCount)
                _boarEnemyCount = _initialBoarCount;
        }
    }
    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(_waitBeforeSpawnEnemy);
        SpawnBoars();
        SpawnCannibal();
        StartCoroutine(CheckToSpawnEnemies());
    }
    public void StopSpawning()
    {
        StopCoroutine(CheckToSpawnEnemies());
    }
    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }
}
