namespace test12._0
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.showListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(156, 86);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(438, 22);
            this.urlTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "開始网址";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(316, 378);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 50);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "開始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // showListBox
            // 
            this.showListBox.FormattingEnabled = true;
            this.showListBox.ItemHeight = 12;
            this.showListBox.Location = new System.Drawing.Point(156, 162);
            this.showListBox.Name = "showListBox";
            this.showListBox.Size = new System.Drawing.Size(438, 196);
            this.showListBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 503);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.showListBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.urlTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ListBox showListBox;
        private System.Windows.Forms.Label label2;
    }
}

