using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TeachView : View
{
    public int Index;
    public List<Image> Targets=new List<Image>();
    public RectGuidanceController controller;

    private void Start()
    {
        controller.UpdateTarget(Targets[Index]);
    }
    public void NextEquipTeach()
    {

        if (!gameObject.activeSelf)
            return;
            Index++;

        if (Index >= Targets.Count)
        {
            PlayerInfoController._instance.pi.Teach["Equip"] = true;
            OnCloseClick();
            return;
        }
        controller.UpdateTarget(Targets[Index]);
    }
}
