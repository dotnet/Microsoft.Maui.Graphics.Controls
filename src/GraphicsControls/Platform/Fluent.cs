namespace Microsoft.Maui.Graphics.Controls
{
    public static class Fluent
    {
        public static class Color
        {
            public static class Light
            {
                public static class Accent
                {
                    public static string Primary = "#005FB8";
                    public static string Secondary = "#E6005FB8";
                    public static string Tertiary = "#CC005FB8";
                    public static string Disabled = "#C8C8C8";
                }

                public static class Foreground
                {
                    public static string Primary = "#E3000000";
                    public static string Secondary = "#6E6E6E";
                    public static string Tertiary = "#70000000";
                    public static string Disabled = "#AAAAAA";
                }

                public static class Background
                {
                    public static string Transparent = "#FFFFFFFF";
                    public static string Default = "#B3FFFFFF";
                    public static string Secondary = "#80F9F9F9";
                    public static string Tertiary = "#4DF9F9F9";
                    public static string Disabled = "#4DF9F9F9";
                }

                public static class Control
                {
                    public static class Background
                    {
                        public static string Default = "#FEFEFE";
                        public static string Secondary = "#F9F9F9";
                        public static string Tertiary = "#FAFAFA";
                        public static string Disabled = "#FAFAFA";
                    }

                    public static class Border
                    {
                        public static string Default = "#E1E1E1";
                        public static string Disabled = "#D8D8D8";
                    }
                }
            }

            public static class Dark
            {
                public static class Accent
                {
                    public static string Primary = "#99EBFF";
                    public static string Secondary = "#99EBFF";
                    public static string Tertiary = "#60CDFF";
                    public static string Disabled = "#54FFFFFF";
                }

                public static class Foreground
                {
                    public static string Primary = "#E3FFFFFF";
                    public static string Secondary = "#CFCFCF";
                    public static string Tertiary = "#70FFFFFF";
                    public static string Disabled = "#D0D0D0";
                }

                public static class Background
                {
                    public static string Transparent = "#FF000000";
                    public static string Default = "##191919";
                    public static string Secondary = "#80000000";
                    public static string Tertiary = "#4D000000";
                    public static string Disabled = "#4D000000";
                }

                public static class Control
                {
                    public static class Background
                    {
                        public static string Default = "#2A2A2A";
                        public static string Secondary = "#2F2F2F";
                        public static string Tertiary = "#232323";
                        public static string Disabled = "#333333";
                    }

                    public static class Border
                    {
                        public static string Default = "#303030";
                        public static string Disabled = "#323232";
                    }
                }
            }
        }

        public static class Font
        {
            public const double Header = 46;
            public const double SubHeader = 34;
            public const double Title = 24;
            public const double SubTitle = 20;
            public const double Base = 14;
            public const double Body = 14;
            public const double Caption = 12;
        }
    }
}