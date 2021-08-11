using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool IsMine { get; set; }
    
    void TakeDamage(float damage);
    void Death();

    void Initialize();
}
