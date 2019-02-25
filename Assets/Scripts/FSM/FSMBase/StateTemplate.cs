using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTemplate<T> :FSMState  {
    public T Owner;
    public StateTemplate(string id, T o) : base(id)
    {
        Owner = o;
    }
}
