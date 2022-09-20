using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class CODE_EnemyClass : MonoBehaviour
    {
        public GameObject _ObjRef;
        public GameObject _ColliderLeft;
        public GameObject _ColliderRight;
        public float _Speed;
        public float _TimeDelay;

        protected bool InCombat = false;
        
        private Vector3 target;
        private bool bIsMoving = false;
        private bool bCourotineEnd = true;


        /// <summary>
        /// Metodo responsavel por fazer a movimentacao do inimigo
        /// </summary>
        public void Movimentation()
        {
            if (bIsMoving) // Translada o inimigo para a proxima posicao
            {
                Vector3 enemyTranslate = target - _ObjRef.transform.position;
                _ObjRef.transform.Translate(enemyTranslate.normalized * _Speed * Time.deltaTime, Space.World);

                if (Vector3.Distance(_ObjRef.transform.position, target) <= 0.01f)
                {
                    bIsMoving = false;
                }
            }
            else // Quando a posicao eh alcancada, starta um Delay e troca para uma nova posicao
            {
                if (bCourotineEnd)
                {
                    StartCoroutine(TranslateDelay(_TimeDelay));
                    target = _ObjRef.transform.position + SelectDirection();
                }

            }
        }

        /// <summary>
        /// Funcao de Delay
        /// </summary>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        private IEnumerator TranslateDelay(float delayTime)
        {
            bCourotineEnd = false;
            yield return new WaitForSeconds(delayTime);
            bIsMoving = true;
            bCourotineEnd = true;
        }

        /// <summary>
        /// Randomiza uma direcao
        /// </summary>
        private Vector3 SelectDirection()
        {
            // Randomiza a direcao
            bool randomSeed, leftCol, rightCol;
            randomSeed = System.Convert.ToBoolean(Random.Range(0, 2));

            // Verifica Colisao a frente
            leftCol = _ColliderLeft.GetComponent<CODE_ColliderDetection>()._InCollision;
            rightCol = _ColliderRight.GetComponent<CODE_ColliderDetection>()._InCollision;

            Debug.Log(_ColliderLeft.GetComponent<CODE_ColliderDetection>()._InCollision);
            Debug.Log(_ColliderRight.GetComponent<CODE_ColliderDetection>()._InCollision);

            if (leftCol || rightCol)
            {
                 // Caso haja colisao verifica se eh uma torre ou um aliado
                if (_ColliderLeft.GetComponent<CODE_ColliderDetection>()._TagIndex == TagDetected.Tower || _ColliderRight.GetComponent<CODE_ColliderDetection>()._TagIndex == TagDetected.Tower)
                {
                    //ESTA EM MODO DE ATAQUE
                    InCombat = true;
                    return new Vector3(0f, 0f, 0f);
                }
                else
                {
                    //Movimenta para uma direcao possivel
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

                    InCombat = false;
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

                InCombat = false;
            }            
        }
    }
}
