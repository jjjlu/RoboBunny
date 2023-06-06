using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraYMin;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, Mathf.Max(player.position.y, cameraYMin), transform.position.z);
    }
}
