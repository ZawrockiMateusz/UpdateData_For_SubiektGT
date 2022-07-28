using System;
using System.Windows.Forms;

namespace UpdateData_For_SubiektGT
{
    public partial class AddNote : Form
    {
        public AddNote()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            note = txtNote.Text;
            Close();
        }
        private string note;

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

    }
}
