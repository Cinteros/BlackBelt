﻿namespace Cinteros.Xrm.ViewDesignerTool.Controls
{
    partial class ViewEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvDesign = new System.Windows.Forms.ListView();
            this.tlDesign = new System.Windows.Forms.TableLayoutPanel();
            this.tlDesign.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDesign
            // 
            this.lvDesign.AllowColumnReorder = true;
            this.lvDesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDesign.FullRowSelect = true;
            this.lvDesign.GridLines = true;
            this.lvDesign.Location = new System.Drawing.Point(3, 73);
            this.lvDesign.Name = "lvDesign";
            this.lvDesign.Size = new System.Drawing.Size(594, 324);
            this.lvDesign.TabIndex = 0;
            this.lvDesign.UseCompatibleStateImageBehavior = false;
            this.lvDesign.View = System.Windows.Forms.View.Details;
            // 
            // tlDesign
            // 
            this.tlDesign.ColumnCount = 1;
            this.tlDesign.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDesign.Controls.Add(this.lvDesign, 0, 1);
            this.tlDesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlDesign.Location = new System.Drawing.Point(0, 0);
            this.tlDesign.Name = "tlDesign";
            this.tlDesign.RowCount = 2;
            this.tlDesign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlDesign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDesign.Size = new System.Drawing.Size(600, 400);
            this.tlDesign.TabIndex = 1;
            // 
            // ViewEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlDesign);
            this.Name = "ViewEditor";
            this.Size = new System.Drawing.Size(600, 400);
            this.tlDesign.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvDesign;
        private System.Windows.Forms.TableLayoutPanel tlDesign;
    }
}
