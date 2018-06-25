using Emgu.CV;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace facenet_0613
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private Capture cap;
        private bool isProcess = false;

        void button1_Click(object sender, EventArgs e)
        {
            if (cap != null)
            {
                if (isProcess)
                {
                    Application.Idle -= new EventHandler(ProcessFram);
                    button1.Text = "stop!";
                }
                else
                {
                    Application.Idle += new EventHandler(ProcessFram);
                    button1.Text = "start!";
                }
                isProcess = !isProcess;
            }
            else
            {
                try
                {
                    cap = new Emgu.CV.Capture("rtsp://admin:a83123008@192.168.0.69:554/Streaming/Channels/101");
                }
                catch (NullReferenceException expt)
                {
                    MessageBox.Show(expt.Message);
                }
            }
        }

        private void ProcessFram(object sender, EventArgs arg)
        {
            imageBox1.Image = cap.QueryFrame();
        }

        private void imageBox1_Click(object sender, EventArgs e)
        {

        }
        Capture cam;

        private void button2_Click(object sender, EventArgs e)
        {
 
                CvInvoke.UseOpenCL = false;
                cam = new Capture("rtsp://admin:a83123008@192.168.0.69:554/Streaming/Channels/101");   //这里是rtsp地址
                cam.ImageGrabbed += Cam_ImageGrabbed;
                cam.Start();
           
        }
        Mat frame = new Mat();
        private void Cam_ImageGrabbed(object sender, EventArgs e)
        {
            cam.Retrieve(frame, 0);
            pictureBox1.Image = frame.Bitmap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
