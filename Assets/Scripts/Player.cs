using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Transform bulletSpawnLocation;
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float shootInterval;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] float shootVolume;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float deathVolume;

    [Header("Cheats")]
    [SerializeField] bool forceField;

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

        //Rotate ship
        if(transform.localEulerAngles.z < 70f || transform.localEulerAngles.z > 270f)
        {
            transform.RotateAround(transform.position, Vector3.forward, -moveValue.x * rotateSpeed * Time.deltaTime);
        }

        //Rotate ship back to 0 when not moving
        //if (moveValue.x == 0) transform.RotateAround(transform.position, Vector3.forward, -transform.localEulerAngles.z * rotateSpeed * Time.deltaTime);
        if (moveValue.x == 0) transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, rotateSpeed * Time.deltaTime);
        //print($"{moveValue.x}");
        //print($"{transform.localEulerAngles.z}");
    }

    IEnumerator ShootLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            //print("Firing");
            Bullet bullet = BulletsPool.Instance.GetBullet();
            bullet.transform.SetPositionAndRotation(bulletSpawnLocation.position, new Quaternion(0, 0, 0, 1));
            bullet.SetColor(true);

            AudioManager.Instance.PlaySFX(shootSFX, shootVolume);
        }
    }

    public void Explode()
    {
        if (forceField) return;

        print("Exploding");
        AudioManager.Instance.PlaySFX(deathSFX, deathVolume);
        UIManager.Instance.GameOver();
        gameObject.SetActive(false);
    }
}
