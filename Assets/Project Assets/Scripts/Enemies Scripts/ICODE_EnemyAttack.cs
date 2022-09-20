using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemies
{
    /// <summary>
    /// Interface de ataque do inimigo
    /// </summary>
    public interface IEnemyAttack
    {
        public void AttackToTower();
    }
}