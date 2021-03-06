using UnityEngine.InputSystem;

namespace Input
{
    public sealed class BoolInputActionHandler : InputActionHandler<bool>
    {
        protected override void OnActionStarted(InputAction.CallbackContext ctx)
        {
            if (onActionStartedEvent) onActionStartedEvent.Invoke(ctx.ReadValueAsButton());
        }

        protected override void OnActionPerformed(InputAction.CallbackContext ctx)
        {
            if (onActionPerformedEvent) onActionPerformedEvent.Invoke(ctx.ReadValueAsButton());
        }

        protected override void OnActionCanceled(InputAction.CallbackContext ctx)
        {
            if (onActionCanceledEvent) onActionCanceledEvent.Invoke(ctx.ReadValueAsButton());
        }
    }
}