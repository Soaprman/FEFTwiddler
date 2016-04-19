using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEFTwiddler.GUI.Controls
{
    public class Blanket : Panel
    {
        private Label _message;

        public static Blanket AttachTo(Control parent)
        {
            var blanket = new Blanket();
            parent.Controls.Add(blanket);

            blanket.Top = 0;
            blanket.Left = 0;
            blanket.Width = parent.Width;
            blanket.Height = parent.Height;
            blanket.BringToFront();

            blanket.Uncover();

            return blanket;
        }

        public Blanket()
        {
            _message = new Label();
            this.Controls.Add(_message);
        }

        public void SetMessage(string message)
        {
            _message.Text = message;
            _message.Width = Parent.Width;
            _message.Height = Parent.Height;
            _message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            _message.Top = (this.Height / 2) - (_message.Height / 2);
            _message.Left = (this.Width / 2) - (_message.Width / 2);
        }

        public void Cover()
        {
            this.Visible = true;
        }

        public void Uncover()
        {
            this.Visible = false;
        }
    }
}
