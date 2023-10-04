namespace ScrivenerExplorer.Extensions
{
    public static class ColorExtensions
    {
        public static Color ToColor(this string scrivenerColorFormat)
        {
            if (string.IsNullOrEmpty(scrivenerColorFormat))
            {
                return null;
            }

            var components = scrivenerColorFormat.Split(' ');
            if (components.Length != 3)
            {
                return null;
            }

            return Color.FromRgb(double.Parse(components[0]), double.Parse(components[1]), double.Parse(components[2]));
        }
    }
}
