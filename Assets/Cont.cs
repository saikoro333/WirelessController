using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Analog [0,32767]

public class Cont : MonoBehaviour
{

    const int ButtonMax = 16;
    //GCController cont;
    public GameUI_Button BL;
    public TMP_Text label;
    /*
    bool isFinishKeyBind = false;
    int keyBindStep = 0;
    */
    ComonControllerData mainCont;
    int data = 0;

    void Start()
    {
        //cont = new GCController();

        InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

        for (int bcnt = 0; bcnt < ButtonMax; bcnt++)
        {
            var name = string.Format("test button {0}", bcnt);
            var button = string.Format("joystick button {0}",bcnt);
            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, button, ""));
        }

        mainCont = new ComonControllerData(0);
        label.text = "push 1:Wii 2:GC";

    }

    // Update is called once per frame
    void Update()
    {
        if (mainCont.Type == null)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                mainCont.setType("def", "Wii");
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                mainCont.setType("def", "GC");
            }
            return;
        }
        /*
        if (isFinishKeyBind == false)
        {
            this.KeyBind();
        }
        else
        {
            this.getInput();
        }
        */
        if (mainCont.isFinishKeyBind == false)
        {
            this.KeyBind_NEW();
        }
        else
        {
            this.getInput();
            this.sendContInput();
        }

    }

    /*
    uint ConvertAxisToUint(float ax)
    {
        const int adjust = 32767 / 2;
        return (uint)( (ax+1.0f) * adjust);
    }
    */


    public int getFirstButtonNum()
    {
        for (int bcnt = 0; bcnt < ButtonMax; bcnt++)
        {
            var name = string.Format("test button {0}", bcnt);
            if (Input.GetButton(name))
            {
                return bcnt;
            }
        }
        return -1;
    }
    /*

    public int getGCIndex(int value)
    {
        for (int i = 0; i < ButtonMax; i++)
        {
            if(RelateButtonNum[i] == value) return i;
        }
        return -1;
    }

    public int converGCInput()
    {
        //////
        int res = 0;
        for (int bcnt = 0; bcnt < ButtonMax; bcnt++)
        {
            var name = string.Format("test button {0}", bcnt);
            if (Input.GetButton(name))
            {
                res |= (1 << getGCIndex(bcnt));
            }
        }
        return res;
    }
    */

    private int Convert()
    {
        int res = 0;
        for (int bcnt = 0; bcnt <ButtonMax; bcnt++)
        {
            var name = string.Format("test button {0}", bcnt);
            if (Input.GetButton(name))
            {
                res |= (1 << mainCont.getButtonIndex(bcnt));
            }
        }
        this.data = res;
        return res;
    }

    public void getInput()
    {
        label.text = mainCont.Type.Platform + " Input : 0x" + Convert().ToString("X");
        for (int bcnt = 0; bcnt < ButtonMax; bcnt++)
        {
            var name = string.Format("test button {0}", bcnt);
            if (Input.GetButton(name))
            {

                var kuso = BL.ButtonList[bcnt].colors;
                kuso.normalColor = Color.red;
                BL.ButtonList[bcnt].colors = kuso;

            }
            else
            {
                var kuso = BL.ButtonList[bcnt].colors;
                kuso.normalColor = Color.white;
                BL.ButtonList[bcnt].colors = kuso;
            }
        }

        /*
        if (Input.GetButton("A"))
        {
            cont.buttons |= (int)GCController.ButtonBit.A;
            //Debug.Log("A");
        }        
        if (Input.GetButton("B"))
        {
            cont.buttons |= (int)GCController.ButtonBit.B;
        }
        if (Input.GetButton("X"))
        {
            cont.buttons |= (int)GCController.ButtonBit.X;
        }
        if (Input.GetButton("Y"))
        {
            cont.buttons |= (int)GCController.ButtonBit.Y;
        }
        if (Input.GetButton("Start"))
        {
            cont.buttons |= (int)GCController.ButtonBit.START;
        }
        if (Input.GetButton("Z"))
        {
            cont.buttons |= (int)GCController.ButtonBit.Z;
        }
        if (Input.GetButton("L"))
        {
            cont.buttons |= (int)GCController.ButtonBit.L;
        }
        if (Input.GetButton("R"))
        {
            cont.buttons |= (int)GCController.ButtonBit.R;
        }
        */
        /*
        cont.analogStickX = ConvertAxisToUint(Input.GetAxis("Analog_X"));
        cont.analogStickY = ConvertAxisToUint(Input.GetAxis("Analog_Y"));
        cont.CStickX = ConvertAxisToUint(Input.GetAxis("CStick_X"));
        cont.CStickY = ConvertAxisToUint(Input.GetAxis("CStick_Y"));
        */
        //Debug.Log(cont.CStickX + " : " + Input.GetAxis("CStick_X"));
        //Debug.Log(Input.GetAxis("CStick_X"));
     
    }
    /*
    int[] RelateButtonNum = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

    public void KeyBind()
    {
        //label.text = "Chose " + Rlist[i] + " Button !";
        if(keyBindStep == GCController.GC_BUTTON_NUM)
        {
            this.isFinishKeyBind = true;
            label.text = "KeyBind Done ... !";
        }
        else
        {
            label.text = "Chose " + GCController.GC_BUTTON_STRING[this.keyBindStep] + " Button !";
            int Bnum = this.getFirstButtonNum();
            if (Bnum >= 0)
            {
                for(int i = 0; i < RelateButtonNum.Length; i++)
                {
                    if (Bnum == RelateButtonNum[i]) return;
                }
                RelateButtonNum[this.keyBindStep] = Bnum;
                this.keyBindStep++;
                //　ここでバインド
            }

        }
        
    }
    */

    public void KeyBind_NEW()
    {
        //label.text = "Chose " + Rlist[i] + " Button !";
        if (mainCont.keyBindStep == mainCont.Type.ButtonNum)
        {
            mainCont.isFinishKeyBind = true;
            this.label.text = "KeyBind Done ... !";
        }
        else
        {
            label.text = "Chose " + mainCont.Type.ButtonString[mainCont.keyBindStep] + " Button !";
            int Bnum = this.getFirstButtonNum();
            if (Bnum >= 0)
            {
                for (int i = 0; i < mainCont.Type.ButtonNum; i++)
                {
                    if (Bnum == mainCont.KeyRelate[i]) return;
                }
                mainCont.KeyRelate[mainCont.keyBindStep] = Bnum;
                mainCont.keyBindStep++;
                //　ここでバインド
            }

        }

    }

    public bool sendContInput()
    {
        //　ここでデータを送ればいい
        // this.data(ボタン情報)をおくる
        return false; //失敗
    }

}
