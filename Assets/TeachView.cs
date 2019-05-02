using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TeachType
{
    BuyEquipment,
    GetSitDown
}
 

public class TeachView : View
{
    public TeachType type;
    public int Index;
    public RectGuidanceController controller;
    public List<Image> EquipmentTargets=new List<Image>();
    public List<Image> SitDownTargets = new List<Image>();
    public GameObject[] TipTextRoots;
    public GameObject[] EquipmentTipText;
    public GameObject[] SitDownTipText;
    List<Image> Targets;
    GameObject[] TipText;
    private void Start()
    {
        

    }
    private void OnEnable()
    {
        switch (type)
        {
            case TeachType.BuyEquipment:
                TipTextRoots[0].SetActive(true); TipTextRoots[1].SetActive(false);
                Targets = EquipmentTargets;
                TipText = EquipmentTipText;
                break;
            case TeachType.GetSitDown:
                TipTextRoots[1].SetActive(true); TipTextRoots[0].SetActive(false);
                Targets = SitDownTargets;
                TipText = SitDownTipText;
                break;
            default:
                break;
        }

        controller.UpdateTarget(Targets[Index]);
    }
    private void Update()
    {
        if(type==TeachType.GetSitDown)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
             NextEquipTeach();
        }
    }
    public void NextEquipTeach ()
    {
      



        if (!gameObject.activeSelf)
            return;
            Index++;

        if (Index >= Targets.Count)
        {
            switch (type)
            {
                case TeachType.BuyEquipment:
                    PlayerInfoController._instance.pi.Teach["Equip"] = true;
                    break;
                case TeachType.GetSitDown:
                  
                    break;
                default:
                    break;
            }
          
            OnCloseClick();
            Index = 0;
            return;
        }
        TipText[Index-1].SetActive(false);
        Timer.Register(0.5f, () => { TipText[Index].SetActive(true); });
       
        controller.UpdateTarget(Targets[Index]);
    }
}
