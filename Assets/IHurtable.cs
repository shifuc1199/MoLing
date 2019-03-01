using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void DieCallBack();
public delegate void HurtCallBack();
public interface IHurtable
{
     
     float Health { get; set; }
     float MaxHealth { get; set; }
     HurtCallBack _HurtCallBack { get; set; }
    DieCallBack _DieCallBack { get; set; }
    void GetHurt(float attack);
}
