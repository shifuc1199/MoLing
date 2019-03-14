using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingView : View
{
    public Behaviour[] behaviours;
    public GameObject[] LowBackGround;
    public GameObject[] MediumBackGround;
    public GameObject[] Effects;
    public GameObject Lights;
    public Material _mat;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void On_BackGround_ValueChange(int index)
    {
        if (index == 0)
        {
            foreach (var item in LowBackGround)
            {
                item.SetActive(true);
            }
        }
        else if (index == 1)
        {
            foreach (var item in LowBackGround)
            {
                item.SetActive(true);
            }
            foreach (var item in MediumBackGround)
            {
                item.SetActive(false);
            }
        }
        else
        {
            foreach (var item in LowBackGround)
            {
                item.SetActive(false);
            }
        }
    }
    public void On_CameraPost_ValueChange(int index)
    {
        if (index == 0)
        {
            foreach (var item in behaviours)
            {
                item.enabled = true;
            }
        }
        else
        {
            foreach (var item in behaviours)
            {
                item.enabled = false;
            }
        }
    }
    public void On_Effect_ValueChange(int index)
    {
        if (index == 0)
        {
            foreach (var item in Effects)
            {
                item.SetActive(true);
            }
        }
        else
        {
            foreach (var item in Effects)
            {
                item.SetActive(false);
            }
        }
    }
    public void On_Light_ValueChange(int index)
    {
        if (index == 0)
        {
            _mat.shader = Shader.Find("Sprites/Diffuse");

            Lights.SetActive(true);

        }
        else
        {
            _mat.shader = Shader.Find("Sprites/Default");

            Lights.SetActive(false);

        }
    }
}
