namespace Presentacion_Escritorio
{
    partial class AddLoanForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AddLoanForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "AddLoanForm";
            this.Load += new System.EventHandler(this.AddLoanForm_Load);
            this.ResumeLayout(false);

        }
    }
}
