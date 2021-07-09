using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f;
    [SerializeField] private ParticleSystem crashVFX;
    private bool crashVFXNotPlayed = true;
    private void OnTriggerEnter(Collider other)
    {
        if (crashVFXNotPlayed)
        {
            crashVFX.Play();
            crashVFXNotPlayed = false;
        }
        StartCrashSequence();
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void StartCrashSequence()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControl>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }
}
