using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingZone : MonoBehaviour
{
    [SerializeField] float forcePower;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid);
        if (rigid == null)
            return;
        rigid.AddForce(Vector3.left * forcePower);
    }
}
