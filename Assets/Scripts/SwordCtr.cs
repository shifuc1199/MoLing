using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 public enum AttackType
{
    单剑发射,
    群剑发射
}
public class SwordCtr : MonoBehaviour, IAttackable
{
    public float _speed;
    public AttackType type;

    private Animator _anim;

    bool ismove = false;
 
    Transform parent;

   
    public bool isCanSingal=true;
    public bool isCanMutli = true;

    public float _attack;
    public float Attack
    {
        get
        {
            return _attack;
        }
        set
        {
            _attack = value;
        }
    }

    public AttackCallBack _attackcallback
    {
        get;
        set;
    }

  

    List<Tweener> resettweeners = new List<Tweener>();
    List<Tweener> mutliswordtweeners = new List<Tweener>();
    // Start is called before the first frame update

    void Start()
    {
      

     
        
    }


    public void ResetState()
    {
        _anim.SetTrigger("idle");
        Timer.Register(0.1f, () =>
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            transform.parent = parent;
            resettweeners.Add( transform.DOLocalMove(new Vector3(Mathf.Abs( parent.right.x) * 10 ,    10, 0), 0.25f).SetEase(Ease.Linear));
            Timer.Register(0.25f, () => { resettweeners.Add( transform.DOLocalMove(new Vector3(-0.24f,-1f,0), 0.25f).SetEase(Ease.Linear));});
            resettweeners.Add( transform.DOLocalRotate(new Vector3(0,0,360), 0.15f,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1,LoopType.Incremental));
            ismove = false;
         
            Timer.Register(0.6f, () =>
            {
                foreach (var item in resettweeners)
                {
                    item.Kill();
                }
                resettweeners.Clear();
                transform.DOLocalRotate(new Vector3(0, 0, -141.566f), 0f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
                Timer.Register(0.1f, () => { isCanSingal = true; }); });
        });
    }
   public void Skill(AttackType type)
    {
        this.type = type;
       
       

        switch (type)
        {
            case AttackType.单剑发射:
                {
                    isCanSingal = false;
                    transform.DOLocalMove(new Vector3(4.5f, -1.2f, 0), 0.15f).SetEase(Ease.Linear);
                    transform.DOLocalRotate(Vector3.zero, 0.15f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
                    Timer.Register(0.25f, () => { Camera.main.DOShakePosition(0.1f, 0.5f); GetComponent<PolygonCollider2D>().enabled = true; ismove = true; _anim.SetTrigger("shoot"); transform.parent = null; });
                    Timer.Register(0.5f, () => { ResetState(); });
                }
                break;

            case AttackType.群剑发射:
                {
                    isCanMutli = false;
                    Vector3 aim = parent.transform.position+ new Vector3(parent.transform.right.x * 10, 7, 0);
                    Debug.Log(aim);
                    GameObject temp = GameObjectPool.GetInstance().GetGameObject("群剑", parent.position, parent.rotation);
                    
                    mutliswordtweeners.Add( temp.transform.DOMove(aim, 0.3f).SetEase(Ease.Linear));
                    mutliswordtweeners.Add(temp.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental));
                    Timer.Register(0.3f, () => {
                        foreach (var item in mutliswordtweeners)
                        {
                            item.Kill();
                        }
                        mutliswordtweeners.Clear();
                        temp.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.1f, RotateMode.FastBeyond360).SetEase(Ease.Linear); });

                    Timer.Register(1.25f, () => { temp.GetComponent<Animator>().SetTrigger("shoot"); temp.GetComponent<Mutli_Sword_Ctr>().ismove = true; });
            

                    List<GameObject> list = new List<GameObject>();
                    Timer.Register(0.35f, () =>
                    {
                        Vector3[] pos = new Vector3[] { new Vector3(1.5f, -1f, 0), new Vector3(-1.5f, -1f, 0) };
                        for (int i = 0; i < 2; i++)
                        {
                         
                            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("群剑", temp.transform.position + pos[i], Quaternion.identity);
                            temp2.transform.right = (new Vector3(aim.x, aim.y-7, aim.z) - temp2.transform.position).normalized;
                            Timer.Register(0.1f + (i + 1) * 0.25f, () => { temp2.GetComponent<Animator>().SetTrigger("shoot"); temp2.GetComponent<Mutli_Sword_Ctr>().ismove = true; });
                         
                            list.Add(temp2);
                        
                        }

                    });
                    list.Add(temp);
                     
                    Timer.Register(3f, () =>
                    {
                        foreach (var item in list)
                        {
                            GameObjectPool.GetInstance().ReleaseGameObject("群剑", item, 0);
                            item.GetComponent<Mutli_Sword_Ctr>().ismove = false;
                        }
                        
                        isCanMutli = true; });
                }
                break;
            default:
                break;
        }
    }
    private void OnEnable()
    {
        this._attackcallback = (t) => { t.GetComponent<Rigidbody2D>().AddForce(t.transform.right * 50, ForceMode2D.Impulse); };

       

          parent = transform.parent;
        _anim = GetComponent<Animator>();


    }
    
    // Update is called once per frame
    void Update()
    {
           if (ismove)
            transform.Translate(transform.right * _speed * Time.deltaTime, Space.World);

    }
}
