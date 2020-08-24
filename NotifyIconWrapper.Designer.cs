using System.Windows;

namespace MouseOverColor
{
    partial class NotifyIconWrapper
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.ContextMenu;
            this.notifyIcon1.Text = "MouseOverColor";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowWindow,
            this.Exit});
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.Size = new System.Drawing.Size(148, 48);
            // 
            // ShowWindow
            // 
            this.ShowWindow.Name = "ShowWindow";
            this.ShowWindow.Size = new System.Drawing.Size(147, 22);
            this.ShowWindow.Text = "ShowWindow";
            // 
            // Exit
            // 
            this.Exit.AccessibleName = "Exit";
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(147, 22);
            this.Exit.Text = "Exit";
            this.ContextMenu.ResumeLayout(false);

        }

        private void NotifyIconWrapper_Click(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ShowWindow;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}
