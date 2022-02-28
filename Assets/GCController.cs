
public class GCController
{
    public const string PLATFORM = "GC";
    public const int MAX_BUTTON_NUM = 12;
    public static readonly string[] BUTTON_STRING = { "A", "B", "X", "Y", "Z", "START", "L", "R", "UP", "DOWN", "LEFT", "RIGHT" };
    //stick [-1.0f - 1.0f] => [0 - 32767]
    public enum ButtonBit
    {
        A = 0x01,
        B = 0x02,
        X = 0x04,
        Y = 0x08,
        Z = 0x10,
        START = 0x20,
        L = 0x40,
        R = 0x80,
        UP = 0x0100,
        DOWN = 0x0200,
        LEFT = 0x0400,
        RIGHT = 0x0800
    }

}
