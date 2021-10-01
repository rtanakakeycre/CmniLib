using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using UtilLib;

namespace PipeLib
{
    // ポートタイプ
    public enum ePORT_TYPE
    {
        [Description("シリアル")] SER,
        [Description("パイプ")] PIPE,
        [Description("USB")] USB,
        LMT
    };

    public enum eSER_BRT
    {
        _300,
        _600,
        _1200,
        _2400,
        _4800,
        _9600,
        _14400,
        _19200,
        _28800,
        _38400,
        _57600,
        _115200,
        _230400,
        _208333,
        _312500,
        LMT
    };

    // ハンドシェイク
    public enum eSER_HSK
    {
        _300,
        _600,
        _1200,
        _2400,
        _4800,
        _9600,
        _14400,
        _19200,
        _28800,
        _38400,
        _57600,
        _115200,
        _230400,
        _208333,
        _312500,
        LMT
    };

    public enum eUSB_BRT
    {
        _9600,
        _76800,
        LMT
    };

    public class PipeCom
    {
        public const int RCV_CMD_LEN_MAX = 32;
        public const int SRI_RCV_DATA_SIZE = 256;
        public const int MMF_SIZE = 16384;
        public const int DFL_LOG_GAME = 10;
        public const int EASY_CHK_BUF_MAX = 1024;
        public const int EASY_CHK_DATA_MAX = 1024;

        static public string[] m_atxPortName;        // ポート
        static public int[] m_adtSerBrt;        // シリアルボーレート
        static public int[] m_adtDataBit;        // データ長
        static public sTX_INT[] m_asParity;        // パリティビット
        static public sTX_INT[] m_asStopBit;        // ストップビット
        static public sTX_INT[] m_asFlwCtl;        // フロー制御
        static public int[] m_adtUsbBrt;        // USBボーレート

        static PipeCom()
        {
            //! 利用可能なシリアルポート名の配列を取得する.
            m_adtDataBit = new int[] {
                5,
                6,
                7,
                8,
            };

            // シリアルボーレート設定
            m_adtSerBrt = new int[] {
                300,
                600,
                1200,
                2400,
                4800,
                9600,
                14400,
                19200,
                28800,
                38400,
                57600,
                115200,
                230400,
                208333,
                312500,
            };

            // USBボーレート設定
            m_adtUsbBrt = new int[] {
                9600,
                76800,
            };

            // フロー制御
            m_asFlwCtl = new sTX_INT[] {
                new sTX_INT("なし", (int)Handshake.None),
                new sTX_INT("XON/XOFF制御", (int)Handshake.XOnXOff),
                new sTX_INT("RTS/CTS制御", (int)Handshake.RequestToSend),
                new sTX_INT("XON/XOFF + RTS/CTS制御", (int)Handshake.RequestToSendXOnXOff),
            };

            // ストップビット
            m_asStopBit = new sTX_INT[] {
                new sTX_INT("なし", (int)StopBits.None),
                new sTX_INT("1", (int)StopBits.One),
                new sTX_INT("1.5", (int)StopBits.OnePointFive),
                new sTX_INT("2", (int)StopBits.Two),
            };

            // パリティ
            m_asParity = new sTX_INT[] {
                new sTX_INT("なし", (int)Parity.None),
                new sTX_INT("奇数", (int)Parity.Odd),
                new sTX_INT("偶数", (int)Parity.Even),
                new sTX_INT("マーク", (int)Parity.Mark),
                new sTX_INT("スペース", (int)Parity.Space),
            };

        }
    }
}
