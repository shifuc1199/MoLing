using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingCtr : MonoBehaviour
{
    public static string LevelName;
    AsyncOperation operation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
      
        operation = SceneManager.LoadSceneAsync(LevelName);
        operation.allowSceneActivation = false;
        yield return operation;
       
        //    operation.allowSceneActivation = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(operation.progress>=0.9f)
        {
            operation.allowSceneActivation = true;
        }
       
    }
}
