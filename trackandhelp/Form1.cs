using trackandhelp.logic.ActiveWindow;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Diagnostics;

namespace trackandhelp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var activeWindowWatcher = new ActiveWindowWatcher(TimeSpan.FromMilliseconds(500));
            activeWindowWatcher.ActiveWindowChanged += (o, e) => SetText($"Active Window: {e.ActiveWindow}");
            activeWindowWatcher.Start();

            using (var connection = new SqliteConnection("Data Source=tracknhelp.db;Mode=ReadWriteCreate"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT window_name FROM tracknhelp";
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var window_name = reader.GetString(0);
                        Debug.WriteLine($"Window Name from DB: {window_name}!");
                    }

                }
            } 
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
