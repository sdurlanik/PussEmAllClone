using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float _forcePower;
    
    private Rigidbody _rb;
    private float _deathHight = -1;

    #endregion

    #region Unity Funcs

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Düşmana çarparsa düşmanın forwardına göre playera güç uygulanır (Düşman önden çarparsa karakter geri gider
            // arkadan çarparsa karakter ileri gider)
            _rb.AddForce(collision.transform.forward * _forcePower);
        }
    }

    #endregion

}
