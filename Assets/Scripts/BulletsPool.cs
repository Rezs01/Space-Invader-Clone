using UnityEngine;
using UnityEngine.Pool;

public class BulletsPool : MonoBehaviour
{
    public static BulletsPool Instance;

    [SerializeField] Bullet bullet;

    ObjectPool<Bullet> bulletsPool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        bulletsPool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(bullet),
            actionOnGet: (bullet) => bullet.gameObject.SetActive(true),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 30
        );
    }

    public Bullet GetBullet()
    {
        return bulletsPool.Get();
    }

    public void ReleaseBullet(Bullet bullet)
    {
        bulletsPool.Release(bullet);
    }
}
