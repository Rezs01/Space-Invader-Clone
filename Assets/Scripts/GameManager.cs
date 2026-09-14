using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] EnemyCluster[] enemyClusters;

    int level = 0;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SpawnEnemyCluster() => StartCoroutine(SpawnEnemyClusterCoroutine());
    public IEnumerator SpawnEnemyClusterCoroutine()
    {
        yield return new WaitForSeconds(3f);

        if (level > 0) enemyClusters[level - 1].gameObject.SetActive(false);

        enemyClusters[level].gameObject.SetActive(true);
        enemyClusters[level].EnableAllEnemies();
        enemyClusters[level].Reset();
        level++;
        level = Mathf.Clamp(level, 0, enemyClusters.Length - 1);
    }

}
