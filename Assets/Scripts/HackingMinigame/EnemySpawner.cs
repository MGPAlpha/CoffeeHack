using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Serializable]
    class SpawnType {
        [SerializeField] public GameObject prefab;
        [SerializeField] public float cost;
        [SerializeField] public float weightScale;
        [SerializeField] public AnimationCurve weight;
    }
    [SerializeField] private float radius = 3;

    [SerializeField] private float duration = 10*60;
    [SerializeField] private float spawnEarnRate = 2;
    [SerializeField] private AnimationCurve spawnEarnRateCurve;
    [SerializeField] private List<SpawnType> spawns;

    private bool started = false;
    private float time = 0;

    private float earned = 0;
    private SpawnType nextSpawn = null;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
    }

    void StartSpawning() {
        started = true;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;
        float timePercent = time / duration;
        timePercent = Mathf.Min(timePercent, 1);

        if (nextSpawn == null) {
            IEnumerable<(SpawnType, float)> evaluatedWeights = from s in spawns select (s, s.weight.Evaluate(timePercent) * s.weightScale);
            float weightSum = (from s in evaluatedWeights select s.Item2).Sum();

            float r = UnityEngine.Random.Range(0, weightSum);
            foreach ((SpawnType, float) s in evaluatedWeights) {
                if (r < s.Item2) {
                    nextSpawn = s.Item1;
                    break;
                } else {
                    r -= s.Item2;
                }
            }
        }

        float currEarnRate = spawnEarnRateCurve.Evaluate(timePercent) * spawnEarnRate;
        earned += currEarnRate * Time.deltaTime;

        if (nextSpawn != null && earned >= nextSpawn.cost) {
            Spawn(nextSpawn);
            earned -= nextSpawn.cost;
            nextSpawn = null;
        }

        time += Time.deltaTime;
    }

    void Spawn(SpawnType s) {
        float spawnAngle = UnityEngine.Random.Range(-180, 180);
        Vector3 spawnDir = Quaternion.Euler(0, 0, spawnAngle) * Vector3.up;
        Vector3 spawnPos = transform.position + spawnDir * radius;
        HackEnemy newEnemy = Instantiate(s.prefab, spawnPos, Quaternion.identity).GetComponent<HackEnemy>();
        newEnemy.target = HackPlayer.Instance.transform;
    }

    #if UNITY_EDITOR
    void OnDrawGizmos() {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    }
    #endif
}
