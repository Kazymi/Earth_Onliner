using System;
using UnityEngine;

[Serializable]
public class ResourceGenerate
{
    [SerializeField] private Resource generatingResource;
    [SerializeField] private float generationTime;

    public Resource GenerationResource => generatingResource;

    public float GenerateTimer
    {
        set => generationTime = value;
        get => generationTime;
    }
}