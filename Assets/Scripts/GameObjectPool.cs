using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool 
{
    private static GameObjectPool _pool;
    public static GameObjectPool GetInstance()
    {
        if(_pool==null)
        {
            _pool = new GameObjectPool();
            
        }
        return _pool;
    }
    private GameObjectPool()
    {
        Init(Resources.Load<Effect>("EffectConfig").EffectNames);
    }
    private void Init(List<string> name)
    {
        foreach (var item in name)
        {
            gameobject_pooldic.Add(item, new ObjectPool<GameObject>());
        } 
    
    }
    private Dictionary<string, ObjectPool<GameObject>> gameobject_pooldic = new Dictionary<string, ObjectPool<GameObject>>();
    public  GameObject GetGameObject(string name, Transform parent=null)
    {
        ObjectPool<GameObject> pool = gameobject_pooldic[name];
        GameObject temp = pool.Get();
 
        if(temp==null)
        {
            temp = Resources.Load<GameObject>(name);
            temp =UnityEngine.GameObject.Instantiate(temp, parent);
        }
        else
        {
            temp.transform.parent = parent;
            temp.SetActive(true);
        }
     
        return temp;
    }
    public GameObject GetGameObject(string name,Vector3 positon,Quaternion qua, Transform parent = null)
    {
        ObjectPool<GameObject> pool = gameobject_pooldic[name];
        GameObject temp = pool.Get();
    
        if (temp == null)
        {
            temp = Resources.Load<GameObject>(name);
            temp = UnityEngine.GameObject.Instantiate(temp, parent);
        }
        else
        {
            temp.SetActive(true);
          
            temp.transform.rotation = qua;
            temp.transform.parent = parent;
        }
        temp.transform.position = positon;
        return temp;
    }
    public void ReleaseGameObject(string name,GameObject temp,float timer)
    {
        Timer.Register(timer, () =>
        {
            ObjectPool<GameObject> pool = gameobject_pooldic[name];
            temp.SetActive(false);
            pool.Add(temp);
        });
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
