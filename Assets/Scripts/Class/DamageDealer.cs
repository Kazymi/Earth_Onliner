using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageDealer
{
    bool isMine { get; set; }
    
    void TakeDamage();
    void Death();
}
