using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Serializable]
    struct SpawnType {
        [SerializeField] GameObject prefab;
        [SerializeField] float cost;
        [SerializeField] AnimationCurve weight;
    }
    [SerializeField] private float radius = 3;

    [SerializeField] private float spawnEarnRate;
    [SerializeField] private AnimationCurve spawnEarnRateCurve;
    [SerializeField] private List<SpawnType> spawns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos() {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    }
}
