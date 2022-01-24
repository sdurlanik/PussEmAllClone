using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : MonoBehaviour
{
   #region Variables

   [SerializeField] private Animator anim;

   private Touch _touch;
   private bool _isTouched;
   private bool _isOpen;

   public bool IsOpen => _isOpen;

   #endregion

   #region Unity Funcs

   private void Update()
   {
      PoleShoot();
   }

   #endregion

   #region Custom Funcs

   void PoleShoot()
   {
      if (Input.touchCount > 0)
      {
         _touch = Input.GetTouch(0);
         _isTouched = true;
      }

      if (_isTouched)
      {
         if (_touch.phase == TouchPhase.Ended)
         {
            anim.SetTrigger("Shoot");
            StartCoroutine(CancelShoot());
            _isTouched = false;
         }
      }
   }

   #endregion

   #region System Funcs

   IEnumerator CancelShoot()
   {
      _isOpen = true;
      yield return new WaitForSeconds(0.75f);
      _isOpen = false;
   }

   #endregion


}
