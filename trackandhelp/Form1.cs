using trackandhelp.logic.ActiveWindow;

namespace trackandhelp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var activeWindowWatcher = new ActiveWindowWatcher(TimeSpan.FromMilliseconds(500));
            activeWindowWatcher.ActiveWindowChanged += (o,e) => SetText($"Active Window: {e.ActiveWindow}");
            activeWindowWatcher.Start();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }

    }
}
