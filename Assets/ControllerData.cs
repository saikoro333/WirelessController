public class ComonControllerData
{
    public const int MAX_CONT_BUTTON = 16;

    public bool isFinishKeyBind;
    public int keyBindStep;

    public ControllerType Type;
    public int ContNum;

    public int Buttons;
    public AnalogStick Stick1;
    public AnalogStick Stick2;

    public int[] KeyRelate = new int[MAX_CONT_BUTTON];
    public static string[] ButtonString;

    public ComonControllerData(int num)
    {
        this.Type = null;
        this.isFinishKeyBind = false;
        this.keyBindStep = 0;

        this.ContNum = num;
        this.Buttons = 0;

        this.Stick1 = null;
        this.Stick2 = null;

        for(int i = 0; i < MAX_CONT_BUTTON; i++) KeyRelate[i] = -1;
    }


    public void setType(string device, string type)
    {
        this.Type = new ControllerType(device, type);
    }

    public int getButtonIndex(int value)
    {
        for (int i = 0; i < this.Type.ButtonNum; i++)
        {
            if (this.KeyRelate[i] == value) return i;
        }
        return -1;
    }


}

public class AnalogStick
{
    public const int STICK_RANGE_MAX = 32767;
    public const int STICK_RANGE_MIN = 0;

    public uint AxisX, AxisY;
    public string StickName;

    public AnalogStick()
    {
        this.AxisX = 0;
        this.AxisY = 0;
    }
    public AnalogStick(int x,int y,string n)
    {
        this.StickName = n;
        this.setAxis(x,y);
    }

    public void setAxis(int x,int y)
    {
        this.AxisX = this.ConvertAxisToUint(x);
        this.AxisY = this.ConvertAxisToUint(y);
    }

    private uint ConvertAxisToUint(float ax)
    {
        const int adjust = STICK_RANGE_MAX / 2;
        return (uint)((ax + 1.0f) * adjust);
    }
}

public class ControllerType
{
    public string DeviceName;
    public string Platform;

    public int ButtonNum;
    public  string[] ButtonString;

    public ControllerType(string d,string t)
    {
        this.DeviceName = d;
        this.Platform = t;

        this.setPlatform();
    }

    public void setPlatform()
    {
        switch (this.Platform)
        {
            case WiiController.PLATFORM:
                this.ButtonNum = WiiController.MAX_BUTTON_NUM;
                this.ButtonString = WiiController.BUTTON_STRING;
                break;

            case GCController.PLATFORM:
                this.ButtonNum = GCController.MAX_BUTTON_NUM;
                this.ButtonString = GCController.BUTTON_STRING;
                break;

        }
    }

}