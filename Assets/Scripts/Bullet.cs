using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material red, blue;

    bool isPlayerBullet;
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetColor(bool isPlayer)
    {
        isPlayerBullet = isPlayer;

        if (isPlayer)
        {
            meshRenderer.material = blue;
        }
        else
        {
            meshRenderer.material = red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isPlayerBullet)
        {
            BulletsPool.Instance.ReleaseBullet(this);
            other.GetComponent<Enemy>().Explode();
        }
        else if (other.CompareTag("Player") && !isPlayerBullet)
        {
            BulletsPool.Instance.ReleaseBullet(this);
            other.GetComponent<Player>().Explode();
        }
        else if (other.CompareTag("Finish"))
        {
            BulletsPool.Instance.ReleaseBullet(this);
        }   
    }
}
