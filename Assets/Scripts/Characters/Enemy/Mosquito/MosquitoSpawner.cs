using System.Collections.Generic;
using UnityEngine;

public class MosquitoSpawner : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private EnemyMosquitoController _mosquitoPrefab;
    private List<EnemyMosquitoController> _mosquitos = new List<EnemyMosquitoController>();

    [SerializeField] private GameObject _target;

    private void SpawnMosuito()
    {
        EnemyMosquitoController newEnemy = Instantiate(_mosquitoPrefab);
        newEnemy.Spawner = this;
        _mosquitos.Add(newEnemy);
    }

    public void MoveMosquitos()
    {
        foreach(EnemyMosquitoController mosquito in _mosquitos)
        {
            mosquito.Move(_target);
        }
    }

    public void DestroyMosquito(EnemyMosquitoController mosquito)
    {
        if (_mosquitos.Contains(mosquito))
        {
            if (_isDebug) Debug.Log("Destroy mosquito with ID: " + mosquito.GetInstanceID());
            _mosquitos.Remove(mosquito);
        }
        else if (_isDebug) Debug.Log("Mosquito with ID: " + mosquito.GetInstanceID() + " not found!");
        
        Destroy(mosquito);
    } 
}
