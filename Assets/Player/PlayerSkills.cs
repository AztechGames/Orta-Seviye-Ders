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
            if(_ınput > 0 && _ınput < 6)
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
            case Skills.Shield: BasedMethod((int)Skills.Shield,50,shield,Vector3.zero);
                break;
            case Skills.Rocks: BasedMethod((int)Skills.Rocks,35,earth,Vector3.down);
                break;
            case Skills.Halo: BasedMethod((int)Skills.Halo,45,hole,new Vector3(0,1.5f,0));
                break;
        }
        _playerMovement._anim.SetBool("Attack",true);
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

    void BasedMethod(int value,int energy, GameObject skillObject,Vector3 skillPosition)
    {
        if (_skillbools[value] && _playerUI.Energy >= energy)
        {
            UIManager.Instance.Skills[value].enabled = true;
            _playerUI.Energy -= energy;
            var s_Object = Instantiate(skillObject, transform.position + skillPosition, transform.GetChild(0).rotation);
            if (skillObject == shield)
            {
                s_Object.transform.parent = transform;
                _playerUI.vulnerable = false;
            }
            else _playerUI.vulnerable = true;
            
            StartCoroutine(Timer(value));
        }
    }
    IEnumerator Timer(int skillnum)
    {
        _skillbools[skillnum] = false;
        yield return new WaitForSeconds(skillcooldown);
        UIManager.Instance.Skills[skillnum].enabled = false;
        _playerMovement._anim.SetBool("Attack",false);
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
