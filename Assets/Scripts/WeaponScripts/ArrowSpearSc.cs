using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpearSc : MonoBehaviour
{
    Rigidbody _rg;
    public float _speed = 30f;
    public float _destroyTime = 3f;
    public float _damage = 15f;
    private void Awake()
    {
        _rg = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        //Invoke is useful if you want to do something one time only.
        Invoke("DestroyGameObject", _destroyTime);
    }
    public void Launch(Camera camera)
    {
        _rg.velocity = camera.transform.forward * _speed;
        //gidecegi rotasyonu ayarliyoruz. yonunu ayarliyoruz.
        transform.LookAt(transform.position + _rg.velocity);
        //baktigi yon instantiate ederken verilebilir.
    }
    void DestroyGameObject()
    {
        //destroy ile yazilabilir
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
