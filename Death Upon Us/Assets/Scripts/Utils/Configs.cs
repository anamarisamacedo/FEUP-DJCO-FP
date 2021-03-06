namespace utils
{
    public static class Configs
    {
        /* Player mechanics */
        public const float CrouchSpeed = 4f;
        public const float WalkingSpeed = 8f;
        public const float RunningSpeed = 15f;
        public const float JumpForce = 7f;
        public const float RotationSpeed = 3f;
        
        /* Monster mechanics */
        public const float MonsterSpeed = 2f; 
        public const float MonsterFollowRadius = 25f; 

        /* Attack mechanics */
        public const float PlayerAttackRadius = 2f;
        public const float MonsterAttackRadius = 3.5f;
        public const float PlayerAttackRate = 2f;
        public const float MonsterAttackRate = 0.5f;
        public const int ArrowDamage = 20;
        public const int KnifeDamage = 35;

        public const int MonsterDamage = 10;

        /* Scenes */
        public const int MenuScene = 0;
        public const int GameScene = 1;

        /* Sounds */
        public const float DefaultVolume = 1f;

        /* Cut Scene */
        public const float CutSceneSpeed = 4; // degrees per second

        /* Hunger */
        public const int HungerOnWalk = 2;
        public const int HungerOnRun = HungerOnWalk * 2;
        public const int HungerOnJump = 70;
        public const int HungerOnMeleeAttack = 70;
        public const int HungerOnBowAttack = 50;
        public const int MinHungerValToRun = 2500;

        /* Codes */
        public const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    }
}