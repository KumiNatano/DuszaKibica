using UnityEngine;

public class PlayerCameraController : PlayerModule
{
    // Referencja do kamery gracza
    public PlayerCamera playerCamera => parent.playerCamera;
    // Czułość ruchu myszy
    public Vector2 sensitivity = new Vector2(5f, 5f);
    // Krzywa reprezentująca zmianę w przyśpieszniu myszy w zalezności od długości delty ruchu myszy
    public AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    public bool accelerate = true;

    public Transform eyesPoint;


    public override void OnLateUpdate(float deltaTime)
    {
        playerCamera.SetPosition(eyesPoint.position);

        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (accelerate){
            input *= accelerationCurve.Evaluate(input.magnitude);
        }
        input *= sensitivity;
        
        Vector3 viewAngles = playerCamera.viewAngles;
        viewAngles.y += input.x;
        viewAngles.x = Mathf.Clamp(viewAngles.x - input.y, -90f, 90f);

        playerCamera.SetViewAngles(viewAngles);
    }
}
