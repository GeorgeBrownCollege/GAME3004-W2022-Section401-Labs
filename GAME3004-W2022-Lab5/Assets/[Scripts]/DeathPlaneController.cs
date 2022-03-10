using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject;
            var characterController = player.GetComponent<CharacterController>();

            characterController.SimpleMove(Vector3.zero);
            characterController.enabled = false;
            player.transform.position = playerSpawnPoint.position;
            characterController.enabled = true;
        }
    }
}
