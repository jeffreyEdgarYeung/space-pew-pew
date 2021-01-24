using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int waveIdx = 1;
    // Start is called before the first frame update
    void Start()
    {
        var currWave = waveConfigs[waveIdx];
        StartCoroutine(SpawnWaveEnemies(currWave));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnWaveEnemies(WaveConfig currWave)
    {
        for( var i=0; i<currWave.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate
            (
                currWave.GetEnemyPrefab(),
                currWave.GetWaypoints()[0].transform.position,
                currWave.GetEnemyPrefab().transform.rotation
            );

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(currWave);
            yield return new WaitForSeconds(currWave.GetTimeBetweenSpawns());
        }
        
    }
}
