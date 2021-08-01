using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HealthUI : MonoBehaviour {

    [SerializeField] private GameObject _healthUiPrefab;
    [SerializeField] private GameObject _healthValuePrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _healthValueTarget;

    private float _visibleTime = 5f;
    private float _lastMadeVisibleTime;
    private Transform _ui, _valueUI;
    private Image _healthSlider;
    private Transform _cam;
    private Canvas _worldCanvas;



    private void Start() {
        _cam = Camera.main.transform;
        _worldCanvas = GameManager.instance.WorldSpaceCanvas;

        _ui = Instantiate(_healthUiPrefab, _worldCanvas.transform).transform;
        _healthSlider = _ui.GetChild(0).GetComponent<Image>();
        _ui.gameObject.SetActive(false);

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }


    private void OnHealthChanged(int maxHealth, int currentHealth, int changedValue) {
        if (_ui != null) {
            _ui.gameObject.SetActive(true);
            _lastMadeVisibleTime = Time.time;

            float healthPercent = currentHealth / (float)maxHealth;
            _healthSlider.fillAmount = healthPercent;

            if (currentHealth <= 0) {
                Destroy(_ui.gameObject);
            }

            _valueUI = Instantiate(_healthValuePrefab, _worldCanvas.transform).transform;
            _valueUI.position = _healthValueTarget.position;
            _valueUI.GetComponent<TextMeshProUGUI>().text = changedValue.ToString();
            _valueUI.GetComponent<ChangedHealthValue>().target = _healthValueTarget;
        }
    }


    private void LateUpdate() {
        if (_ui != null) {
            _ui.position = _target.position;
            _ui.forward = -_cam.forward;

            if (Time.time - _lastMadeVisibleTime > _visibleTime) {
                _ui.gameObject.SetActive(false);
            }
        }
    }

}
