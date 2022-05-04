using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float _damage = 2f, _radius = 1f;
    [SerializeField] LayerMask _layerMask;
    private void Update()
    {
        //bir sphere yaratiyoruz. _layerMask carptigimizi algilayacagimiz layer
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        //bir seye vurursak demek.
        if (hits.Length > 0)
        {
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(_damage);
        }
    }

}
