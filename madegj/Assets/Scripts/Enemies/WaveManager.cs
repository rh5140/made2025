using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class WaveManager : MonoBehaviour
    {
        private enum WaveState
        {
            WaitingForNextWave,
            InWave
        }

        [Header("Depends")]

        [SerializeField]
        private ProtagCore protag1;

        [SerializeField]
        private ProtagCore protag2;

        [SerializeField]
        private List<EnemyCore> enemyPool;

        [Header("Config")]

        [SerializeField]
        private float timeBetweenWaves;

        [SerializeField]
        private float innerSpawnRadius;

        [SerializeField]
        private float outerSpawnRadius;

        [SerializeField]
        private int initialEnemyCount;

        [SerializeField]
        private int increasePerWave;

        public static WaveManager Instance { get; private set; }

        private int currentWaveNumber;

        private float nextWaveTimer;

        private List<EnemyCore> enemyInstances = new();
        private List<EnemyCore> livingEnemies = new();
        private int remainingEnemies;

        private WaveState waveState = WaveState.WaitingForNextWave;

        private void Awake()
        {
            Instance = this;
            currentWaveNumber = 1;
            nextWaveTimer = currentWaveNumber;
        }

        private void Update()
        {
            if (waveState == WaveState.WaitingForNextWave)
            {
                nextWaveTimer -= Time.deltaTime;
                if (nextWaveTimer <= 0)
                {
                    waveState = WaveState.InWave;
                    SpawnWave();
                    currentWaveNumber++;
                }
            }
        }

        private void OnDrawGizmos()
        {
            // Draw the spawn area for waves
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, innerSpawnRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, outerSpawnRadius);
        }

        private void SpawnWave()
        {
            enemyInstances.Clear();
            livingEnemies.Clear();

            int enemyCount = initialEnemyCount + (currentWaveNumber - 1) * increasePerWave;
            // Spawn enemies for the current wave
            for (var i = 0; i < enemyCount; i++)
            {
                int randomIndex = Random.Range(0, enemyPool.Count);
                Vector2 pos = Random.insideUnitCircle.normalized * Random.Range(innerSpawnRadius, outerSpawnRadius);
                EnemyCore enemy = Instantiate(enemyPool[randomIndex], pos, Quaternion.identity);
                enemy.Initialize(protag1, protag2);

                enemy.OnDefeated.AddListener(OnEnemyDefeated);
                enemyInstances.Add(enemy);
                livingEnemies.Add(enemy);
            }
        }

        private void OnEnemyDefeated(EnemyCore defeated)
        {
            remainingEnemies--;
            livingEnemies.Remove(defeated);

            if (remainingEnemies <= 0)
            {
                foreach (EnemyCore enemy in enemyInstances)
                {
                    enemy.CleanupCorpse();
                }

                nextWaveTimer = timeBetweenWaves;
                waveState = WaveState.WaitingForNextWave;
            }
        }

        public IReadOnlyList<EnemyCore> GetLivingEnemies()
        {
            return livingEnemies;
        }
    }
}