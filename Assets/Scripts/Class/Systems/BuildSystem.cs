using UnityEngine;

public class BuildSystem
{
    private Transform _newBuildTransform;
    private Builder _builder;
    
    public void Initialize(Transform newBuild,Builder builder)
    {
        _builder = builder;
        _newBuildTransform = newBuild;
    }

    public void Rotate()
    {
        _newBuildTransform.Rotate(new Vector3(0,90,0));
    }

    public void Exit()
    {
        _builder.StopBuild();
    }
}