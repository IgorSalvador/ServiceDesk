using MudBlazor;

namespace ServiceDesk.Web;

public static class Configuration
{
    public static string BackendUrl { get; set; } = string.Empty;

    public static MudTheme Theme = new()
    {
        Typography = new()
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "Segoe UI", "Roboto", "Helvetica", "Arial", "sans-serif" },
                FontSize = "0.875rem",
                FontWeight = "400",
                LineHeight = "1.5",
                LetterSpacing = ".005em"
            },
            H5 = new H5Typography()
            {
                FontSize = "1.5rem",
                FontWeight = "600"
            }
        },

        PaletteLight = new()
        {
            Primary = "#0078D4",
            PrimaryDarken = "#005A9E",
            Secondary = "#506070",
            Background = "#F3F5F7",
            AppbarBackground = "#FFFFFF",
            AppbarText = "#283E50",

            DrawerBackground = "#FFFFFF",
            DrawerText = "#404040",
            DrawerIcon = "#0078D4",

            Success = "#107C10",
            Warning = "#D83B01",
            Error = "#A80000",
            Info = "#0078D4",

            TextPrimary = "#323130",
            TextSecondary = "#605E5C",
        },

        LayoutProperties = new()
        {
            DefaultBorderRadius = "6px",
            DrawerWidthLeft = "260px"
        }
    };
}
