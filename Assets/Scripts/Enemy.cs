using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float shootChance;
    [SerializeField] float defeatScore;
    [SerializeField] Transform bulletSpawnLocation;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float deathVolume;

    private void Start()
    {
        StartCoroutine(ShootLoop());
    }

    IEnumerator ShootLoop()
    {
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

    private void OnDestroy()
    {
        UIManager.Instance.AddScore(defeatScore);
        AudioManager.Instance.PlaySFX(deathSFX, deathVolume);
    }
}
