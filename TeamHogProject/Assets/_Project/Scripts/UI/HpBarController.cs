using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    [SerializeField] private Image _hpBar;

    public void ChangeHp(float hp)
    {
        float newHp = hp / 100;
        _hpBar.fillAmount = newHp;
    }
    
}
