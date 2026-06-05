using UnityEngine;

namespace K_PathFinder.Samples {
    [RequireComponent(typeof(CharacterController))]
    public class ScaredCritter : MonoBehaviour {
        public GameObject Player;
        
        [Header("Fear Settings")]
        public float panicDistance = 5f; // Radius at which the critter gets scared
        [Range(0.1f, 6f)] public float runSpeed = 5f; // Fleeing speed

        private CharacterController _controller;
        private MeshRenderer _renderer;

        void Start() {
            _controller = GetComponent<CharacterController>();
            _renderer = GetComponent<MeshRenderer>();
            
            // Automatically find the player object in the scene
            if (Player == null) {
                Player = GameObject.Find("Player");
            }
            
            // The critter is calm by default - set color to green
            if (_renderer != null) {
                _renderer.material.color = Color.green;
            }
        }

        void Update() {
            // Safety check: ensure we have both the controller and the player
            if (_controller == null || Player == null) return;

            // 1. Calculate the distance between the critter and the player
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

            // 2. If the player enters the panic radius - FLEE!
            if (distanceToPlayer < panicDistance) {
                
                // Critter is panicking - change color to red
                if (_renderer != null) _renderer.material.color = Color.red;

                // MAGIC: Subtract positions backwards to get a vector pointing AWAY from the player
                Vector3 fleeDirection = transform.position - Player.transform.position;
                fleeDirection.y = 0; // Keep the movement on the ground plane (prevent flying)

                // Move the critter in the opposite direction
                if (fleeDirection.magnitude > 0.1f) {
                    _controller.SimpleMove(fleeDirection.normalized * runSpeed);
                }
            } 
            // 3. If the player is far away - CALM DOWN AND STOP
            else {
                // Change color back to green
                if (_renderer != null) _renderer.material.color = Color.green;
                
                // Hit the brakes to stop the drift
                _controller.SimpleMove(Vector3.zero); 
            }
        }
    }
}

