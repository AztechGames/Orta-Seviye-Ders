using System;
using System.Collections;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    private int _ınput;
    private float _startspeed;
    
    public float skillcooldown;
    
    public GameObject fireBall;
    public GameObject shield;
    public GameObject hole;
    public GameObject earth;
    
    PlayerMovement _playerMovement;
    PlayerUI _playerUI;
    
    bool[] _skillbools = new bool[5];
    
    public enum Skills
    {
        SuperSpeed,
        Stamina,
        Shield,
        Rocks,
        Halo,
    }
    public Skills Skill;

    private void Start()
    {
        _playerUI = GetComponent<PlayerUI>();
        _playerMovement = GetComponent<PlayerMovement>();
        _startspeed = _playerMovement.speed;
        for (int i = 0; i < _skillbools.Length; i++)
        {
            _skillbools[i] = true;
        }
    }

    private void Update()
    {
        if (Input.inputString != "")
        {
            int.TryParse(Input.inputString, out _ınput);
            if(_ınput > 0 && _ınput < 8)
            {
                Skill = (Skills)_ınput - 1;
            }
        }
        if(Input.GetMouseButtonDown(1))
            SkillSet();
        else if(Input.GetMouseButtonDown(0))
            Fireball();
        Countdown();
    }

    void SkillSet()
    {
        switch (Skill)
        {
            case Skills.SuperSpeed: SuperSpeed((int)Skills.SuperSpeed);
                break;
            case Skills.Stamina: Stamina((int)Skills.Stamina);
                break;
            case Skills.Shield: Shield((int)Skills.Shield);
                break;
            case Skills.Rocks: Rocks((int)Skills.Rocks);
                break;
            case Skills.Halo: Halo((int)Skills.Halo);
                break;
        }
    }
    void SuperSpeed(int value)
    {
        if (_skillbools[value] && _playerUI.Energy >= 30)
        {
            UIManager.Instance.Skills[value].enabled = true;
            _playerUI.Energy -= 30;
            _startspeed = _playerMovement.speed;
            Time.timeScale = .5f;
            _playerMovement.speed = _startspeed * 2;
            StartCoroutine(Timer(value));
        }
    }
    void Fireball()
    {
        if (_playerUI.Energy >= 15)
        {
            _playerUI.Energy -= 15;
            Instantiate(fireBall,transform.position,transform.GetChild(0).rotation);
        }
    }
    void Stamina(int value)
    {
        if (_skillbools[value])
        {
            UIManager.Instance.Skills[value].enabled = true;
            _playerUI.Energy += 20;
            _playerUI.Health += 5;
            StartCoroutine(Timer(value));
        }
    }
    void Shield(int value)
    {
        if (_skillbools[value] && _playerUI.Energy >= 50)
        {
            UIManager.Instance.Skills[value].enabled = true;
            _playerUI.Energy -= 50;
            _playerUI.vulnerable = false;
            var _shield = Instantiate(shield,transform.position,transform.rotation);
            _shield.transform.parent = transform;
            StartCoroutine(Timer(value));
        }
    }
    void Rocks(int value)
    {
        if (_skillbools[value] && _playerUI.Energy >= 25)
        {
            UIManager.Instance.Skills[value].enabled = true;
            _playerUI.Energy -= 25;
            Instantiate(earth,transform.position + Vector3.down,transform.GetChild(0).rotation);
            StartCoroutine(Timer(value));
        }
    }
    void Halo(int value)
    {
        if (_skillbools[value] && _playerUI.Energy >= 40)
        {
            UIManager.Instance.Skills[value].enabled = true;
            _playerUI.Energy -= 40;
            Instantiate(hole,transform.position + new Vector3(0,1.5f,0),transform.GetChild(0).rotation);
            StartCoroutine(Timer(value));
        }
    }
    IEnumerator Timer(int skillnum)
    {
        _skillbools[skillnum] = false;
        yield return new WaitForSeconds(skillcooldown);
        UIManager.Instance.Skills[skillnum].enabled = false;
        _skillbools[skillnum] = true;
    }
    public void Countdown()
    {
        for (int i = 0; i < _skillbools.Length; i++)
        {
            if (UIManager.Instance.Skills[i].enabled)
            {
                UIManager.Instance.Skills[i].fillAmount -= Time.deltaTime / skillcooldown;
            }
            else UIManager.Instance.Skills[i].fillAmount = 1;
        }

        if (_skillbools[(int)Skills.SuperSpeed])
        {
            Time.timeScale = 1;
            _playerMovement.speed = _startspeed;
        }
        if(_skillbools[(int)Skills.Shield])
        {
            _playerUI.vulnerable = true;
        }
    }
}
