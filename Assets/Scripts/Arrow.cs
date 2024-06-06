
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Arrow : MonoBehaviour
{
    private Rigidbody _rb;
    private float _damage;

    public void Init(float speed, float damage)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * speed;

        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = Vector3.zero;
        transform.parent = collision.transform;
    }
}
