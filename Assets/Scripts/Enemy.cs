using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyCluster enemyCluster;
    [SerializeField] float shootChance;
    [SerializeField] float defeatScore;
    [SerializeField] Transform bulletSpawnLocation;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float deathVolume;

    private void OnEnable()
    {
        StartCoroutine(ShootLoop());
    }

    IEnumerator ShootLoop()
    {
        yield return new WaitForSeconds(Random.value);

        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (Random.value * 100f < shootChance)
            {
                //print("Firing");
                Bullet bullet = BulletsPool.Instance.GetBullet();
                bullet.transform.SetPositionAndRotation(bulletSpawnLocation.position, new Quaternion(0, 180, 0, 1));
                bullet.SetColor(false);
            }
        }
    }

    public void Explode()
    {
        enemyCluster.EnemyDefeated();
        UIManager.Instance.AddScore(defeatScore);
        AudioManager.Instance.PlaySFX(deathSFX, deathVolume);
        gameObject.SetActive(false);
    }
}
