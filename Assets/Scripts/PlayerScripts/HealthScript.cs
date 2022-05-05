using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    EnemyAnimationController _enemyAnim;
    PlayerStats _playerStats;
    NavMeshAgent _navMeshAgent;
    EnemyController _enemyController;
    public float _health = 100f;
    [SerializeField] bool _isPlayer, _isBoar, _isCannibal;
    bool _isDead;
    EnemyAudio _enemyAudio;
    private void Awake()
    {
        //burasi enumla yapilabilir.
        if (_isBoar || _isCannibal)
        {
            EnemyRef();

        }
        if (_isPlayer)
        {
            _playerStats = GetComponent<PlayerStats>();
        }
    }
    public void ApplyDamage(float _damage)
    {
        if (_isDead)
            return;

        _health -= _damage;

        if (_isPlayer)
        {
            _playerStats.DisplayHealthStat(_health);
        }
        if (_isBoar || _isCannibal)
        {
            //saldiri yaptigimizda bize saldiri yapmasini mumkun kiliyoruz.
            if (_enemyController.EnemyStateReturner == EnemyStates.PATROL)
                _enemyController._chaseDistance = 100f;
        }
        if (_health <= 0f)
        {
            PlayerDied();
            _isDead = true;
        }
    }
    private void PlayerDied()
    {
        if (_isCannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 10f);
            _enemyController.enabled = false;
            _navMeshAgent.enabled = false;
            _enemyAnim.enabled = false;
        }
        if (_isBoar)
        {
            _navMeshAgent.velocity = Vector3.zero;
            _navMeshAgent.isStopped = true;
            _enemyController.enabled = false;
            _enemyAnim.Dead();
            StartCoroutine(DeadSound());
        }
        if (_isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            //foreach (var item in enemies)
            //{
            //    item.GetComponent<EnemyController>().enabled = false;
            //}
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }
        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }
    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(.3f);
        _enemyAudio.PlayDeadSound();
    }
    void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    private void EnemyRef()
    {
        _enemyAnim = GetComponent<EnemyAnimationController>();
        _enemyController = GetComponent<EnemyController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyAudio = GetComponentInChildren<EnemyAudio>();
    }
}
