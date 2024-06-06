
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    const string Vertical = "Vertical";
    const string Horizontal = "Horizontal";

    private float _speed = 10f;
    private Rigidbody _rb;
    private bool _isMove;
    
    public bool IsMove => _isMove;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move()
    {
        Vector3 direction = GetMoveDirection();

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);

        _rb.velocity = direction.normalized * _speed;

        _isMove = direction.magnitude != 0;
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 v = Input.GetAxis(Vertical) * cameraForward.normalized;
        Vector3 h = Input.GetAxis(Horizontal) * cameraRight.normalized;


        return v + h;
    }
}
