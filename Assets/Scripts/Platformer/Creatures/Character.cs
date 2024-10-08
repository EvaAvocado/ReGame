using System;
using FSM;
using FSM.States;
using Items;
using Platformer.FSM;
using UI;
using UnityEngine;
using Utils;

namespace Platformer.Creatures
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private LayerMask _platformLayer;
        //[SerializeField] private LayerMask _itemLayer;
        [SerializeField] private LayerMask _exitLayer;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private PointsInPlatformer _points;
        public GameObject parent;

        private Vector3 _startPos;
        private float _currentSpeed;
        private bool _isJumping;
        private CharacterStateMachine _characterStateMachine;

        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int IsJumping = Animator.StringToHash("is-jumping");

        public void Awake()
        {
            _startPos = gameObject.transform.position;
            _characterStateMachine = new CharacterStateMachine();
            _characterStateMachine.EnterIn<IdleCharacterState>();
        }

        private void OnEnable()
        {
            _characterStateMachine.States[typeof(IdleCharacterState)].EnterHandler += Idle;
            _characterStateMachine.States[typeof(RunCharacterState)].EnterHandler += Run;
            _characterStateMachine.States[typeof(JumpCharacterState)].EnterHandler += Jump;
        }

        private void OnDisable()
        {
            _characterStateMachine.States[typeof(IdleCharacterState)].EnterHandler -= Idle;
            _characterStateMachine.States[typeof(RunCharacterState)].EnterHandler -= Run;
            _characterStateMachine.States[typeof(JumpCharacterState)].EnterHandler -= Jump;
        }

        private void Idle()
        {
            _currentSpeed = 0;
            _isJumping = false;
            
            _animator.SetBool(IsRunning, false);
            _animator.SetBool(IsJumping, false);
        }

        private void Run()
        {
            _currentSpeed = _speed;
            _isJumping = false;
            
            _animator.SetBool(IsRunning, true);
            _animator.SetBool(IsJumping, false);
        }

        private void Jump()
        {
            _currentSpeed = 0;
            _isJumping = true;
            
            _rb.AddForce(Vector2.up * _jumpForce);
            
            _animator.SetBool(IsJumping, true);
        }

        private void Flip()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        private void FixedUpdate()
        {
            if (_currentSpeed != 0 && !_isJumping)
            {
                _rb.AddForce((_spriteRenderer.flipX ? Vector2.left : Vector2.right) * (_currentSpeed * Time.fixedDeltaTime));
            }
        }

        public void Spawn()
        {
            if (parent.activeSelf)
            {
                transform.position = _startPos;
                _characterStateMachine.EnterIn<IdleCharacterState>();
                _rb.velocity = Vector2.zero;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_wallLayer.Contains(other.gameObject.layer))
            {
                Flip();
            }

            if (_platformLayer.Contains(other.gameObject.layer))
            {
                _characterStateMachine.EnterIn<JumpCharacterState>();
            }
            
            /*if (_itemLayer.Contains(other.gameObject.layer))
            {
                if (other.TryGetComponent(out Star star))
                {
                    star.DeleteThis();
                    _points.AddScore();
                }
            }*/
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_platformLayer.Contains(other.gameObject.layer))
            {
                _characterStateMachine.EnterIn<RunCharacterState>();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_exitLayer.Contains(other.gameObject.layer))
            {
                Spawn();
            }
        }
    }
}