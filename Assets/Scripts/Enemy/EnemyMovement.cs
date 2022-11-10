using System;
using UnityEngine;
using UnityEngine.AI;

namespace ChessPuzzle.Game.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _lookRadius = 10f;
        [SerializeField] private GameObject chess;

        private void Update()
        {
            float distance = Vector3.Distance(chess.transform.position, transform.position);

            if (distance <= _lookRadius)
            {
                _agent.SetDestination(chess.transform.position);
            }
            else
            {
                _agent.SetDestination(transform.position);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _lookRadius);
        }
    }
}