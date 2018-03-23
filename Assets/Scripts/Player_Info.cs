using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Info : MonoBehaviour {

    public Image[] img;
    public Chasing NPC;
    public Text objText;
    private float p_HP = 1; //HP 현재수치
    private float MAX_HP = 1;// HP 최대치

    public void Start()
    {
        MAX_HP = NPC.MAXTime;
        objText.text = "거리";
    }

    public void UIUpdate(string _Type,string _InfoType,float _Value)// 게이지의 증가,감소상태/체력바/줄어드는양
    {
       
        int index = 0; //이미지 인덱스
       
        //MAX_HP = NPC.MAXTime;
        switch (_InfoType)
        {
            case "HP":
                {
                    index = 0;
                   
                   if (_Type == "Diminution")
                        p_HP = _Value;
                   if (_Type== "Increase")
                        p_HP = _Value;
                    break;
                }
        }

        img[index].fillAmount = p_HP/MAX_HP;//Type/MAXType;

       
    }

    public void TextUpdate(float Distance)
    {
        int DistanceInt = (int)Distance;
        objText.text = DistanceInt.ToString()+"m" ;
    }


}
