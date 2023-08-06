using System.Windows.Forms;

namespace AtominaCraft.ZResources.Windows {
    public partial class DebugWindow : Form {
        public DebugWindow() {
            InitializeComponent();
        }

        public void Write(string text) {
            this.label1.Text += text;
        }

        public void SetText(string text) {
            this.label1.Text = text;
        }

        public void Clear() {
            this.label1.Text = "";
        }
    }
}