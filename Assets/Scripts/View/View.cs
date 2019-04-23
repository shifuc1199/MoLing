using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
  public virtual void OnCloseClick()
    {
        gameObject.SetActive(false);
    }
    public virtual void OnOpenClick()
    {
        gameObject.SetActive(true);
    }
}
