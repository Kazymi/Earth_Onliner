using EventBusSystem;
using UnityEngine;

public interface IChangingAmountResources : IGlobalSubscriber
{
    void ChangingAmountResources(TypeResource typeResource);
}
