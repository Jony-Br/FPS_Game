using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerCCStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) 
    {
       
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        Ctx.CCAnimator.SetBool(Ctx.IsCrouchingHash, true);
        Ctx.Speed = Ctx.Speed / 2;
        Ctx.Controller.height = Ctx.Controller.height / 1.4f;
        Ctx.Controller.center = Ctx.Controller.center / 2;
        Ctx.MainCamera.transform.position = Ctx._LerpCameraTo.transform.position;
    }

    public override void UpdateState()
    {
        GravityHandler();
        CheckSwitchStates();
    }

    public override void ExitState() 
    {
        Ctx.CCAnimator.SetBool(Ctx.IsCrouchingHash, false);
        Ctx.IsCrouchPressed = false;
        Ctx.Speed = Ctx.Speed * 2;
        Ctx.Controller.height = Ctx.Controller.height * 1.4f;
        Ctx.Controller.center = Ctx.Controller.center * 2;
        Ctx.MainCamera.transform.position = Ctx._LerpCameraFrom.transform.position;
    }

    public override void InitializeSubState() 
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Walk());
        }


    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded && !Ctx.IsCrouchPressed)
        {   
            SwitchState(Factory.Grounded());
        }
        else if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchState(Factory.Jump());
        }
    }

    private void GravityHandler()
    {

        Ctx.IsGrounded = Physics.CheckSphere(Ctx.GroundCheck.position, Ctx.GroundDistance, Ctx.GroundMask);

        if (Ctx.IsGrounded && Ctx._velocity.y < 0)
        {
            Ctx._velocity.y = -2f;
        }

        Ctx._velocity.y += Ctx.Gravity * Time.deltaTime;
        Ctx.Controller.Move(Ctx._velocity * Time.deltaTime);
    }

}
