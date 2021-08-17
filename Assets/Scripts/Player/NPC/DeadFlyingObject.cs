using System;
using System.Collections;
using UnityEngine;

public class DeadFlyingObject : MonoBehaviour,IFactoryInitialize
{
    [SerializeField] private AnimationCurve rotateCurve;
    [SerializeField] private float destroyTimer;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;

    private bool isGround;
    private int _fallAnimation;
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;

    private float _currentTimeCurve;
    private float _totalTimeCurve;
    
    public Factory ParentFactory { get; set; }
    private void Update()
    {
        if (isGround)
        {
            return;
        }
        transform.Rotate(0,rotateCurve.Evaluate(_currentTimeCurve),0);
        transform.position -= transform.forward * speed * Time.deltaTime;

        _currentTimeCurve += Time.deltaTime;
        if (_currentTimeCurve >= _totalTimeCurve)
        {
            _currentTimeCurve = 0;
        }
    }
   
    public void Initialize()
    {
        _totalTimeCurve = rotateCurve.keys[rotateCurve.keys.Length - 1].time;
        _fallAnimation = Animator.StringToHash("Fall");
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        
        _boxCollider.enabled = true;
        _rigidbody.isKinematic = false;
        explosionEffect.SetActive(false);
        animator.SetBool(_fallAnimation,true);
        isGround = false;
        StartCoroutine(Destroy());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Earth>() == false)
        {
            return;
        }
        isGround = true;
        animator.SetBool(_fallAnimation,false);
        _boxCollider.enabled = false;
        _rigidbody.isKinematic = true;
        explosionEffect.SetActive(true);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyTimer);
        ParentFactory.Destroy(gameObject);
    }
}