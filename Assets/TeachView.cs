using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TeachView : View
{
    public int Index;
    public List<Image> Targets=new List<Image>();
    public RectGuidanceController controller;
    public GameObject[] TipText;
    private void Start()
    {
        controller.UpdateTarget(Targets[Index]);
    }
    public void NextEquipTeach ()
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
        TipText[Index-1].SetActive(false);
        Timer.Register(0.5f, () => { TipText[Index].SetActive(true); });
       
        controller.UpdateTarget(Targets[Index]);
    }
}
