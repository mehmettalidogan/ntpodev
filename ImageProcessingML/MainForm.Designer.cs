namespace ImageProcessingML
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnMatrixMultiply;
        private System.Windows.Forms.Button btnLinearRegression;
        private System.Windows.Forms.Button btnKMeans;
        private System.Windows.Forms.Button btnBlurFilter;
        private System.Windows.Forms.Button btnEdgeDetection;
        private System.Windows.Forms.Button btnSharpen;
        private System.Windows.Forms.Button btnBrightness;
        private System.Windows.Forms.Button btnDarken;
        private System.Windows.Forms.Button btnContrast;
        private System.Windows.Forms.Button btnSepia;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button btnShowHistogram;
        private System.Windows.Forms.GroupBox groupBoxMatrix;
        private System.Windows.Forms.GroupBox groupBoxML;
        private System.Windows.Forms.GroupBox groupBoxImage;
        private System.Windows.Forms.GroupBox groupBoxImageDisplay;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picOriginal;
        private System.Windows.Forms.PictureBox picProcessed;
        private System.Windows.Forms.Label lblOriginal;
        private System.Windows.Forms.Label lblProcessed;

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
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnMatrixMultiply = new System.Windows.Forms.Button();
            this.btnLinearRegression = new System.Windows.Forms.Button();
            this.btnKMeans = new System.Windows.Forms.Button();
            this.btnBlurFilter = new System.Windows.Forms.Button();
            this.btnEdgeDetection = new System.Windows.Forms.Button();
            this.btnSharpen = new System.Windows.Forms.Button();
            this.btnBrightness = new System.Windows.Forms.Button();
            this.btnDarken = new System.Windows.Forms.Button();
            this.btnContrast = new System.Windows.Forms.Button();
            this.btnSepia = new System.Windows.Forms.Button();
            this.btnInvert = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnShowHistogram = new System.Windows.Forms.Button();
            this.groupBoxMatrix = new System.Windows.Forms.GroupBox();
            this.groupBoxML = new System.Windows.Forms.GroupBox();
            this.groupBoxImage = new System.Windows.Forms.GroupBox();
            this.groupBoxImageDisplay = new System.Windows.Forms.GroupBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picOriginal = new System.Windows.Forms.PictureBox();
            this.picProcessed = new System.Windows.Forms.PictureBox();
            this.lblOriginal = new System.Windows.Forms.Label();
            this.lblProcessed = new System.Windows.Forms.Label();
            this.groupBoxMatrix.SuspendLayout();
            this.groupBoxML.SuspendLayout();
            this.groupBoxImage.SuspendLayout();
            this.groupBoxImageDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessed)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1160, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🖼️ Image Processing & Machine Learning - Advanced";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxMatrix
            // 
            this.groupBoxMatrix.Controls.Add(this.btnMatrixMultiply);
            this.groupBoxMatrix.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxMatrix.Location = new System.Drawing.Point(12, 60);
            this.groupBoxMatrix.Name = "groupBoxMatrix";
            this.groupBoxMatrix.Size = new System.Drawing.Size(200, 120);
            this.groupBoxMatrix.TabIndex = 1;
            this.groupBoxMatrix.TabStop = false;
            this.groupBoxMatrix.Text = "Matrix İşlemleri";
            // 
            // btnMatrixMultiply
            // 
            this.btnMatrixMultiply.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnMatrixMultiply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatrixMultiply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMatrixMultiply.ForeColor = System.Drawing.Color.White;
            this.btnMatrixMultiply.Location = new System.Drawing.Point(15, 30);
            this.btnMatrixMultiply.Name = "btnMatrixMultiply";
            this.btnMatrixMultiply.Size = new System.Drawing.Size(170, 70);
            this.btnMatrixMultiply.TabIndex = 0;
            this.btnMatrixMultiply.Text = "Matrix Çarpımı";
            this.btnMatrixMultiply.UseVisualStyleBackColor = false;
            this.btnMatrixMultiply.Click += new System.EventHandler(this.btnMatrixMultiply_Click);
            // 
            // groupBoxML
            // 
            this.groupBoxML.Controls.Add(this.btnLinearRegression);
            this.groupBoxML.Controls.Add(this.btnKMeans);
            this.groupBoxML.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxML.Location = new System.Drawing.Point(230, 60);
            this.groupBoxML.Name = "groupBoxML";
            this.groupBoxML.Size = new System.Drawing.Size(200, 200);
            this.groupBoxML.TabIndex = 2;
            this.groupBoxML.TabStop = false;
            this.groupBoxML.Text = "Machine Learning";
            // 
            // btnLinearRegression
            // 
            this.btnLinearRegression.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnLinearRegression.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLinearRegression.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLinearRegression.ForeColor = System.Drawing.Color.White;
            this.btnLinearRegression.Location = new System.Drawing.Point(15, 30);
            this.btnLinearRegression.Name = "btnLinearRegression";
            this.btnLinearRegression.Size = new System.Drawing.Size(170, 70);
            this.btnLinearRegression.TabIndex = 0;
            this.btnLinearRegression.Text = "Linear Regression";
            this.btnLinearRegression.UseVisualStyleBackColor = false;
            this.btnLinearRegression.Click += new System.EventHandler(this.btnLinearRegression_Click);
            // 
            // btnKMeans
            // 
            this.btnKMeans.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnKMeans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKMeans.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKMeans.ForeColor = System.Drawing.Color.White;
            this.btnKMeans.Location = new System.Drawing.Point(15, 115);
            this.btnKMeans.Name = "btnKMeans";
            this.btnKMeans.Size = new System.Drawing.Size(170, 70);
            this.btnKMeans.TabIndex = 1;
            this.btnKMeans.Text = "K-Means Clustering";
            this.btnKMeans.UseVisualStyleBackColor = false;
            this.btnKMeans.Click += new System.EventHandler(this.btnKMeans_Click);
            // 
            // groupBoxImage
            // 
            this.groupBoxImage.Controls.Add(this.btnLoadImage);
            this.groupBoxImage.Controls.Add(this.btnBlurFilter);
            this.groupBoxImage.Controls.Add(this.btnEdgeDetection);
            this.groupBoxImage.Controls.Add(this.btnSharpen);
            this.groupBoxImage.Controls.Add(this.btnBrightness);
            this.groupBoxImage.Controls.Add(this.btnDarken);
            this.groupBoxImage.Controls.Add(this.btnContrast);
            this.groupBoxImage.Controls.Add(this.btnSepia);
            this.groupBoxImage.Controls.Add(this.btnInvert);
            this.groupBoxImage.Controls.Add(this.btnSaveImage);
            this.groupBoxImage.Controls.Add(this.btnShowHistogram);
            this.groupBoxImage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxImage.Location = new System.Drawing.Point(450, 60);
            this.groupBoxImage.Name = "groupBoxImage";
            this.groupBoxImage.Size = new System.Drawing.Size(370, 440);
            this.groupBoxImage.TabIndex = 3;
            this.groupBoxImage.TabStop = false;
            this.groupBoxImage.Text = "Görüntü İşleme";
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoadImage.ForeColor = System.Drawing.Color.White;
            this.btnLoadImage.Location = new System.Drawing.Point(15, 25);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(165, 40);
            this.btnLoadImage.TabIndex = 0;
            this.btnLoadImage.Text = "📁 Görüntü Yükle";
            this.btnLoadImage.UseVisualStyleBackColor = false;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnBlurFilter
            // 
            this.btnBlurFilter.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnBlurFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlurFilter.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnBlurFilter.ForeColor = System.Drawing.Color.White;
            this.btnBlurFilter.Location = new System.Drawing.Point(15, 75);
            this.btnBlurFilter.Name = "btnBlurFilter";
            this.btnBlurFilter.Size = new System.Drawing.Size(110, 45);
            this.btnBlurFilter.TabIndex = 1;
            this.btnBlurFilter.Text = "Blur";
            this.btnBlurFilter.UseVisualStyleBackColor = false;
            this.btnBlurFilter.Click += new System.EventHandler(this.btnBlurFilter_Click);
            // 
            // btnEdgeDetection
            // 
            this.btnEdgeDetection.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnEdgeDetection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdgeDetection.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnEdgeDetection.ForeColor = System.Drawing.Color.White;
            this.btnEdgeDetection.Location = new System.Drawing.Point(135, 75);
            this.btnEdgeDetection.Name = "btnEdgeDetection";
            this.btnEdgeDetection.Size = new System.Drawing.Size(110, 45);
            this.btnEdgeDetection.TabIndex = 2;
            this.btnEdgeDetection.Text = "Edge";
            this.btnEdgeDetection.UseVisualStyleBackColor = false;
            this.btnEdgeDetection.Click += new System.EventHandler(this.btnEdgeDetection_Click);
            // 
            // btnSharpen
            // 
            this.btnSharpen.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnSharpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSharpen.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSharpen.ForeColor = System.Drawing.Color.White;
            this.btnSharpen.Location = new System.Drawing.Point(255, 75);
            this.btnSharpen.Name = "btnSharpen";
            this.btnSharpen.Size = new System.Drawing.Size(100, 45);
            this.btnSharpen.TabIndex = 3;
            this.btnSharpen.Text = "Sharpen";
            this.btnSharpen.UseVisualStyleBackColor = false;
            this.btnSharpen.Click += new System.EventHandler(this.btnSharpen_Click);
            // 
            // btnBrightness
            // 
            this.btnBrightness.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnBrightness.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrightness.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnBrightness.ForeColor = System.Drawing.Color.White;
            this.btnBrightness.Location = new System.Drawing.Point(15, 130);
            this.btnBrightness.Name = "btnBrightness";
            this.btnBrightness.Size = new System.Drawing.Size(110, 45);
            this.btnBrightness.TabIndex = 4;
            this.btnBrightness.Text = "☀️ Brighten";
            this.btnBrightness.UseVisualStyleBackColor = false;
            this.btnBrightness.Click += new System.EventHandler(this.btnBrightness_Click);
            // 
            // btnDarken
            // 
            this.btnDarken.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnDarken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDarken.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnDarken.ForeColor = System.Drawing.Color.White;
            this.btnDarken.Location = new System.Drawing.Point(135, 130);
            this.btnDarken.Name = "btnDarken";
            this.btnDarken.Size = new System.Drawing.Size(110, 45);
            this.btnDarken.TabIndex = 5;
            this.btnDarken.Text = "🌙 Darken";
            this.btnDarken.UseVisualStyleBackColor = false;
            this.btnDarken.Click += new System.EventHandler(this.btnDarken_Click);
            // 
            // btnContrast
            // 
            this.btnContrast.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnContrast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContrast.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnContrast.ForeColor = System.Drawing.Color.White;
            this.btnContrast.Location = new System.Drawing.Point(255, 130);
            this.btnContrast.Name = "btnContrast";
            this.btnContrast.Size = new System.Drawing.Size(100, 45);
            this.btnContrast.TabIndex = 6;
            this.btnContrast.Text = "Contrast";
            this.btnContrast.UseVisualStyleBackColor = false;
            this.btnContrast.Click += new System.EventHandler(this.btnContrast_Click);
            // 
            // btnSepia
            // 
            this.btnSepia.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnSepia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSepia.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSepia.ForeColor = System.Drawing.Color.White;
            this.btnSepia.Location = new System.Drawing.Point(15, 185);
            this.btnSepia.Name = "btnSepia";
            this.btnSepia.Size = new System.Drawing.Size(110, 45);
            this.btnSepia.TabIndex = 7;
            this.btnSepia.Text = "Sepia";
            this.btnSepia.UseVisualStyleBackColor = false;
            this.btnSepia.Click += new System.EventHandler(this.btnSepia_Click);
            // 
            // btnInvert
            // 
            this.btnInvert.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnInvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvert.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnInvert.ForeColor = System.Drawing.Color.White;
            this.btnInvert.Location = new System.Drawing.Point(135, 185);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new System.Drawing.Size(110, 45);
            this.btnInvert.TabIndex = 8;
            this.btnInvert.Text = "Invert";
            this.btnInvert.UseVisualStyleBackColor = false;
            this.btnInvert.Click += new System.EventHandler(this.btnInvert_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnSaveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveImage.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnSaveImage.ForeColor = System.Drawing.Color.White;
            this.btnSaveImage.Location = new System.Drawing.Point(190, 25);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(165, 40);
            this.btnSaveImage.TabIndex = 9;
            this.btnSaveImage.Text = "💾 Kaydet";
            this.btnSaveImage.UseVisualStyleBackColor = false;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnShowHistogram
            // 
            this.btnShowHistogram.BackColor = System.Drawing.Color.FromArgb(230, 126, 34);
            this.btnShowHistogram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHistogram.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnShowHistogram.ForeColor = System.Drawing.Color.White;
            this.btnShowHistogram.Location = new System.Drawing.Point(255, 185);
            this.btnShowHistogram.Name = "btnShowHistogram";
            this.btnShowHistogram.Size = new System.Drawing.Size(100, 45);
            this.btnShowHistogram.TabIndex = 10;
            this.btnShowHistogram.Text = "📊 Histogram";
            this.btnShowHistogram.UseVisualStyleBackColor = false;
            this.btnShowHistogram.Click += new System.EventHandler(this.btnShowHistogram_Click);
            // 
            // groupBoxImageDisplay
            // 
            this.groupBoxImageDisplay.Controls.Add(this.picOriginal);
            this.groupBoxImageDisplay.Controls.Add(this.picProcessed);
            this.groupBoxImageDisplay.Controls.Add(this.lblOriginal);
            this.groupBoxImageDisplay.Controls.Add(this.lblProcessed);
            this.groupBoxImageDisplay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxImageDisplay.Location = new System.Drawing.Point(840, 60);
            this.groupBoxImageDisplay.Name = "groupBoxImageDisplay";
            this.groupBoxImageDisplay.Size = new System.Drawing.Size(332, 500);
            this.groupBoxImageDisplay.TabIndex = 4;
            this.groupBoxImageDisplay.TabStop = false;
            this.groupBoxImageDisplay.Text = "Görüntü Önizleme";
            // 
            // lblOriginal
            // 
            this.lblOriginal.AutoSize = true;
            this.lblOriginal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOriginal.Location = new System.Drawing.Point(15, 25);
            this.lblOriginal.Name = "lblOriginal";
            this.lblOriginal.Size = new System.Drawing.Size(50, 15);
            this.lblOriginal.TabIndex = 0;
            this.lblOriginal.Text = "Orijinal";
            // 
            // picOriginal
            // 
            this.picOriginal.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.picOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOriginal.Location = new System.Drawing.Point(15, 45);
            this.picOriginal.Name = "picOriginal";
            this.picOriginal.Size = new System.Drawing.Size(300, 200);
            this.picOriginal.TabIndex = 1;
            this.picOriginal.TabStop = false;
            // 
            // lblProcessed
            // 
            this.lblProcessed.AutoSize = true;
            this.lblProcessed.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProcessed.Location = new System.Drawing.Point(15, 260);
            this.lblProcessed.Name = "lblProcessed";
            this.lblProcessed.Size = new System.Drawing.Size(58, 15);
            this.lblProcessed.TabIndex = 2;
            this.lblProcessed.Text = "İşlenmiş";
            // 
            // picProcessed
            // 
            this.picProcessed.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.picProcessed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProcessed.Location = new System.Drawing.Point(15, 280);
            this.picProcessed.Name = "picProcessed";
            this.picProcessed.Size = new System.Drawing.Size(300, 200);
            this.picProcessed.TabIndex = 3;
            this.picProcessed.TabStop = false;
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.White;
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtOutput.Location = new System.Drawing.Point(12, 570);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(1160, 150);
            this.txtOutput.TabIndex = 5;
            this.txtOutput.WordWrap = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(1040, 730);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(132, 35);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "🗑️ Temizle";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(1184, 776);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.groupBoxImageDisplay);
            this.Controls.Add(this.groupBoxImage);
            this.Controls.Add(this.groupBoxML);
            this.Controls.Add(this.groupBoxMatrix);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Processing & ML - Advanced Edition";
            this.groupBoxMatrix.ResumeLayout(false);
            this.groupBoxML.ResumeLayout(false);
            this.groupBoxImage.ResumeLayout(false);
            this.groupBoxImageDisplay.ResumeLayout(false);
            this.groupBoxImageDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

