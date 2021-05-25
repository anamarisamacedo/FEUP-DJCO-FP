namespace utils
{
    public static class Configs
    {
        /* Player mechanics */
        public const float CrouchSpeed = 4f;
        public const float WalkingSpeed = 8f;
        public const float RunningSpeed = 15f;
        public const float JumpForce = 7f;
        public const float RotationSpeed = 6f;
        
        /* Monster mechanics */
        public const float MonsterSpeed = 7f; 
        public const float MonsterFollowRadius = 15f; 

        /* Attack mechanics */
        public const float PlayerAttackRadius = 2f;
        public const float PlayerAttackRate = 2f;

        /* Scenes */
        public const int MenuScene = 0;
        public const int GameScene = 1;

        /* Sounds */
        public const float DefaultVolume = 1f;
    }
}