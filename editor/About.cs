using System.Windows.Forms;

namespace EditorProject
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            versionLabel.Text = @"Version: " + Application.ProductVersion;
        }
    }
}