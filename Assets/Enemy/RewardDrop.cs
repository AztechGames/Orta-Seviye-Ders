using UnityEngine;

public class RewardDrop : MonoBehaviour
{
    public enum Quantity
    {
        Common, // %75
        Rare, // %20
        Legendary // %5
    }
    public Quantity quantity;
    
    public GameObject[] commonDrops;
    public GameObject[] rareDrops;
    public GameObject[] legendaryDrops;

    public void Drop()
    {
        int randomQuantity = Random.Range(0, 100);
        
        if (randomQuantity <= 5) quantity = Quantity.Legendary;
        else if (randomQuantity <= 25) quantity = Quantity.Rare;
        else quantity = Quantity.Common;

        switch (quantity)
        {
            case Quantity.Common: DropCalculate(commonDrops,2);
                break;
            case Quantity.Rare: DropCalculate(rareDrops,3);
                break;
            case Quantity.Legendary: DropCalculate(legendaryDrops,5);
                break;
        }
    }

    void DropCalculate(GameObject[] objectList, int quantityEXP)
    {
        int randomObject = Random.Range(0, objectList.Length);
        Instantiate(objectList[randomObject], transform.position + Vector3.up, Quaternion.identity);
        GetComponent<Enemy>()._player.GetComponent<PlayerUI>().EXP += 5 * quantityEXP;
    }
}
