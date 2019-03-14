using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AttackCallBack(GameObject attack_object);
public interface IAttackable
{
   AttackCallBack _attackcallback { get; set; }
  float Attack { get; set; }
}
