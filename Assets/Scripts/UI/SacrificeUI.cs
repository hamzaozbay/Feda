using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SacrificeUI : MonoBehaviour {

    [SerializeField] private Image _bigSlotIcon;
    [SerializeField] private RectTransform _sacrificeTitle, _holder;
    [SerializeField]
    private TextMeshProUGUI _bigSlotItemName, _bigSlotItemDescription,
    _bigSlotItemDamage, _bigSlotItemArmor, _bigSlotSoulValue, _currentSoulValue, _neededSoulValue;
    [SerializeField] private GameObject _damageValueGameObject, _armorValueGameObject, _soulValueGameObject, _sacrificeButton, _addBackButton;
    [SerializeField] private Transform _inventorySlotsParent, _sacrificeSlotsParent;
    [SerializeField] private GameObject _acceptButton;


    private SacrificeSlot[] _inventorySlots;
    private SacrificeSlot[] _sacrificedSlots;
    private SacrificeSlot _currentSlot;
    private List<Item> _sacrificedItems = new List<Item>();
    private EndLevel _endLevel;
    private float _sacrificeTitleStartPos, _holderStartPos;
    private int _soulValue = 0;
    private CanvasGroup _acceptButtonCanvasGroup;



    private void Awake() {
        Inventory.instance.onItemChangedCallBack += FillInventorySlots;

        _inventorySlots = _inventorySlotsParent.GetComponentsInChildren<SacrificeSlot>();
        _sacrificedSlots = _sacrificeSlotsParent.GetComponentsInChildren<SacrificeSlot>();

        _acceptButtonCanvasGroup = _acceptButton.GetComponent<CanvasGroup>();
    }


    private void Start() {
        _sacrificeTitleStartPos = _sacrificeTitle.localPosition.y;
        _holderStartPos = _holder.localPosition.y;
    }


    public void SetUI() {
        ClearSacrificedSlots();
        FillInventorySlots();
        ClearFields();

        _neededSoulValue.text = GameManager.instance.levelManager.NeededSoulValue.ToString();
        _sacrificeTitle.DOLocalMoveY(430f, 1f).SetUpdate(true);
        _holder.DOLocalMoveY(40f, 1f).SetUpdate(true);
    }

    public void ResetUI() {
        _sacrificeTitle.localPosition = new Vector3(_sacrificeTitle.localPosition.x, _sacrificeTitleStartPos, _sacrificeTitle.localPosition.z);
        _holder.localPosition = new Vector3(_holder.localPosition.x, _holderStartPos, _holder.localPosition.z);

        _acceptButtonCanvasGroup.alpha = .25f;
        _acceptButtonCanvasGroup.interactable = false;
        _soulValue = 0;
        _currentSoulValue.text = _soulValue.ToString();
    }


    public void FillInventorySlots() {
        for (int i = 0; i < _inventorySlots.Length; i++) {
            if (i > Inventory.instance.Items.Count - 1) {
                _inventorySlots[i].ClearSlot();
                continue;
            }

            _inventorySlots[i].AddItem(Inventory.instance.Items[i]);
        }
    }


    public void SelectSlot(SacrificeSlot slot) {
        _currentSlot = slot;

        if (_currentSlot != null) {
            FillFields();
        }
        else {
            ClearFields();
        }
    }
    public void FillFields() {
        _bigSlotIcon.sprite = _currentSlot.GetItem().icon;
        _bigSlotIcon.enabled = true;
        _bigSlotItemName.text = _currentSlot.GetItem().itemName;
        _bigSlotItemDescription.text = _currentSlot.GetItem().itemDescription;
        _bigSlotSoulValue.text = _currentSlot.GetItem().soulValue.ToString();

        if (_currentSlot.GetItem() is Equipment) {
            Equipment equipment = (Equipment)_currentSlot.GetItem();
            _bigSlotItemDamage.text = equipment.damageModifier.ToString();
            _bigSlotItemArmor.text = equipment.armorModifier.ToString();

            _damageValueGameObject.SetActive(true);
            _armorValueGameObject.SetActive(true);
            _soulValueGameObject.SetActive(true);
        }
    }
    public void ClearFields() {
        _bigSlotIcon.sprite = null;
        _bigSlotIcon.enabled = false;
        _bigSlotItemName.text = null;
        _bigSlotItemDescription.text = null;

        _bigSlotItemDamage.text = null;
        _bigSlotItemArmor.text = null;
        _bigSlotSoulValue.text = null;
        _damageValueGameObject.SetActive(false);
        _armorValueGameObject.SetActive(false);
        _soulValueGameObject.SetActive(false);

        _sacrificeButton.SetActive(false);
    }



    public void SacrificeButton() {
        AddToSacrificedItems();
        ClearSacrificedSlots();
        FillSacrificedSlots();
        Inventory.instance.Remove(_currentSlot.GetItem());
        ClearFields();
        _currentSlot = null;
    }

    private void AddToSacrificedItems() {
        _sacrificedItems.Add(_currentSlot.GetItem());

        CalculateNeededSoul(_currentSlot.GetItem().soulValue);
    }

    private void FillSacrificedSlots() {
        for (int i = 0; i < _sacrificedItems.Count; i++) {
            if (_sacrificedSlots[i].GetItem() == null) {
                _sacrificedSlots[i].AddItem(_sacrificedItems[i]);
            }
        }
    }
    private void ClearSacrificedSlots() {
        for (int i = 0; i < _sacrificedSlots.Length; i++) {
            _sacrificedSlots[i].ClearSlot();
        }
    }

    public void AddBackButton() {
        RemoveFromSacrificedItems();
        ClearSacrificedSlots();
        FillSacrificedSlots();
        ClearFields();

        _addBackButton.SetActive(false);
        _currentSlot = null;
    }

    private void RemoveFromSacrificedItems() {
        Inventory.instance.Add(_currentSlot.GetItem());
        _sacrificedItems.Remove(_currentSlot.GetItem());

        CalculateNeededSoul(-_currentSlot.GetItem().soulValue);
    }


    private void CalculateNeededSoul(int value) {
        _soulValue += value;
        _currentSoulValue.text = _soulValue.ToString();

        if (_soulValue >= GameManager.instance.levelManager.NeededSoulValue) {
            _acceptButtonCanvasGroup.alpha = 1f;
            _acceptButtonCanvasGroup.interactable = true;
        }
        else {
            _acceptButtonCanvasGroup.alpha = .25f;
            _acceptButtonCanvasGroup.interactable = false;
        }
    }


    public void CancelButton() {
        if (GameManager.instance.IsGamePaused()) {
            GameManager.instance.UnPauseGame();
            this.gameObject.SetActive(false);

            if (_sacrificedItems.Count > 0) {
                foreach (Item item in _sacrificedItems) {
                    Inventory.instance.Add(item);
                }

                _sacrificedItems.Clear();
            }

            ResetUI();
        }
    }

    public void AcceptButton() {
        this.gameObject.SetActive(false);
        GameManager.instance.UnPauseGame();
        GameManager.instance.LoadNewScene(_endLevel.NextScene);
    }


    public void SetEndLevel(EndLevel endLevel) {
        this._endLevel = endLevel;
    }

    private void OnDestroy() {
        Inventory.instance.onItemChangedCallBack -= FillInventorySlots;
    }

}
