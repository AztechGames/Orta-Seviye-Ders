using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image HealthBar, EnergyBar, ExperianceBar;
    public TextMeshProUGUI LevelText;
    public float EnergyFillAmount;
    
    [HideInInspector] public bool vulnerable = true;
    
    private float _health;
    private float _energy;
    private float _exp;
    private int _level = 1;
    public float Health
    {
        get => _health;
        set
        {
            if (vulnerable)
            {
                _health = value;
                _health = Mathf.Clamp(_health, 0, 100);
                HealthBar.fillAmount = _health / 100;
            }
            if(_health <= 0) Die();
        }
    }
    public float Energy
    {
        get => _energy;
        
        set
        {
            _energy = value;
            _energy = Mathf.Clamp(_energy, 0, 100);
            EnergyBar.fillAmount = _energy / 100;
        }
    }

    public float EXP
    {
        get => _exp;
        set
        {
            if (_level < 5)
            {
                _exp = value;
                _exp = Mathf.Clamp(_exp, 0, 100);
                ExperianceBar.fillAmount = _exp / 100;
                if (_exp >= 100) LevelUp();
            }
        }
    }

    void LevelUp()
    {
        ExperianceBar.fillAmount = 0;
        EXP = 0;
        _level++;
        LevelText.text = _level.ToString();
    }
    private void Start()
    {
        _level = 1;
        Health = 100;
        Energy = 100;
    }

    void Update()
    {
        if(Energy < 100) Energy += EnergyFillAmount * Time.deltaTime;
    }

    void Die()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Die");
    }
}
