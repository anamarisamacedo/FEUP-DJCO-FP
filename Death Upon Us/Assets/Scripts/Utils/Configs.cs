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
        public const float MonsterSpeed = 2f; 
        public const float MonsterFollowRadius = 25f; 

        /* Attack mechanics */
        public const float PlayerAttackRadius = 2f;
        public const float PlayerAttackRate = 2f;

        /* Scenes */
        public const int MenuScene = 0;
        public const int GameScene = 1;

        /* Sounds */
        public const float DefaultVolume = 1f;

        /* Cut Scene */
        public const float CutSceneSpeed = 4; // degrees per second

        /* Hunger */
        public const int HungerOnWalk = 3;
        public const int HungerOnRun = HungerOnWalk * 2;
        public const int HungerOnJump = 100;
        public const int HungerOnMeleeAttack = 100;
        public const int HungerOnBowAttack = 50;
        public const int MinHungerValToRun = 2500;
    }
}