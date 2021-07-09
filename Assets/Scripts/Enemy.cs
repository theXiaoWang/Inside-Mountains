using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private float timeTillDestroy;
    [SerializeField] private int scorePerHit = 15;

    private ScoreBoard _scoreBoard;

    private void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }

    private void ProcessHit()
    {
        _scoreBoard.UpdateScore(scorePerHit);
    }
    private void KillEnemy()
    {
        //Instantiate the vfx in runtime
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        //Annihilate this object
        Destroy(gameObject, timeTillDestroy);
    }

}
