using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODE_ColliderDetection : MonoBehaviour
{
    public bool _InCollision;
    public uint _TagIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            _InCollision = true;
            _TagIndex = 1;
        }
        else if (collision.CompareTag("Enemy"))
        {
            _InCollision = true;
            _TagIndex = 2;
        }
        else
        {
            _InCollision = false;
            _TagIndex = 0;
        }
    }
}
