using System;
using OdinNative.Odin.Peer;
using OdinNetworking;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Odin.OdinNetworking
{
    public class OdinPlayer : OdinNetworkIdentity
    {
        [Header("Player Settings")]
        
        public TextMeshPro playerName;

        [OdinSyncVar(hook = nameof(OnNameChanged))]
        public string Name;
        
        public float movementSpeed = 2.0f;
        
        private StarterAssetsInputs _input;
        private CharacterController _characterController;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _input = GetComponent<StarterAssetsInputs>();
            _characterController = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();   
        }

        // Start is called before the first frame update
        void Start()
        {

            Debug.Log($"Added player with peer Id: {Peer.Id}");

            Name = $"Player_{Peer.Id}";
        }

        // Update is called once per frame
        void Update()
        {
            playerName.text = Name;

            if (!IsLocalPlayer())
            {
                return;
            }
            
            transform.localPosition += new Vector3(_input.move.x * Time.deltaTime * movementSpeed, 0, _input.move.y * Time.deltaTime * movementSpeed);
        }

        public override void OnStartLocalClient()
        {
            base.OnStartLocalClient();

            _characterController.enabled = true;
            _playerInput.enabled = true;
        }


        public void OnNameChanged(string oldValue, string newValue)
        {
            
        }
    }
}

