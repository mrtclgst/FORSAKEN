using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _characterController;
    Vector3 _moveDirection; //hangi eksende gittigimizi bilmek icin
    [SerializeField] float _moveSpeed = 5f, _jumpForce = 10f;  //karakter hareket hizi
    float _gravity = 20f, _verticalVelocity;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        _moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        //float x = Input.GetAxis("Horizontal");
        //yukaridaki gibi yazmamizin sebebi yazim hatalarindan kacinmak.
        _moveDirection = transform.TransformDirection(_moveDirection);
        //karakterimizin positionunu unitynin space'ine gore ayarliyoruz.
        _moveDirection = _moveSpeed * Time.deltaTime * _moveDirection;
        //time.deltatime iki frame arasini veriyor bu bize yumusak bir hareket imkani sagliyor.
        ApplyGravity();
        _characterController.Move(_moveDirection);
    }
    void ApplyGravity()
    {
        _verticalVelocity -= _gravity * Time.deltaTime;
        PlayerJump();
        _moveDirection.y = _verticalVelocity * Time.deltaTime;//jumpu smootlastirdigimiz yer
    }
    private void PlayerJump()
    {
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
            _verticalVelocity = _jumpForce;

    }
}