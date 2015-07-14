using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsVicoClient.ctrls
{
    public partial class PDFReader : UserControl
    {
        public PDFReader(string fileName)
        {
            InitializeComponent();

            this.axAcroPDF1.LoadFile(fileName);

            axAcroPDF1.setPageMode("thumbs");
            axAcroPDF1.setShowScrollbars(true);

            this.Disposed += new EventHandler(PDFReader_Disposed);
        }

        void PDFReader_Disposed(object sender, EventArgs e)
        {
            if (!axAcroPDF1.IsDisposed)
            {
                axAcroPDF1.Dispose();
            }
        }
    }
}
