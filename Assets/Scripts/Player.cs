using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform bulletSpawnLocation;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] float shootVolume;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float deathVolume;

    InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        StartCoroutine(ShootLoop());    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        //print(moveValue);
        transform.position += new Vector3(moveValue.x, 0, 0) * moveSpeed * Time.deltaTime;
    }

    IEnumerator ShootLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //print("Firing");
            Bullet bullet = BulletsPool.Instance.GetBullet();
            bullet.transform.SetPositionAndRotation(bulletSpawnLocation.position, new Quaternion(0, 0, 0, 1));
            bullet.SetColor(true);

            AudioManager.Instance.PlaySFX(shootSFX, shootVolume);
        }
    }

    private void OnDestroy()
    {
        AudioManager.Instance.PlaySFX(deathSFX, deathVolume);
    }
}
