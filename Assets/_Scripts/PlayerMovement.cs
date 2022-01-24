using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed = 500;

    private Touch _touch;

    private Vector3 _touchDown;
    private Vector3 _touchUp;

    private bool _isDragStarted;


    void Update()
    {
        FirstTouch();
        DragTouch();
        RotatePlayer();
        MovePlayer();
    }
    
    // Ekrana ilk dokunulduğunda değer atamalarını yapar
    void FirstTouch()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began) // Parmak ekrana ilk değdiğinde
            {
                _isDragStarted = true;
                _touchDown = _touch.position;
                _touchUp = _touch.position;
            }
        }
    }
    
    // Sürükleme başladığında değer atamalırını yapar ve karakterin hareket & dönüş fonksiyonlarını çalıştırır
    void DragTouch()
    {
        if (_isDragStarted)
        {
            if (_touch.phase == TouchPhase.Moved) // Parmak hareket ediyorken
            {
                _touchDown = _touch.position;
            }

            if (_touch.phase == TouchPhase.Ended) // Parmak ekrandan çekildiğinde
            {
                _touchDown = _touch.position;
                _isDragStarted = false;
            }
            
        }
    }
    
    // Dokunulan noktalar arasındaki yönü hesaplar
    Vector3 CalculateDirection()
    {
        Vector3 dir = (_touchDown - _touchUp).normalized;
        dir.z = dir.y;
        dir.y = 0;

        return dir;
    }
    
    // Karakterin hesaplanan yöne bakmasını sağlar 
    Quaternion CalculateRotation()
    {
        Quaternion rotation = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
        return rotation;
    }
    
    // Karakteri hesaplanan yöne döndürür
    void RotatePlayer()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), _rotationSpeed);
    }
    
    // Karakterin ileri gitmesini sağlar
    void MovePlayer()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * _movementSpeed));
    }
}
