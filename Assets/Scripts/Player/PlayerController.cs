using System.Collections;
using UnityEngine;

public class PlayerController : PlayerModule
{
    public float walkSpeed = 5f;
    public float runSpeed = 7.5f;
    public float runStamina = 12.5f;
    public float duckSpeed = 2.5f;
    public float duckStep = 4f;

    public bool isRunning = false;
    public bool isDucking = false;

    public PlayerCamera playerCamera => parent.playerCamera;
    public CharacterController controller => parent.characterController;


    public override void OnUpdate(float deltaTime)
    {
        Duck();
        Move();
    }
    public override void OnFixedUpdate(float deltaTime)
    {
        Run();
        UseStamina();
    }


    private void Move(){
        float speed;
        float vy = playerCamera.viewAngles.y;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 dir = AngleToDir(vy) * input.y + AngleToDir(vy + 90f) * input.x;

        isRunning = Input.GetButton("Sprint") && input.y > float.Epsilon;

        if (isDucking){
            speed = duckSpeed;
        }
        else if (isRunning){
            speed = runSpeed;
        }
        else {
            speed = walkSpeed;
        }

        dir.Normalize();
        controller.SimpleMove(dir * speed);
    }
    private void Duck(){
        bool i = Input.GetButton("Duck");
        if (isDucking != i)
        {
            isDucking = i;
            StartCoroutine(DoDuck(controller.height, isDucking ? parent.duckHeight : parent.normalHeight));
        }
    }
    private void Run()
    {
        isRunning = Input.GetButton("Sprint") && Input.GetAxisRaw("Vertical") > float.Epsilon && parent.stamina.points > runStamina * Time.fixedDeltaTime;
    }
    private void UseStamina()
    {
        if (isRunning)
        {
            parent.stamina.UsePoints(runStamina * Time.fixedDeltaTime);
        }
    }
    private IEnumerator DoDuck(float from, float to){
        bool c = isDucking;
        float t = from;
        
        bool ct = true;
        while (ct)
        {
            if (from > to)
            {
                t -= duckStep * Time.deltaTime;
                if(t < to)
                {
                    ct = false;
                    continue;
                }
            }
            else
            {
                t += duckStep * Time.deltaTime;
                if (t > to){
                    ct = false;
                    continue;
                }
            }
            parent.SetHeight(t);

            if (c != isDucking){
                yield break;
            }
            yield return null;
        }
        parent.SetHeight(to);
    }
    private Vector3 AngleToDir(float angle){
        return Quaternion.Euler(Vector3.up * angle) * Vector3.forward;
    }
}
