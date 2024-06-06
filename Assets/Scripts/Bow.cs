
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private Arrow _arrow;
    [SerializeField] private Animator _animator;

    private float _cooldown = 1;
    private float _lastAttack;

    public override void Attack()
    {
        if (Time.time - _lastAttack > _cooldown)
        {
            _lastAttack = Time.time;
            
            _animator.Play("Attack");

            Arrow arrow = Instantiate(_arrow, transform.position, transform.rotation);
            arrow.Init(20, 20);
        }
    }
}
