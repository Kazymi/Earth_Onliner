using TMPro;
using UnityEngine;

public class ArmyPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameArmy;
    [SerializeField] private TMP_Text amountArmy;

    public void Initialize(string name)
    {
        nameArmy.text = name;
        amountArmy.text = "0";
    }

    public void UpdateState(int amountArmy)
    {
        this.amountArmy.text = amountArmy.ToString();
    }
}
