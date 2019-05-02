using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerCtr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SceneJump(string name)
    {
        SceneManager.LoadScene(name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
