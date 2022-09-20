using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class CODE_EnemyClass : MonoBehaviour
    {
        public GameObject _ObjRef;
        public float _Speed;
        public float _TimeDelay;
        public int _Health;
        public int _Damage;

        private Vector3 target;
        private bool bIsMoving = false;
        private bool courotineEnd = true;
        public GameObject colliderLeft;
        public GameObject colliderRight;


        void Start()
        {
            //colliderLeft = _ObjRef.transform.Find("Collider_Left").gameObject;
            //colliderRight = _ObjRef.transform.Find("Collider_Right").gameObject;
            bIsMoving = false;
            courotineEnd = true;
        }


        /// <summary>
        /// M�todo respons�vel por fazer a movimenta��o do inimigo
        /// </summary>
        public void Movimentation()
        {
            if (bIsMoving) // Translada o inimigo para a proxima posi��o
            {
                Vector3 enemyTranslate = target - _ObjRef.transform.position;
                _ObjRef.transform.Translate(enemyTranslate.normalized * _Speed * Time.deltaTime, Space.World);

                if (Vector3.Distance(_ObjRef.transform.position, target) <= 0.01f)
                {
                    bIsMoving = false;
                }
            }
            else // Quando a posi��o � alcan�ada, starta um Delay e troca para uma nova posi��o
            {
                if (courotineEnd)
                {
                    StartCoroutine(TranslateDelay(_TimeDelay));
                    target = _ObjRef.transform.position + SelectDirection();
                }

            }
        }

        public void AttackTower()
        {
            /*ADICIONAR M�TODO*/
        }

        /// <summary>
        /// Fun��o de Delay
        /// </summary>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        private IEnumerator TranslateDelay(float delayTime)
        {
            courotineEnd = false;
            yield return new WaitForSeconds(delayTime);
            bIsMoving = true;
            courotineEnd = true;
        }

        /// <summary>
        /// Randomiza uma dire��o
        /// </summary>
        private Vector3 SelectDirection()
        {
            // Randomiza a dire��o
            bool randomSeed, leftCol, rightCol;
            randomSeed = System.Convert.ToBoolean(Random.Range(0, 2));

            // Verifica Colis�o a frente
            leftCol = colliderLeft.GetComponent<CODE_ColliderDetection>()._InCollision;
            rightCol = colliderRight.GetComponent<CODE_ColliderDetection>()._InCollision;

            if (leftCol || rightCol)
            {
                 // Caso haja colis�o verifica se � uma torre ou um aliado
                if (colliderLeft.GetComponent<CODE_ColliderDetection>()._TagIndex == 1 || colliderRight.GetComponent<CODE_ColliderDetection>()._TagIndex == 1)
                {
                    //ATAQUE
                    AttackTower();
                    return new Vector3(0f, 0f, 0f);
                }
                else
                {
                    //Movimenta para uma dire��o poss�vel
                    if (leftCol && (this.transform.position.x + 0.5f) < 4.51f && !rightCol)
                    {
                        // Move a Direita
                        return new Vector3(0.5f, -0.755f, 0f);
                    }
                    else if (rightCol && (this.transform.position.x - 0.5f) > (-7.51f) && !leftCol)
                    {
                        // Move a Esquerda
                        return new Vector3(-0.5f, -0.755f, 0f);
                    }
                    else
                    {
                        return new Vector3(0f, 0f, 0f);
                    }
                }
            }
            else
            {
                // Move o Inimigo
                if (randomSeed)
                {
                    // Verifica limite do mapa
                    if ((this.transform.position.x + 0.5f) > 4.51f)
                    {
                        return new Vector3(-0.5f, -0.755f, 0f);
                    }
                    else
                    {
                        // Move a Direita
                        return new Vector3(0.5f, -0.755f, 0f);
                    }
                }
                else
                {
                    if ((this.transform.position.x - 0.5f) < (-7.51f))
                    {
                        return new Vector3(0.5f, -0.755f, 0f);
                    }
                    else
                    {
                        // Move a Esquerda
                        return new Vector3(-0.5f, -0.755f, 0f);
                    }
                }
            }            
        }
    }
}
