using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpearSc : MonoBehaviour
{
    Rigidbody _rg;
    [SerializeField] float _speed = 30f;
    [SerializeField] float _destroyTime = 3f;
    [SerializeField] float _damage = 15f;
    private void Awake()
    {
        _rg = GetComponent<Rigidbody>();
    }
    public void Launch(Camera camera)
    {
        _rg.velocity = camera.transform.forward * _speed;
        //gidecegi rotasyonu ayarliyoruz. yonunu ayarliyoruz.
        transform.LookAt(transform.position + _rg.velocity);
        //baktigi yon instantiate ederken verilebilir.
        DestroyGameObject();
    }
    void DestroyGameObject()
    {
        if (gameObject.activeInHierarchy)
            Destroy(this.gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.ENEMY_TAG)
        {
            other.GetComponent<HealthScript>().ApplyDamage(_damage);
            Destroy(gameObject, 1f);
        }
    }
}
