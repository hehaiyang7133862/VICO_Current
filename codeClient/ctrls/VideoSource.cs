using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;

namespace nsVicoClient.ctrls
{
    class VideoSource
    {
        private static VideoSource instance;

        /// <summary>
        /// AForge视频捕获对象
        /// </summary>
        public VideoCaptureDevice captureAForge = null;
        /// <summary>
        /// 设备列表
        /// </summary>
        private FilterInfoCollection EquipList = null;

        public NewFrameEventHandler NewFarmeEnvent;

        public bool bInitState = false;
        public Size DesiredFrameSize
        {
            set
            {
                captureAForge.DesiredFrameSize = value;
            }
        }
        public bool bIsStop
        {
            get
            {
                return captureAForge.IsRunning;
            }
        }

        private VideoSource()
        {
            //获取视频设备列表
            GetVideoDevices();
            //获取视频设备
            GetDevicePerformance();

            if (captureAForge != null)
            {
                bInitState = true;
                captureAForge.NewFrame += new NewFrameEventHandler(captureAForge_NewFrame);
            }
            else
            {
                bInitState = false;
            }
        }

        /// <summary>
        /// 获取所有链接的USB设备
        /// </summary>
        private void GetVideoDevices()
        {
            EquipList = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        /// <summary>
        /// 获取设备性能
        /// </summary>
        private void GetDevicePerformance()
        {
            if (EquipList.Count != 0)
            {
                captureAForge = new VideoCaptureDevice(EquipList[0].MonikerString);
            }
        }

        public static VideoSource getInstance()
        {
            if (instance == null)
            {
                instance = new VideoSource();
            }

            return instance;
        }


        /// <summary>
        /// 停止视频源
        /// </summary>
        public void Stop()
        {
            if (captureAForge != null)
            {
                if (captureAForge.IsRunning)
                {
                    captureAForge.SignalToStop();
                    captureAForge.WaitForStop();
                }
            }
        }
        /// <summary>
        /// 打开视频源
        /// </summary>
        public void Start()
        {
            if (captureAForge != null)
                if (!captureAForge.IsRunning)
                {
                    captureAForge.Start();
                }
        }

        public void Close()
        {
            if (captureAForge != null)
                if (captureAForge.IsRunning)
                {
                    captureAForge.SignalToStop();
                    captureAForge.WaitForStop();

                    captureAForge = null;
                }
        }

        void captureAForge_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (NewFarmeEnvent != null)
            {
                NewFarmeEnvent(sender, eventArgs);
            }
        }
    }

    /// <summary>
    /// 设备性能结构体
    /// </summary>
    public struct DeviceCapabilityInfo
    {
        public Size FrameSize;
        public int MaxFrameRate;

        public DeviceCapabilityInfo(Size frameSize, int maxFrameRate)
        {
            FrameSize = frameSize;
            MaxFrameRate = maxFrameRate;
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}  {2}fps", FrameSize.Width, FrameSize.Height, MaxFrameRate);
        }
    }
}