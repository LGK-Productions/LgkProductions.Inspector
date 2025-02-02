using System.ComponentModel;

namespace WinForms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        Form window = new()
        {
            Text = "Hello, WinForms!",
            Controls =
            {
                new PropertyGrid()
                {
                    Dock = DockStyle.Fill,
                    SelectedObject = new TestModel()
                }
            }
        };
        Application.Run(window);
    }

    sealed class TestModel
    {
        public string? Test { get; set; }

        [ReadOnly(true)]
        public bool ReadOnlyTest { get; set; } = true;

        [Category("BoxGroup")]
        public int CustomCategoryTest { get; set; }
    }
}