using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCluster : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveLimit;
    [SerializeField] GameObject[] enemies;

    bool movingRight;
    int defeatedEnemies = 0;
    void Update()
    {
        Move();
    }

    void Move()
    {
        //Moves the enemy cluster back and forth between the moveLimit
        if (movingRight)
        {
            transform.localPosition += new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
            if (transform.localPosition.x >= moveLimit) movingRight = false;
        }
        else
        {
            transform.localPosition -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
            if (transform.localPosition.x <= -moveLimit) movingRight = true;
        }
    }
    public void EnableAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }
    public void EnemyDefeated()
    {
        defeatedEnemies++;
        if (defeatedEnemies >= enemies.Length)
        {
            GameManager.Instance.SpawnEnemyCluster();
        }
    }
}
