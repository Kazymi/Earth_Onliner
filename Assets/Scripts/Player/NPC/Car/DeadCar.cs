
    using System.Collections;
    using UnityEngine;

    public class DeadCar : MonoBehaviour,IFactoryInitialize
    {
        [SerializeField] private float timeDestroy;
        public Factory ParentFactory { get; set; }
        public void Initialize()
        {
            StartCoroutine(Destroy());
        }

        IEnumerator Destroy()
        {
            yield return new WaitForSeconds(timeDestroy);
            ParentFactory.Destroy(gameObject);
        }
    }
