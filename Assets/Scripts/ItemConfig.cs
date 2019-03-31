using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ItemConfig")]
public class ItemConfig :ScriptableObject
{
    public List<Item> items = new List<Item>();
}
