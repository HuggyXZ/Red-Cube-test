using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Singleton instance
    public static PlayerMovement Instance { get; private set; }

    // Event triggered when the player dies
    public event EventHandler OnPlayerDied;

    [SerializeField] private Rigidbody myRigidbody;

    [SerializeField] private float fowardForce = 500f;
    [SerializeField] private float sidewayForce = 500f;
    [SerializeField] private float jumpForce = 100;

    private bool touchingGround = true;

    private bool isAlive = true;

    public enum State {
        WaitingToStart,
        Normal,
        GameOver,
    }

    private State state;

    private void Awake() {
        // Singleton instance
        Instance = this;

        state = State.WaitingToStart;
    }

    // Use FixedUpdate for physics updates like adding force
    private void FixedUpdate() {

        switch (state) {
            default:
            case State.WaitingToStart:
                // Press any key to start the game
                if (GameInput.Instance.IsRightActionPressed() ||
                    GameInput.Instance.IsLeftActionPressed() ||
                    GameInput.Instance.IsJumpActionPressed()) {
                    state = State.Normal;
                }
                break;
            case State.Normal:
                // Add a forward force to the player
                myRigidbody.AddForce(0, 0, fowardForce * Time.fixedDeltaTime);

                // Check for player input and add sideway forces accordingly
                if (GameInput.Instance.IsRightActionPressed()) {
                    myRigidbody.AddForce(sidewayForce * Time.fixedDeltaTime, 0, 0, ForceMode.VelocityChange);
                }

                if (GameInput.Instance.IsLeftActionPressed()) {
                    myRigidbody.AddForce(-sidewayForce * Time.fixedDeltaTime, 0, 0, ForceMode.VelocityChange);
                }

                if (GameInput.Instance.IsJumpActionPressed() && touchingGround) {
                    myRigidbody.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
                    touchingGround = false;
                }

                if (myRigidbody.position.y < -1) {
                    state = State.GameOver;
                    OnPlayerDied?.Invoke(this, EventArgs.Empty);
                    GameManager.Instance.EndGame();
                }
                break;
            case State.GameOver:
                // Do nothing
                break;
        }


    }


    private void OnCollisionEnter(Collision collision) {
        // check if that game object has the component of that type ("Obstacle")
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle)) {
            if (isAlive) {
                state = State.GameOver;
                // Invoke the OnPlayerDied event
                OnPlayerDied?.Invoke(this, EventArgs.Empty);
                GameManager.Instance.EndGame();
                isAlive = false;
            }
            return;
        }

        touchingGround = true;
    }
}