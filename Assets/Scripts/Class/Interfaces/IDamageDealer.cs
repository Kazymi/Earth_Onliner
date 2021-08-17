using UnityEngine;

public interface IDamageable
{
    bool IsMine { get; set; }
    Transform TargetTransform { get; set; }
    void TakeDamage(float damage);
    void Death();
    void Initialize();
}
