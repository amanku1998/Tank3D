using UnityEngine;

[CreateAssetMenu(fileName = "NewTankWave", menuName = "Wave System/Tank Wave")]
public class TankWaveData : ScriptableObject
{
    [System.Serializable]
    public class EnemyTankWaveInfo
    {
        public EnemyTankType tankType;
        public int spawnCount;
    }

    public EnemyTankWaveInfo[] tanksInWave;
    public int initialSpawnCount = 2;
    public float spawnDelay = 0.5f;
}
