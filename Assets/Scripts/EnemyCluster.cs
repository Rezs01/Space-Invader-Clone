using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCluster : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveLimit;

    bool movingRight;
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //Moves the enemy cluster back and forth between the moveLimit
        //transform.position += new Vector3(moveDirection, 0, 0) * moveSpeed * Time.deltaTime;
        //if (transform.position.x >= moveLimit * moveDirection) moveDirection *= -1;

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
}
