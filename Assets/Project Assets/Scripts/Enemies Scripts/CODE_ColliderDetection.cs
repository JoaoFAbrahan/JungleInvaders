using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Tags de retorno da colisao
    /// </summary>
    public enum TagDetected
    {
        Tower, Enemy
    };

    /// <summary>
    /// Detector de colisao do inimigo
    /// </summary>
    public class CODE_ColliderDetection : MonoBehaviour
    {
        public bool _InCollision;
        public TagDetected _TagIndex;
        //public uint _TagIndex;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 
            if (collision.CompareTag("Tower"))
            {
                _InCollision = true;
                _TagIndex = TagDetected.Tower;
            }
            else if (collision.CompareTag("Enemy"))
            {
                _InCollision = true;
                _TagIndex = TagDetected.Enemy;
            }
        }
    }
}