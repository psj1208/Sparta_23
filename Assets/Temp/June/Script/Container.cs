using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField][Range(0, 1)] float TimeBetweenPop;
    [SerializeField] List<GameObject> objs;
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        InvokeRepeating(nameof(Pop), 1, TimeBetweenPop);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        objs.Add(collision.gameObject);
    }

    void Pop()
    {
        if (objs.Count <= 0)
            return;
        objs[0].transform.position = new Vector3(transform.position.x, col.bounds.max.y - 0.5f, 0);
        objs[0].GetComponent<Rigidbody2D>().velocity = Physics.gravity;
        objs.RemoveAt(0);
    }
}
