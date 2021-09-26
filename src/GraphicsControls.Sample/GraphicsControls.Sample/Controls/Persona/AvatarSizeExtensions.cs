namespace GraphicsControls.Sample.Controls
{
    public static class AvatarSizeExtensions
    {
        public static int GetAvatarSize(this AvatarSize avatarSize)
        {
            switch (avatarSize)
            {
                case AvatarSize.Small:
                default:
                    return 48;
                case AvatarSize.Large:
                    return 72;
                case AvatarSize.XXLarge:
                    return 120;
            }
        }

        public static int GetAvatarIndicatorSize(this AvatarSize avatarSize)
        {
            switch (avatarSize)
            {
                case AvatarSize.Small:
                default:
                    return 12;
                case AvatarSize.Large:
                    return 20;
                case AvatarSize.XXLarge:
                    return 36;
            }
        }

        public static float GetAvatarIndicatorIconScale(this AvatarSize avatarSize)
        {
            switch (avatarSize)
            {
                case AvatarSize.Small:
                default:
                    return 1.0f;
                case AvatarSize.Large:
                    return 1.5f;
                case AvatarSize.XXLarge:
                    return 2.5f;
            }
        }

        public static int GetInitialsFontSize(this AvatarSize avatarSize)
        {
            switch (avatarSize)
            {
                case AvatarSize.Small:
                default:
                    return 20;
                case AvatarSize.Large:
                    return 28;
                case AvatarSize.XXLarge:
                    return 42;
            }
        }
    }
}
