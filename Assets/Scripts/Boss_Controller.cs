using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : EnemyBase
{
    public Transform[] dashpos;
    public float dash_spped;
    public Transform player;
    public Animator _anim;

    // Start is called before the first frame update
   new void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        _machine.RegisterState(new Boss_AttackState("attack", this));
        _machine.RegisterState(new Boss_AirAttackState("airattack", this));
        ReleaseSkill();
    }
    public void HideCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;

    }
    public void ShowCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;

    }
    int lastindex=-1;
    public void ReleaseSkill()
    {
     
        string[] skillname = new string[] { "attack" ,"airattack"};
        int index = UnityEngine.Random.Range(0, skillname.Length);
        while (index == lastindex)
        {
            index = UnityEngine.Random.Range(0, skillname.Length);
        }
        switch (skillname[index])
        {
            case "shoot":
                Timer.Register(4
                    , () => { ReleaseSkill(); });
                break;
            case "airattack":
                Timer.Register(5f, () => { ReleaseSkill(); });
                break;
            case "attack":
                Timer.Register(4f, () => { ReleaseSkill(); });
                break;
            default:
                break;
        }
        lastindex = index;
        Debug.Log("转状态:" + skillname[index]);
        _machine.ChangeState(skillname[index]);
    }
    public int GetDashPointIndex()
    {
        float maxDistance = float.MinValue;
        int Index = -1;
        for (int i = 0; i < dashpos.Length; i++)
        {
            if (Vector3.Distance(player.transform.position, dashpos[i].position) >= maxDistance)
            {
                maxDistance = Vector3.Distance(player.transform.position, dashpos[i].position);
                Index = i;
            }
        }
        return Index;
    }
}
