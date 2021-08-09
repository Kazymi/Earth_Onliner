using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer
{
    bool IsMine { get; set; }
    
    void TakeDamage(float damage);
    void Death();
}
