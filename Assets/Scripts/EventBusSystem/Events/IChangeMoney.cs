using EventBusSystem;

public interface IChangingAmountResources : IGlobalSubscriber
{
    void ChangingAmountResources(TypeResource typeResource);
}