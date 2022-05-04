using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    EnemyAnimationController _EnemyAnimationController;
    NavMeshAgent _navAgent;
    EnemyStates enemyState;
    public float _walkSpeed = 0.5f, _runSpeed = 4f, _chaseDistance = 20f, _attackDistance = 2.2f;
    float _currentChaseDistance;//distance'i degistirdigimiz icin memory bir ekstra memory alani olusturuyoruz storage icin
    public float _chaseAfterAttackDistance = 2f; //playera az uzaklasma firsati taniyoruz.
    public float _patrolRadiusMin = 20f, _patrolRadiusMax = 60f, _patrolForThisTime = 15f;
    float _patrolTimer;
    public float _waitBeforeAttack = 2f;
    float _attackTimer;
    Transform target;
    [SerializeField] GameObject _attackPoint;
    private void Awake()
    {
        AwakeRef();
    }
    private void Start()
    {
        enemyState = EnemyStates.PATROL;
        _patrolTimer = _patrolForThisTime;//ilk seferinde yurumesini saglamak icin veriyoruz.
        //ilk seferinde dogrudan saldirip digerlerinde ayarlama yapacagiz
        _attackTimer = _waitBeforeAttack;
        //hafizaya aliyoruz mesafeyi ayni seyi tekrar kullanabilmek icin
        _currentChaseDistance = _chaseDistance;
    }
    private void Update()
    {
        if (enemyState == EnemyStates.PATROL)
        {
            Patrol();
        }
        if (enemyState == EnemyStates.CHASE)
        {
            Chase();
        }
        if (enemyState == EnemyStates.ATTACK)
        {
            Attack();
        }
    }
    private void Patrol()
    {
        //nav agenta hareket edebilecegini soyluyoruz
        _navAgent.isStopped = false;
        _navAgent.speed = _walkSpeed;
        //patrol icin saat timer olusturduk.
        _patrolTimer += Time.deltaTime;
        if (_patrolTimer > _patrolForThisTime)//kac sn bozmadan yuruyecegini belirliyoruz.
        {
            SetNewRandomDestination();
            _patrolTimer = 0f;
        }
        if (_navAgent.velocity.sqrMagnitude > 0)//navagent hareket ediyorsa
            _EnemyAnimationController.Walk(true);
        else
            _EnemyAnimationController.Walk(false);

        if (Vector3.Distance(transform.position, target.position) <= _chaseDistance)
        {
            _EnemyAnimationController.Walk(false);//run animasyonunu devreye sokacagiz 
            enemyState = EnemyStates.CHASE;

        }
    }
    private void Chase()
    {
        _navAgent.isStopped = false;
        _navAgent.speed = _runSpeed;
        //player'imiza dogru kosacak
        _navAgent.SetDestination(target.position);

        if (_navAgent.velocity.sqrMagnitude > 0)//navagent hareket ediyorsa
            _EnemyAnimationController.Run(true);
        else
            _EnemyAnimationController.Run(false);

        //playere yeterince yakinsak attack yapacagiz.
        if (Vector3.Distance(transform.position, target.position) <= _attackDistance)
        {
            _EnemyAnimationController.Run(false);
            _EnemyAnimationController.Walk(false);
            enemyState = EnemyStates.ATTACK;

            //Uzaktan enemye ates ettigimizde bizi tespit edip kosmasini istiyoruz.
            if (_chaseDistance != _currentChaseDistance)
                _chaseDistance = _currentChaseDistance;
        }
        else if (Vector3.Distance(transform.position, target.position) <= _chaseDistance)
        {
            _EnemyAnimationController.Run(false);
            enemyState = EnemyStates.PATROL;
            _patrolTimer = _patrolForThisTime;
            if (_chaseDistance != _currentChaseDistance)
                _chaseDistance = _currentChaseDistance;
        }
    }
    private void Attack()
    {
        //enemy'i durduruyoruz.
        _navAgent.velocity = Vector3.zero;
        _navAgent.isStopped = true;

        //timer baslatiyoruz.
        _attackTimer += Time.deltaTime;
        if (_attackTimer > _waitBeforeAttack)
        {
            _EnemyAnimationController.Attack();
            _attackTimer = 0f;
        }
        //player kactigi zaman yapilacak islemler
        if (Vector3.Distance(transform.position, target.position)
            >= _attackDistance + _chaseAfterAttackDistance/*aralik veriyoruz playerin kacabilmesi icin*/)
        {
            enemyState = EnemyStates.CHASE;
        }
    }
    private void SetNewRandomDestination()
    {
        float _randomRadius = UnityEngine.Random.Range(_patrolRadiusMin, _patrolRadiusMax);
        Vector3 _randomDirection = UnityEngine.Random.insideUnitSphere * _randomRadius;
        _randomDirection += transform.position;
        NavMeshHit _navMeshHit;
        //_randomDirectionu aliyor ve yurulebilir olan alanlarla karsilastiriyor.
        //eger yurunmez yerlerdense tekrardan hesap yapiyor belirledigimiz alan icerisinde.
        //-1 tum layerleri kontrol ediyor.
        NavMesh.SamplePosition(_randomDirection, out _navMeshHit, _randomRadius, -1);
        _navAgent.SetDestination(_navMeshHit.position);
    }
    public EnemyStates EnemyStateReturner { get; set; }
    void TurnOnAttackPoint()
    {
        _attackPoint.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if (_attackPoint.activeInHierarchy)
            _attackPoint.SetActive(false);
    }
    void AwakeRef()
    {
        _EnemyAnimationController = GetComponent<EnemyAnimationController>();
        _navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }
}
public enum EnemyStates
{
    PATROL, CHASE, ATTACK
}
