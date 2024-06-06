
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    public abstract void Attack();

}
