using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {

    [SerializeField] protected int cooldDown;
    [SerializeField] protected int damage;
    protected SkillManager skillManager;
    protected bool canExecute = true;



    private void Start() {
        skillManager = transform.parent.GetComponent<SkillManager>();
    }

    public virtual void Execute() {
        skillManager.anySkillPlaying = true;
    }

    protected void DonePlayingSkill() {
        skillManager.PlayerController.canMove = true;
        skillManager.PlayerMotor.GetAgent().angularSpeed = 5000f;
        skillManager.PlayerCombat.CanAttackTrue();
        skillManager.anySkillPlaying = false;
    }

    public bool CanExecute { get { return canExecute; } }
    public int CoolDown { get { return cooldDown; } }
}
