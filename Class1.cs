using System;
using System.IO;
using BilibiliDM_PluginFramework;

namespace XCMusic
{
    public class Class1 : DMPlugin
    {
        public Class1()
        {
            Connected += Class1_Connected;
            Disconnected += Class1_Disconnected;
            ReceivedDanmaku += Class1_ReceivedDanmaku;
            ReceivedRoomCount += Class1_ReceivedRoomCount;
            PluginAuth = "yiktllw";
            PluginName = "XCMusic";
            PluginDesc = "XCMusic的点歌插件";
            PluginCont = "yiktllw@qq.com";
            PluginVer = "v0.0.1";
        }


        private void Class1_ReceivedRoomCount(object sender, ReceivedRoomCountArgs e)
        {
        }

        private void Class1_ReceivedDanmaku(object sender, ReceivedDanmakuArgs e)
        {
            if (e == null || e.Danmaku == null)
            {
                Log("ReceivedDanmakuArgs 或 Danmaku 为空，无法处理。");
                return; // 早期返回
            }

            // 设置文件路径
            string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dirPath = Path.Combine(homeDir, "弹幕姬", "Plugins", "xcmusic");
            string filePath = Path.Combine(dirPath, "songPicker.txt");

            // 确保目录存在
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string danmakuContent = e.Danmaku.CommentText;
            if (string.IsNullOrEmpty(danmakuContent))
            {
                Log("CommentText 为空，无法处理。");
                return; // 早期返回
            }

            string writeContent = "";
            if (danmakuContent.StartsWith("点歌"))
            {
                writeContent = danmakuContent.Substring(2);
                WriteToSongPicker(e.Danmaku.CommentText, filePath);
                AddDM($"成功点歌: {writeContent}", true);
                Log($"成功点歌: {writeContent}");
            }
            else if (danmakuContent.StartsWith("下一首"))
            {
                writeContent = danmakuContent.Substring(3);
                WriteToSongPicker(e.Danmaku.CommentText, filePath);
                AddDM($"成功添加到下一首: {writeContent}", true);
                Log($"成功添加到下一首: {writeContent}");
            }
        }
        private void WriteToSongPicker(string content, string filePath)
        {
            // 写入内容到文件，若文件不存在则创建
            File.AppendAllText(filePath, $"{DateTime.UtcNow}: {content} status:tbd{Environment.NewLine}");
        }
        private void Class1_Disconnected(object sender, DisconnectEvtArgs e)
        {
        }

        private void Class1_Connected(object sender, ConnectedEvtArgs e)
        {
        }

        public override void Admin()
        {
            base.Admin();
            Console.WriteLine("Hello World");
        }

        public override void Stop()
        {
            base.Stop();
            //請勿使用任何阻塞方法
            Console.WriteLine("XCMusic Plugin Stoped!");
        }

        public override void Start()
        {
            base.Start();
            //請勿使用任何阻塞方法
            Console.WriteLine("XCMusic Plugin Started!");
        }
    }
}