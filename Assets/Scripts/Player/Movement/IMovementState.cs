namespace Player.Movement
{
    public interface IMovementState
    {
        public bool IsOnMovement { get; }
        public float MovementDirection { get; }
    }
}