using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
   
    public PlayerRunState(PlayerCCStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState() 
    {
        Ctx.Speed = Ctx.Speed * 2;
    }

    public override void UpdateState()
    {
        Ctx.CCAnimator.SetFloat(Ctx.HorizontalPatameterName, Ctx.HorizontalInput);
        Ctx.CCAnimator.SetFloat(Ctx.VerticalPatameterName, Ctx.VericalInput);
        Ctx.move = Ctx.gameObject.transform.right * Ctx.HorizontalInput + Ctx.gameObject.transform.forward * Ctx.VericalInput;
        Ctx.Controller.Move(Ctx.move * Ctx.Speed * Time.deltaTime);
        CheckSwitchStates();
    }

    public override void ExitState() 
    {
        Ctx.Speed = Ctx.Speed /2;

    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates() 
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SwitchState(Factory.Walk());
        }


    }
}
