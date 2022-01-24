using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   #region Variables

   [SerializeField] private GameObject _player;
   [SerializeField] private float _attackRange;
   [SerializeField] private float _movementSpeed;
   [SerializeField] private float _forcePower;
   
   private float _distance;
   private float _deathHight = -1;

   private Vector3 _tempDir;
   private Rigidbody _rb;

   #endregion

   #region Unity Funcs

   private void Start()
   {
      _rb = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      EnemyMove();
      EnemyDeath();

   }

   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Pole"))
      {
         // IsOpen değeri Pole nesnesinin açık olup olmadığını döndürür
         if (collision.gameObject.GetComponent<PoleController>().IsOpen)
         {
            // Pole nesnesinin forwardına göre düşman nesnesine güç uygular
            _rb.AddForce(collision.transform.forward * _forcePower);

         }
      }
   }

   #endregion
   
   #region Custom Funcs

   void EnemyMove()
   {
      // Düşman ile karakter arasındaki mesafeyi bulur
      _distance = Vector3.Distance(transform.position, _player.transform.position);
      
      //Aradaki mesafe minimum atak mesafesinden küçük ise ife girer
      if (_distance <= _attackRange)
      {
         // Düşmanın yönünü karakterin pozisyonuna çevirir
         _tempDir = _player.transform.position;
         
         // Farklı yükseklik seviyelerinde düşman açısının bozulmaması için yüksekliği sabit tutar
         _tempDir.y = transform.position.y;
         
         // Belirtilen yöne bakar
         transform.LookAt(_tempDir);

         // 0,0,1 yönünde ilerler 
         transform.Translate(Vector3.forward * (Time.deltaTime * _movementSpeed));

      }
   }

   void EnemyDeath()
   {
      // Düşmanın yüksekliği belirtilen yükseklikten azsa (platformdan düşmüşse)
      if (transform.position.y <= _deathHight)
      {
         // Ölüm anında yazılacak kod
      }
   }

   #endregion


}
