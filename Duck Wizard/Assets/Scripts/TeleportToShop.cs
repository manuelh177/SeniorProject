using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script "teleports" the player to the shop, or some other specified scene, by loading that scene.

public class TeleportToShop : MonoBehaviour
{
    [SerializeField] private string sceneName = "shopScene";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
