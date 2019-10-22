using System;
using UnityEngine;

public class Coin : MonoBehaviour{
    public enum Types {
        Genna,
        Kanei,
        Houei,
        Tenpou
    }

    public Types type;

    public int Value {
        get {
            switch(type) {
                case Types.Genna:
                    return 1;
                case Types.Kanei:
                    return 4;
                case Types.Houei:
                    return 10;
                case Types.Tenpou:
                    return 100;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}