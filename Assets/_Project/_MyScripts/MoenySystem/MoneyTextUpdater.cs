using TMPro;
using UnityEngine;

public class MoneyTextUpdater : MonoBehaviour
{
    TextMeshProUGUI _tmp;
    private void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        MoneyManager.Instance.SetTMP(_tmp);
        MoneyManager.Instance.AddMoney(0);
    }
}
