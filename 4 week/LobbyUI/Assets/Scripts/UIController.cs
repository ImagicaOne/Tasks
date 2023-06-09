using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Button _left;

    [SerializeField]
    private Button _right;

    [SerializeField]
    private Button _select;

    [SerializeField]
    private GameObject _lobbyCanvas;

    [SerializeField]
    private GameObject _choosingHeroCanvas;

    [SerializeField]
    private GameObject _priceButton;

    [SerializeField]
    private GameObject _selectButton;

    [SerializeField]
    private GameObject _money;

    [SerializeField]
    private HeroController _heroController;

    private List<HeroDependElement> _heroDependElements = new List<HeroDependElement>();

    public void Start()
    {
        
        _heroDependElements.AddRange(_lobbyCanvas.GetComponentsInChildren<HeroDependElement>(false));
        

        _heroController.ShowFirst();
        ControlBuyButtonVisibility();
        LoadHeroData();
    }

    public void PressHeroes()
    {
        SwitchScreen(false, true);
    }

    public void GoToMainScreen()
    {
        SwitchScreen(true, false);

        _heroController.ShowSelected();
        LoadHeroData();
    }

    public void PressLeft()
    {
        _heroController.ShowPrevious();
        ControlBuyButtonVisibility();
        LoadHeroData();
    }

    public void PressRight()
    {
        _heroController.ShowNext();
        ControlBuyButtonVisibility();
        LoadHeroData();
    }

    public void ControlBuyButtonVisibility()
    {
        if (_heroController.IsBought())
        {
            _priceButton.gameObject.SetActive(false);
            _selectButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            _priceButton.gameObject.SetActive(true);
            _selectButton.GetComponent<Button>().interactable = false;

        }
    }

    public void SelectHero()
    {
        _heroController.SelectHero();
        SwitchScreen(true, false);
    }

    public void BuyHero()
    {
        _heroController.SetBought();
        _priceButton.gameObject.SetActive(false);
        var price = _priceButton.GetComponentInChildren<TextMeshProUGUI>();
        var money = _money.GetComponent<TextMeshProUGUI>();
        money.text = (Convert.ToDecimal(money.text.Replace(",", string.Empty)) - Convert.ToInt32(price.text)).ToString("#,#");
        _selectButton.GetComponent<Button>().interactable = true;
    }

    private void LoadHeroData()
    {
        foreach (var element in _heroDependElements)
        {
            element.Change(_heroController.GetCurrentHeroStat(element.statToChange));
        }
    }

    private void SwitchScreen(bool first, bool second)
    {
         _lobbyCanvas.SetActive(first);
         _choosingHeroCanvas.SetActive(second);

    }
}
