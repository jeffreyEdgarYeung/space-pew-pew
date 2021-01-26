using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] float waveStartDelay = 0f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(waveStartDelay);
        StartCoroutine(SpawnInfiniteWaves());
        
    }

   private IEnumerator SpawnAllWaves()
    {
        for ( var waveIdx = startingWave; waveIdx < waveConfigs.Count; waveIdx++)
        {
            var currWave = waveConfigs[waveIdx];
            yield return StartCoroutine(SpawnWaveEnemies(currWave));
        }
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

    private IEnumerator SpawnInfiniteWaves()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());

        } while (looping);
    }
}
