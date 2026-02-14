using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace BoardingHouse
{
    public partial class CameraCaptureForm : Form
    {
        private FilterInfoCollection _devices;
        private VideoCaptureDevice _device;
        private Bitmap _currentFrame;
        private readonly object _frameLock = new();

        public string SavedImagePath { get; private set; } = "";

        public CameraCaptureForm()
        {
            InitializeComponent();
        }

        private void CameraCaptureForm_Load(object sender, EventArgs e)
        {
            try
            {
                _devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                cboDevices.Items.Clear();
                foreach (FilterInfo d in _devices)
                    cboDevices.Items.Add(d.Name);

                if (cboDevices.Items.Count > 0)
                    cboDevices.SelectedIndex = 0;
                else
                    MessageBox.Show("No camera device detected.", "Camera",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load camera devices.\n\n{ex.Message}", "Camera",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_devices == null || _devices.Count == 0) return;
            if (cboDevices.SelectedIndex < 0) return;

            StopCamera();

            _device = new VideoCaptureDevice(_devices[cboDevices.SelectedIndex].MonikerString);
            _device.NewFrame += Device_NewFrame;
            _device.Start();
        }

        private void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frameClone = null;
            Bitmap normalizedFrame = null;
            Bitmap previewClone = null;
            Bitmap captureClone = null;

            try
            {
                frameClone = (Bitmap)eventArgs.Frame.Clone();
                if (frameClone.Width <= 0 || frameClone.Height <= 0)
                    return;

                normalizedFrame = NormalizeFrameTo24bpp(frameClone);

                // keep copies for preview and capture storage
                previewClone = (Bitmap)normalizedFrame.Clone();
                captureClone = (Bitmap)normalizedFrame.Clone();

                SafeSetPictureBoxImage(pbPreview, previewClone);
                previewClone = null;

                lock (_frameLock)
                {
                    _currentFrame?.Dispose();
                    _currentFrame = captureClone;
                    captureClone = null;
                }

                Debug.WriteLine($"[Camera] Frame OK ({normalizedFrame.Width}x{normalizedFrame.Height}), PixelFormat={normalizedFrame.PixelFormat}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Camera] Frame error: {ex.Message}\n{ex.StackTrace}");
                previewClone?.Dispose();
                captureClone?.Dispose();
            }
            finally
            {
                frameClone?.Dispose();
                normalizedFrame?.Dispose();
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentFrame == null)
                {
                    MessageBox.Show("No camera frame yet. Click Start first.", "Camera",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "profile_images");
                Directory.CreateDirectory(folder);

                string filename = $"owner_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                string path = Path.Combine(folder, filename);

                Bitmap capture;
                lock (_frameLock)
                {
                    if (_currentFrame == null)
                        throw new InvalidOperationException("Capture frame lost.");

                    capture = (Bitmap)_currentFrame.Clone();
                }

                using (capture)
                using (var saveBmp = NormalizeFrameTo24bpp(capture))
                {
                    saveBmp.Save(path, ImageFormat.Jpeg);
                }

                SavedImagePath = path;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture failed.\n\n{ex.Message}", "Camera",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CameraCaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();

            if (pbPreview.Image != null)
            {
                var old = pbPreview.Image;
                pbPreview.Image = null;
                old.Dispose();
            }

            lock (_frameLock)
            {
                _currentFrame?.Dispose();
                _currentFrame = null;
            }
        }

        private void StopCamera()
        {
            if (_device != null)
            {
                try
                {
                    _device.NewFrame -= Device_NewFrame;

                    if (_device.IsRunning)
                    {
                        _device.SignalToStop();
                        _device.WaitForStop();
                    }
                }
                catch { }
                finally
                {
                    _device = null;
                }
            }
        }

        private Bitmap NormalizeFrameTo24bpp(Bitmap source)
        {
            var normalized = new Bitmap(source.Width, source.Height, PixelFormat.Format24bppRgb);
            using var g = Graphics.FromImage(normalized);
            g.DrawImage(source, 0, 0, source.Width, source.Height);
            return normalized;
        }

        private void SafeSetPictureBoxImage(PictureBox pb, Bitmap bmp)
        {
            if (pb == null || bmp == null) return;

            if (pb.InvokeRequired)
            {
                pb.BeginInvoke(new Action(() => SafeSetPictureBoxImage(pb, bmp)));
                return;
            }

            var old = pb.Image;
            pb.Image = bmp;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            old?.Dispose();
        }
    }
}
