using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    [SerializeField] private float _lookRadius = 10f;
    [SerializeField] private Outline _silhouetteOutline;

    private Transform _target;
    private NavMeshAgent _agent;
    private CharacterCombat _combat;
    private CharacterStats _myStats;
    private Collider[] _colliders;



    private void Start() {
        _target = GameManager.instance.Player.transform;
        _agent = GetComponent<NavMeshAgent>();
        _combat = GetComponent<CharacterCombat>();
        _myStats = GetComponent<CharacterStats>();

        _colliders = GetComponents<Collider>();

        _myStats.OnDied += Died;
    }


    private void Update() {
        if (_myStats.CurrentHealth <= 0f) {
            if (_agent.hasPath) _agent.ResetPath();
            return;
        }

        if (_target == null) { return; }

        float distance = Vector3.Distance(_target.position, this.transform.position);

        if (distance <= _lookRadius) {
            _silhouetteOutline.enabled = true;
            _agent.SetDestination(_target.position);

            if (distance <= _agent.stoppingDistance && _combat.GetCanAttack() && !_combat.isAttacking) {
                CharacterStats targetStats = _target.GetComponent<CharacterStats>();
                if (targetStats != null)
                    _combat.Attack(targetStats);

                FaceTarget();
            }
        }
        else {
            _agent.ResetPath();
            _silhouetteOutline.enabled = false;
        }
    }


    private void FaceTarget() {
        Vector3 direction = (_target.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }


    private void Died() {
        foreach (Collider col in _colliders) {
            col.enabled = false;
        }
        _agent.enabled = false;
    }
}
