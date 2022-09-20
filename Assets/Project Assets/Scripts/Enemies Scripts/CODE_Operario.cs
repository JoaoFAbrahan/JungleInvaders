using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class CODE_Operario : CODE_EnemyClass, IEnemyAttack
    {
        public int _Health;
        public int _Damage;


        void Start()
        {

        }

        void Update()
        {
            // Movimenta o inimigo
            Movimentation();

            if (InCombat)
            {
                AttackToTower();
            }
        }


        public void AttackToTower()
        {
            // ATAQUE
        }
    }
}
