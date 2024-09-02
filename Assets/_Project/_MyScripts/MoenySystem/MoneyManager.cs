using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyManager : Singelton<MoneyManager>
{
    [SerializeField] private int _money = 0;
    [SerializeField] private TextMeshProUGUI _moneyText;
    public void AddMoney(int x)
    {
        var newAmount = _money + x;
        if (_moneyText != null)
        {
            DOVirtual.Int(_money, newAmount, 1f, (int i) => _moneyText.text = i.ToString()).SetEase(Ease.Linear);
            _moneyText.DOColor(Color.green, 1f).SetEase(Ease.Linear).OnComplete(() => _moneyText.DOColor(Color.white, 1f).SetEase(Ease.Linear));
            _moneyText.transform.DOScale(1.2f, 1f).SetEase(Ease.InOutBack).OnComplete(() => _moneyText.transform.DOScale(1f, 1f).SetEase(Ease.InOutBack));
        }

        _money = newAmount;
    }
    public int GetMoney()
    {
        return _money;
    }
    public void SetTMP(TextMeshProUGUI text)
    {
        _moneyText = text;
    }


}
