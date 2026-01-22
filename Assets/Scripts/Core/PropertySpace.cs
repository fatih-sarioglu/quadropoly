using UnityEngine;

[CreateAssetMenu(fileName = "NewProperty", menuName = "Quadropoly/Property")]
public class PropertySpace : BoardSpace
{
    public ColorGroup colorGroup;
    public int price;
    public int baseRent;
    public int baseMent;
    public int houseRent1;
    public int houseRent2;
    public int houseRent3;
    public int houseRent4;
    public int hotelRent;
    public int houseCost;
    public int hotelCost;

    public enum ColorGroup
    {
        Brown, LightBlue, Pink, Orange, Red, Yellow, Green, DarkBlue
    }
}