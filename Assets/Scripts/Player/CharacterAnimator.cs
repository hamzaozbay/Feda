using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    [HideInInspector] public AnimatorOverrideController overrideController;

    protected AnimationClip[] currentAnimationSet;
    protected CharacterCombat combat;
    protected Animator anim;

    [SerializeField] private AnimationClip _replaceableAttackAnimation;
    [SerializeField] private AnimationClip[] _defaultAnimationSet;

    private const float _locomotionAnimationSmoothTime = .1f;
    private NavMeshAgent _agent;
    private AnimatorOverrideController _defaultController;



    protected virtual void Start() {
        _agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        _defaultController = overrideController;

        currentAnimationSet = _defaultAnimationSet;

        combat.onAttack += OnAttack;
    }



    protected virtual void Update() {
        float speedPercent = _agent.velocity.magnitude / _agent.speed;
        anim.SetFloat("speedPercent", speedPercent, _locomotionAnimationSmoothTime, Time.deltaTime);
        anim.SetBool("inCombat", combat.InCombat);
    }


    protected virtual void OnAttack() {
        anim.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAnimationSet.Length);

        if (_replaceableAttackAnimation != null)
            overrideController[_replaceableAttackAnimation.name] = currentAnimationSet[attackIndex];
    }


    public Animator GetAnimator() {
        return anim;
    }

    public AnimatorOverrideController DefaultController { get { return _defaultController; } }
    public AnimationClip[] DefaultAnimationSet { get { return _defaultAnimationSet; } }

}
