using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject playerInstance;
    private Vector3 playerPos;
    [SerializeField] private float flyHeight = 2;
    [SerializeField] private float distToPlayer = 1;
    [SerializeField] private float speed;


    // Update is called once per frame
    void Update()
    {
        playerPos = playerInstance.transform.position;
        float moveX = playerInstance.transform.localPosition.x + distToPlayer;
        float moveY = flyHeight;
        float moveZ = playerInstance.transform.position.z;

        Vector3 moveTo = new Vector3(moveX, moveY, moveZ);
        transform.Translate(moveTo * Time.deltaTime * speed);


    }
}
