using UnityEngine;

public class Factory
{
    private GameObject _spawnElement;
    private int _countElement;
    private Pool _pool { get; set; }

    public Factory(GameObject spawnElement,int countElement,Transform parentPosition)
    {
        _spawnElement = spawnElement;
        _countElement = countElement;
        _pool = new Pool(_spawnElement, _countElement,parentPosition);
    }

    public GameObject Create()
    {
        return _pool.Pull();
    }

    public void Destroy(GameObject gameObject)
    {
        _pool.Push(gameObject);
    }
}