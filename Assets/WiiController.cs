
public class WiiController
{
    public const string PLATFORM = "Wii";
    public const int MAX_BUTTON_NUM = 12;
    public static readonly string[] BUTTON_STRING = { "A", "B", "1", "2", "+", "-", "Home", "Shake", "UP", "DOWN", "LEFT", "RIGHT" };
    //stick [-1.0f - 1.0f] => [0 - 32767]

    public enum ButtonBit
    {
        A = 0x01,
        B = 0x02,
        ONE = 0x04,
        TWO = 0x08,
        PLUS = 0x10,
        MINUS = 0x20,
        HOME = 0x40,
        SHAKE = 0x80,
        UP = 0x0100,
        DOWN = 0x0200,
        LEFT = 0x0400,
        RIGHT = 0x0800
    }



}
