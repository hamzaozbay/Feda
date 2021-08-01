using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    [HideInInspector] public bool anySkillPlaying = false;

    [SerializeField] private Tornado _tornado;
    [SerializeField] private AttackSpeed _attackSpeed;
    [SerializeField] private MeteorAttack _meteorAttack;

    private Player _playerScript;
    private PlayerController _playerController;
    private CharacterCombat _playerCombat;
    private PlayerMotor _playerMotor;
    private PlayerAnimator _playerAnimator;
    private PlayerStats _playerStats;




    private void Awake() {
        _playerScript = transform.parent.GetComponent<Player>();
        _playerController = transform.parent.GetComponent<PlayerController>();
        _playerCombat = transform.parent.GetComponent<CharacterCombat>();
        _playerMotor = transform.parent.GetComponent<PlayerMotor>();
        _playerAnimator = transform.parent.GetComponent<PlayerAnimator>();
        _playerStats = transform.parent.GetComponent<PlayerStats>();
    }




    private void Update() {
        if (EquipmentManager.instance.GetCurrentEquipment()[(int)EquipmentSlot.Weapon] == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && !anySkillPlaying && _meteorAttack.CanExecute) {
            GameManager.instance.levelManager.SkillsUI.StartCountDown(0, _meteorAttack.CoolDown);
            _meteorAttack.Execute();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !anySkillPlaying && _tornado.CanExecute) {
            GameManager.instance.levelManager.SkillsUI.StartCountDown(1, _tornado.CoolDown);
            _tornado.Execute();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && _attackSpeed.CanExecute) {
            GameManager.instance.levelManager.SkillsUI.StartCountDown(2, _attackSpeed.CoolDown);
            _attackSpeed.Execute();
        }

    }


    public Tornado TornadoSkill { get { return _tornado; } }
    public AttackSpeed AttackSpeedSkill { get { return _attackSpeed; } }
    public MeteorAttack MeteorAttackSkill { get { return _meteorAttack; } }
    public Player Player { get { return _playerScript; } }
    public PlayerController PlayerController { get { return _playerController; } }
    public CharacterCombat PlayerCombat { get { return _playerCombat; } }
    public PlayerMotor PlayerMotor { get { return _playerMotor; } }
    public PlayerAnimator PlayerAnimator { get { return _playerAnimator; } }
    public PlayerStats PlayerStats { get { return _playerStats; } }

}
