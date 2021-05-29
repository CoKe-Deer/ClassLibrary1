using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using mshtml;
//2016年 2月和3月将httpweb的方法添加和优化了
//2016年 3月29日添加了Time类，计时器方法
namespace ClassLibrary1
{
    public class Class1
    {
        private string 高级_访问网页_对象(string Url, int 访问类型, string body, string Cookie, string 代理, string 完整协议头, string 引用网页, string 超时值, string 返回Cookie, string 返回协议头, string 伪装的浏览器, string 伪装的协议头, bool 并联Cookie)//无视这个吧
        {



            DxComObject HTTP = new DxComObject("WinHttp.WinHttpRequest.5.1");
            string b = Url.Substring(0, 4);
            if (b != "http")
            {
                if (b != "file")
                {
                    Url = "http://" + Url;
                }
            }
            //访问
            //string Url = "http://www.ip138.com/";
            object[] args1 = new object[3];//指定参数
            if (访问类型 == 0)
            {
                args1[0] = "GET";
            }
            else
            {
                args1[0] = "POST";
            }
            args1[1] = Url; args1[2] = false;
            HTTP.DoMethod("Open", args1);

            //代理        注明：123.123.123.123:8080
            args1 = new object[2];
            args1[0] = 2; args1[1] = 代理;
            HTTP.DoMethod("SetProxy", args1);

            //引用访问
            args1 = new object[2];
            args1[0] = "Referer"; args1[1] = 引用网页;
            HTTP.DoMethod("SetRequestHeader", args1);

            //协议头
            args1 = new object[2];
            args1[0] = "Content-Type";
            args1[1] = "application/x-www-form-urlencoded";
            HTTP.DoMethod("SetRequestHeader", args1);

            //Content-Length_提交长度  注明：暂时不启用,如实在搞不懂哪里出错,再来修改此处方法
            //args1 = new object [2];
            //args1 [0] = "Content-Length"; args1 [1] = 1;
            //HTTP.DoMethod("SetRequestHeader" , args1);

            //Host
            //args1 = new object [2]; 注明：暂时不启用,如实在搞不懂哪里出错,再来修改此处方法
            //args1 [0] = "Host"; args1 [1] = Url;
            //HTTP.DoMethod("SetRequestHeader" , args1);

            //伪装的浏览器
            args1 = new object[2];
            args1[0] = "User-Agent"; args1[1] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1";
            HTTP.DoMethod("SetRequestHeader", args1);

            //Cookie
            args1 = new object[2];
            args1[0] = "Cookie"; args1[1] = " ";
            HTTP.DoMethod("SetRequestHeader", args1);

            //超时
            args1 = new object[4];
            for (int a = 0; a < 4; a++)
            {
                args1[a] = 3000;
            }
            HTTP.DoMethod("SetTimeouts", args1);

            //提交
            args1 = new object[1];
            args1[0] = "";
            HTTP.DoMethod("Send", args1);
            string Resp = Encoding.Default.GetString((byte[])HTTP["ResponseBody"]);
            return Resp;
        }

    }

}



/// <summary>
/// WinHttp.WinHttpRequest.5.1类
/// </summary>
public class WinHttp //WinHttp.WinHttpRequest.5.1类
{
    public int 开始()
    {
        int a = API.CoInitialize(0);
        HTTP = new DxComObject("WinHttp.WinHttpRequest.5.1");
        return a;//创建对象之前使用这个API就可以多线程
    }
    public WinHttp()
    {

        //MessageBox.Show("00");
    }

    public DxComObject HTTP;
    object[] args;

    public int 结束()
    {
        return API.CoUninitialize();
    }

    public int 高级_设置访问(string Url, int 访问类型)//卧槽一定要有返回类型
    {

        string b = Url.Substring(0, 4);
        if (b != "http")
        {
            if (b != "file")
            {
                Url = "http://" + Url;
            }
        }
        args = new object[3];//指定参数
        if (访问类型 == 0)
        {
            args[0] = "GET";

        }
        else
        {
            args[0] = "POST";
        }
        args[1] = Url; args[2] = false;
        HTTP.DoMethod("Open", args);
        return 0;//草泥马的C#

    }

    public int 高级_设置Accept(string Accept)//1.2.3.4:8080
    {
        args = new object[2];
        args[0] = "Accept"; args[1] = Accept;
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }
    public int 高级_设置Accept_Language(string Accept_Language)
    {
        args = new object[2];
        args[0] = "Accept-Language"; args[1] = Accept_Language;
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }
    public int 高级_设置Accept_Encoding(string Accept_Encoding)
    {
        args = new object[2];
        args[0] = "Accept-Encoding"; args[1] = Accept_Encoding;
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }
    public int 高级_设置Connection(string Connection)
    {
        args = new object[2];
        args[0] = "Connection"; args[1] = Connection;
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }
    public int 高级_设置代理(string 代理)//1.2.3.4:8080
    {
        args = new object[2];
        args[0] = 2; args[1] = 代理;
        HTTP.DoMethod("SetProxy", args);
        return 0;
    }

    public int 高级_引用网页(string Referer)//Referer头
    {
        args = new object[2];
        args[0] = "Referer"; args[1] = Referer;
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }

    public int 高级_伪装协议头(string Content_Type)
    {
        args = new object[2];
        args[0] = "Content-Type";
        if (Content_Type == "")
        {
            args[1] = "application/x-www-form-urlencoded";
        }
        else
        {
            args[1] = Content_Type;
        }
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }//application/x-www-form-urlencoded

    public int 高级_伪装浏览器(string User_Agent)//Mozilla/5.0
    {
        args = new object[2];
        args[0] = "User-Agent"; args[1] = User_Agent;
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }

    public int 高级_设置Cookie(string Cookie)//好像设置空也要“ ” 
    {
        args = new object[2];
        args[0] = "Cookie"; args[1] = Cookie;
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }

    public int 高级_超时值(int Time)//毫秒
    {
        args = new object[4];
        if (Time == 0)
        {
            for (int a = 0; a < 4; a++)
            {
                args[a] = 3000;
            }
        }
        else
        {

            for (int a = 0; a < 4; a++)
            {
                args[a] = Time;
            }
        }
        HTTP.DoMethod("SetTimeouts", args);
        return 0;
    }

    public string 高级_提交数据(String body, bool 字节集)
    {
        byte[] body1;

        args = new object[1];
        if (字节集)
        {
            body1 = System.Text.Encoding.Default.GetBytes(body);
            args[0] = body1;
        }
        else
        {
            args[0] = body;

        }
        HTTP.DoMethod("Send", args);
        string Resp = "";
        try
        {
            Resp = Encoding.Default.GetString((byte[])HTTP["ResponseBody"]);
        }
        catch
        {
            return "";
        }
        return Resp;
    }//返回网页数据
    /// <summary>
    /// 取文本中间
    /// </summary>
    /// <param name="str"></param>
    /// <param name="leftstr"></param>
    /// <param name="rightstr"></param>
    /// <returns></returns>
    public string Between(string str, string leftstr, string rightstr)//取中间文本_子
    {
        int i = str.IndexOf(leftstr) + leftstr.Length;
        string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
        return temp;
    }


    public string 高级_返回信息(ref string 返回cookie, ref string 返回协议头, bool 并联)//只有在返回cookie有值的时候才返回 = 需要的时候才处理
    {
        string strig = (string)HTTP["GetAllResponseHeaders"];
        if (返回cookie != "" && strig != null)
        {
            if (并联 == false)
            {
                返回cookie = "";
                返回协议头 = "";
            }
            //char [ ] separetor = { '\n','' };
            string[] str = strig.Split("\r\n".ToCharArray());
            string ak47;
            foreach (string word in str)
            {
                ak47 = word;
                if (返回协议头 == "")
                {
                    if (ak47.IndexOf("Location:") != -1)
                    {
                        返回协议头 = ak47.Replace("Location:", "").Trim();
                    }
                }
                if (ak47.IndexOf("Set-Cookie") != -1)
                {
                    //返回cookie ＝ 返回cookie + Between(ak47,"Set-Cookie:",";").Trim()+";";//C#你又怎么了？
                    返回cookie = 返回cookie + Between(ak47, "Set-Cookie:", ";").Trim() + ";";
                }

            }

        }
        //string Resp = Encoding.Default.GetString((byte [ ])HTTP ["ResponseBody"]);
        API.CoUninitialize();
        return strig;

    }

    public string 快速_对象读源码和头(string Url, ref string 协议头)//只有在协议头有值的时候才返回 = 需要的时候才方法
    {
        string b = Url.Substring(0, 4);
        if (b != "http")
        {
            if (b != "file")
            {
                Url = "http://" + Url;
            }
        }

        //DxComObject HTTP = new DxComObject("WinHttp.WinHttpRequest.5.1");
        //访问
        //object [ ] args = new object [3];
        args = new object[3];
        args[0] = "GET"; args[1] = Url; args[2] = false;
        HTTP.DoMethod("Open", args);

        //提交
        args = new object[1];
        args[0] = "";
        HTTP.DoMethod("Send", args);

        //返回
        string Resp = Encoding.Default.GetString((byte[])HTTP["ResponseBody"]);
        if (协议头 != "")
        {
            协议头 = (string)HTTP["GetAllResponseHeaders"];
        }


        return Resp;
    }

    public int 备用_Host(string host)//Host头
    {
        args = new object[2];     //; 注明：暂时不启用,如实在搞不懂哪里出错,再来修改此处方法
        args[0] = "Host"; args[1] = host;
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }

    public int 备用_Content_Length(string body1)//Content-Length头
    {
        args = new object[2];     //; 注明：暂时不启用,如实在搞不懂哪里出错,再来修改此处方法
        args[0] = "Content-Length"; args[1] = body1.Length;
        HTTP.DoMethod("SetRequestHeader", args);
        return 0;
    }

    public int SetRequestHeader(object[] a)//辅助方法
    {
        HTTP.DoMethod("SetRequestHeader", a);
        return 0;
    }

    public int Option()
    {
        args = new object[2];     //; 注明：暂时不启用,如实在搞不懂哪里出错,再来修改此处方法
        args[0] = "6"; args[1] = "0";
        HTTP.DoMethod("Option", args);
        return 0;
    }
}



public class API
{
    //public static extern int mciSendString(string lpstrCommand , string lpstrReturnstring , int uReturnLength , int hwndCallback);

    [DllImport("ole32.dll", EntryPoint = "CoInitialize")]  //初始化对象开始

    public static extern int CoInitialize(int a);

    [DllImport("ole32.dll", EntryPoint = "CoUninitialize")]//初始化对象结束

    public static extern int CoUninitialize();
    /// <summary>
    /// 提升权限
    /// </summary>
    /// <param name="a">需要的权限，以SE_开头的常量，比如</param>
    /// <param name="b">如果为真就是打开相应权限，如果为假则是关闭相</param>
    /// <param name="c">如果为真则仅提升当前线程权限，否则提升</param>
    /// <param name="d">输出原来相应权限的状态（打开 | 关闭）</param>
    /// <returns></returns>
    [DllImport("ntdll.dll", EntryPoint = "RtlAdjustPrivilege")]//提升权限

    public static extern bool RtlAdjustPrivilege(int a, bool b, int c, ref int d);
    /// <summary>
    /// 打开进程权限句柄
    /// </summary>
    /// <param name="a">权限标志,常量</param>
    /// <param name="b">是否继承</param>
    /// <param name="c">进程标识符</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]//打开进程句柄

    public static extern int OpenProcess(int a, int b, int c);
    /// <summary>
    /// 申请远程内存
    /// </summary>
    /// <param name="a">权限句柄</param>
    /// <param name="b">首地址</param>
    /// <param name="c">内存大小</param>
    /// <param name="d">flAllocationType 4096 | 8192</param>
    /// <param name="f">flProtect</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "VirtualAllocEx")]//申请内存

    public static extern int VirtualAllocEx(int a, int b, int c, int d, int f);
    /// <summary>
    /// 写远程内存
    /// </summary>
    /// <param name="hProcess">进程句柄</param>
    /// <param name="lpBaseAddress">内存地址</param>
    /// <param name="lpBuffer">写入内容</param>
    /// <param name="nSize">内存大小</param>
    /// <param name="lpNumberOfBytesWritten">0</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]

    public static extern int WriteProcessMemory_string(int hProcess, int lpBaseAddress, string lpBuffer, int nSize, int lpNumberOfBytesWritten);
    /// <summary>
    /// 加载dll
    /// </summary>
    /// <param name="a">路径</param>
    /// <returns>返回模块句柄</returns>
    [DllImport("kernel32.dll", EntryPoint = "LoadLibraryA")]

    public static extern int LoadLibraryA(string a);
    /// <summary>
    /// 获取函数地址
    /// </summary>
    /// <param name="a">模块句柄</param>
    /// <param name="b">函数名称</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]

    public static extern int GetProcAddress(int a, string b);
    /// <summary>
    /// 创建远程线程
    /// </summary>
    /// <param name="a">进程句柄</param>
    /// <param name="b">线程安全描述字，指向SECURITY_ATTRIBUTES结构的指针</param>
    /// <param name="c">线程栈大小，以字节表示</param>
    /// <param name="d">一个LPTHREAD_START_ROUTINE类型的指针，指向在远程进程中执行的函数地址</param>
    /// <param name="e">传入参数</param>
    /// <param name="f">创建线程的其它标志</param>
    /// <param name="g">线程身份标志，如果为NULL</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "CreateRemoteThread")]

    public static extern int CreateRemoteThread(int a, int b, int c, int d, int e, int f, int g);
    /// <summary>
    /// 关闭内核句柄
    /// </summary>
    /// <param name="a">内核句柄</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
    public static extern int CloseHandle(int a);
    /// <summary>
    /// 关闭模块句柄
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
    public static extern int FreeLibrary(int a);
    /// <summary>
    /// 释放内存
    /// </summary>
    /// <param name="a">进程句柄</param>
    /// <param name="b">内存地址</param>
    /// <param name="c">内存大小</param>
    /// <param name="d">MEM_DECOMMIT0x4000 16384D这种试 仅标示 内存空间不可用，内存页还将存在。MEM_RELEASE0x8000 32768D这种方式 很彻底，完全回收。</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "VirtualFreeEx")]
    public static extern int VirtualFreeEx(int a, int b, int c, int d);
    /// <summary>
    /// 强制终止线程
    /// </summary>
    /// <param name="a">线程句柄</param>
    /// <param name="b">退出代码</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "TerminateThread")]
    public static extern int TerminateThread(int a, int b);
    /// <summary>
    /// 返回错误信息
    /// </summary>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "GetLastError")]
    public static extern int GetLastError();
    /// <summary>
    /// 指针到整数
    /// </summary>
    /// <param name="a">变量</param>
    /// <param name="b">指针</param>
    /// <param name="c">长度</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
    public static extern int RtlMoveMemory_int(ref int a, ref int b, int c);
    /// <summary>
    /// 计算整数型的大小
    /// </summary>
    /// <param name="a">变量</param>
    /// <returns>返回大小</returns>
    [DllImport("kernel32.dll", EntryPoint = "LocalSize")]
    public static extern int LocalSize_int(int a);
    /// <summary>
    /// 写内存整数型
    /// </summary>
    /// <param name="a">进程句柄</param>
    /// <param name="b">10进制内存地址</param>
    /// <param name="c">数值内容</param>
    /// <param name="d">数据长度</param>
    /// <param name="e">实际长度</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
    public static extern int WriteProcessMemory_整数型(int a, int b, ref int c, int d, int e);
    /// <summary>
    /// 读内存整数型
    /// </summary>
    /// <param name="a">进程句柄</param>
    /// <param name="b">10进制内存地址</param>
    /// <param name="c">返回变量内存</param>
    /// <param name="d">要传送的字节数</param>
    /// <param name="e">实际传送的字节数</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
    public static extern int ReadProcessMemory_整数型(int a, int b, ref int c, int d, int e);
}


public class _163邮箱
{
    public static string _163提交数据 = "var=%3C%3Fxml%20version%3D%221.0%22%3F%3E%3Cobject%3E%3Cstring%20name%3D%22id%22%3Ec%3A1365022018971%3C%2Fstring%3E%3Cobject%20name%3D%22attrs%22%3E%3Cstring%20name%3D%22account%22%3E%22%E6%88%91%E6%98%AF%E5%A7%93%E5%90%8D%22%26lt%3By827315601%40163.com%26gt%3B%3C%2Fstring%3E%3Cboolean%20name%3D%22showOneRcpt%22%3Efalse%3C%2Fboolean%3E%3Carray%20name%3D%22to%22%3E%3Cstring%3E827315601%40qq.com%3C%2Fstring%3E%3C%2Farray%3E%3Carray%20name%3D%22cc%22%2F%3E%3Carray%20name%3D%22bcc%22%2F%3E%3Cstring%20name%3D%22subject%22%3E%E6%88%91%E6%98%AF%E4%B8%BB%E9%A2%98%3C%2Fstring%3E%3Cboolean%20name%3D%22isHtml%22%3Etrue%3C%2Fboolean%3E%3Cstring%20name%3D%22content%22%3E%26lt%3Bdiv%20style%3D'line-height%3A1.7%3Bcolor%3A%23000000%3Bfont-size%3A14px%3Bfont-family%3Aarial'%26gt%3B%26lt%3Bdiv%26gt%3B%E6%88%91%E6%98%AF%E6%AD%A3%E6%96%87%26lt%3B%2Fdiv%26gt%3B%26lt%3B%2Fdiv%26gt%3B%3C%2Fstring%3E%3Cint%20name%3D%22priority%22%3E3%3C%2Fint%3E%3Cboolean%20name%3D%22requestReadReceipt%22%3Efalse%3C%2Fboolean%3E%3Cboolean%20name%3D%22saveSentCopy%22%3Etrue%3C%2Fboolean%3E%3Cstring%20name%3D%22charset%22%3EGBK%3C%2Fstring%3E%3C%2Fobject%3E%3Cboolean%20name%3D%22returnInfo%22%3Efalse%3C%2Fboolean%3E%3Cstring%20name%3D%22action%22%3Edeliver%3C%2Fstring%3E%3Cint%20name%3D%22saveTextThreshold%22%3E1048576%3C%2Fint%3E%3C%2Fobject%3E";
    public WinHttp HTTP = new WinHttp();
    //private string Coremail;
    //private string Coremil1;
    private string Sid;
    private string Cookie = "0";
    private string _163邮件信息;
    public string 登录(string user, string password)
    {
        string 提交地址, 提交数据, Head;
        提交地址 = "https://ssl.mail.163.com/entry/coremail/fcg/ntesdoor2?df=webmail163&from=web&funcid=loginone&iframe=1&language=-1&net=c&passtype=1&product=mail163&race=67_68_224_gz&style=-1&uid={User}@163.com";
        提交数据 = "savelogin=0&url2=http%3A%2F%2Fmail.163.com%2Ferrorpage%2Ferr_163.htm&username={User}&password={Pass}";
        提交地址 = 提交地址.Replace("{User}", user);
        提交数据 = 提交数据.Replace("{User}", user);
        提交数据 = 提交数据.Replace("{Pass}", password);
        HTTP.开始();
        HTTP.高级_设置访问(提交地址, 1);
        //HTTP.高级_设置代理("217.13.118.93:80");
        HTTP.高级_伪装协议头("");
        HTTP.高级_伪装浏览器("Mozilla/5.0");
        string b = "";
        string ret = HTTP.高级_提交数据(提交数据, false);
        Head = HTTP.高级_返回信息(ref  Cookie, ref  b, false);
        Sid = HTTP.Between(ret, "sid=", "\";");
        //Coremail = HTTP.Between(Head , "Coremail=" , "%" + Sid);
        //Coremil1 = HTTP.Between(Head , Coremail + "%" , ".mail.163.com");
        //Debug.WriteLine(Cookie);
        _163邮件信息 = _163提交数据;
        _163邮件信息 = _163邮件信息.Replace("y827315601", user);
        HTTP.结束();
        return Sid + "\n";
    }
    public int 发邮件(string 收件人, string 主题, string 正文, string 使用姓名, int 次数)
    {
        if (Sid == "" & Cookie == "" & 次数 == 0)
        {
            return 0;
        }
        string 提交地址, ret;
        int 成功次数 = 0;
        提交地址 = "http://cwebmail.mail.163.com/js5/s?sid=aDBbvxCCrIBIjbhXuWCCtkrmWvCwMHoC&func=mbox:compose&from=nav&action=goCompose&cl_send=1&l=compose&action=deliver";
        提交地址 = 提交地址.Replace("aDBbvxCCrIBIjbhXuWCCtkrmWvCwMHoC", Sid);
        //提交数据 = Class1112._163提交数据;
        _163邮件信息 = _163邮件信息.Replace("%E6%88%91%E6%98%AF%E5%A7%93%E5%90%8D", 使用姓名);
        _163邮件信息 = _163邮件信息.Replace("%E6%88%91%E6%98%AF%E6%AD%A3%E6%96%87", 正文);
        _163邮件信息 = _163邮件信息.Replace("%E6%88%91%E6%98%AF%E4%B8%BB%E9%A2%98", 主题);
        _163邮件信息 = _163邮件信息.Replace("827315601%40qq.com", 收件人);
        _163邮件信息 = _163邮件信息.Replace("%E6%88%91%E6%98%AF%E5%A7%93%E5%90%8D", 使用姓名);
        HTTP.开始();
        HTTP.高级_设置访问(提交地址, 1);
        HTTP.高级_设置Cookie(Cookie);
        HTTP.高级_伪装协议头("");
        while (次数 > 0)
        {
            ret = HTTP.高级_提交数据(_163邮件信息, false);
            if (ret.IndexOf("S_OK") != -1)
            {
                成功次数++;
            }
            次数--;
        }
        Cookie = "";
        _163邮件信息 = "";
        Sid = "";
        return 成功次数;
    }

}

public class s_文本操作
{
    public static string RandomText(int Qun)
    {
        string g = "";
        byte[] array = new byte[1];
        Random a = new Random();
        for (; Qun > 0; Qun--)
        {
            array[0] = (byte)(Convert.ToInt32(a.Next(65, 90)));
            g = g + System.Text.Encoding.Default.GetString(array);
        }
        return g;
    }
    /// <summary>
    /// 取文本中间的内容
    /// </summary>
    /// <param name="str"></param>
    /// <param name="leftstr"></param>
    /// <param name="rightstr"></param>
    /// <returns></returns>
    public static string Between(string str, string leftstr, string rightstr)//取中间文本_子
    {

        try
        {
            int i = str.IndexOf(leftstr);
            i = i + leftstr.Length;
            string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
            return temp;
        }
        catch
        {
            return "";
        }

    }
    public static string GetRepeat(string str, int times)
    {
        string ret = "";
        while (times > 0)
        {
            ret = ret + str;
            times--;
        }
        return ret;
    }
}

public class s_编码转换
{
    /// <summary>
    /// 将一个byte转换为16进制的String
    /// </summary>
    /// <param name="byt"></param>
    /// <returns></returns>
    public static string bytToStrHex(byte byt)
    {
        string strHex = byt.ToString("x");
        return strHex.Length == 1 ? "0" + strHex : strHex;
    }
    /// <summary>
    /// 将一个byte数组转换为16进制的String
    /// </summary>
    /// <param name="bytAry"></param>
    /// <returns></returns>
    public static string bytAryToStrHex(byte[] bytAry)
    {
        string strHex = "";
        for (int i = 0; i < bytAry.Length; i++)
            strHex += bytToStrHex(bytAry[i]);
        return strHex;
    }

    /// <summary>
    /// 将一个gbk编码的String转换为Ucs2编码的String
    /// </summary>
    /// <param name="strGbk"></param>
    /// <returns></returns>
    public static string strGbkToStrUcs2(string strGbk)
    {
        byte[] bytAry = Encoding.BigEndianUnicode.GetBytes(strGbk);
        string a = bytAryToStrHex(bytAry);
        int f = 0;
        string b = "";
        a = @"\u" + a;
        for (uint af = Convert.ToUInt16(a.Length / 4); af > 0; af--)
        {
            b = a.Insert(f, @"\u");

            f = f + 6;
        }
        a = b;
        return a;
    }


    /// <summary>
    /// 将一个Ucs2编码的String转换为byte数组
    /// </summary>
    /// <param name="strUcs2"></param>
    /// <returns></returns>
    public static byte[] strUcs2ToBytAry(string strUcs2)
    {
        int len = strUcs2.Length / 2;
        byte[] bytAry = new byte[len];
        for (int i = 0, j = 0; i < strUcs2.Length; i += 2, j++)
        {
            string strByt = strUcs2.Substring(i, 2);
            bytAry[j] = (byte)strXToDec(strByt, 16);
        }
        return bytAry;
    }

    /// <summary>
    /// 将一个Ucs2编码的String转换为Gbk编码的String
    /// </summary>
    /// <param name="strUcs2"></param>
    /// <returns></returns>
    public static string strUcs2ToStrGbk(string strUcs2)
    {
        strUcs2 = strUcs2.Replace("\n", "");
        strUcs2 = strUcs2.Replace(@"\u", "");
        int len = strUcs2.Length / 2;
        byte[] bytAry = strUcs2ToBytAry(strUcs2);
        return Encoding.BigEndianUnicode.GetString(bytAry, 0, len);
    }


    /// <summary>
    /// 将其他进制的String转换到十进制数字
    /// </summary>
    /// <param name="Num"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int strXToDec(string Num, int n)
    {
        char[] nums = Num.ToCharArray();
        int d = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            string number = nums[i].ToString();
            if (n == 16)
            {
                switch (number.ToUpper())
                {
                    case "A":
                        number = "10";
                        break;
                    case "B":
                        number = "11";
                        break;
                    case "C":
                        number = "12";
                        break;
                    case "D":
                        number = "13";
                        break;
                    case "E":
                        number = "14";
                        break;
                    case "F":
                        number = "15";
                        break;
                }
            }
            Double power = Math.Pow(Convert.ToDouble(n),
                Convert.ToDouble(nums.Length - (i + 1)));
            d = d + Convert.ToInt32(number) * Convert.ToInt32(power);
        }
        return d;
    }




    /// <summary>
    /// 发条哥的USC2
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string unicodetogb(string text)
    {
        System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(text, "\\\\u([\\w]{4})");
        string a = text.Replace("\\u", "");
        char[] arr = new char[mc.Count];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = (char)Convert.ToInt32(a.Substring(i * 4, 4), 16);
        }
        string c = new string(arr);
        return c;
    }



    /// <summary>
    /// 默认被转码的是UTF-8,中文编码是GB18030
    /// </summary>
    /// <param name="strChar">被转码的文本</param>
    /// <param name="Strina">要转码的编码号</param>
    /// <returns>返回转码后的文本</returns>
    public static string GetCharGroup(string strChar, string Strina)//默认被转码的是UTF-8  一般转GB18030
    {
        byte[] buffer1 = Encoding.Default.GetBytes(strChar);
        byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(Strina), buffer1, 0, buffer1.Length);
        string strBuffer = Encoding.Default.GetString(buffer2, 0, buffer2.Length);
        return strBuffer;
    }
    /// <summary>
    /// 返回已解码的WEB的URL编码
    /// </summary>
    /// <param name="strin"></param>
    /// <returns></returns>
    public static string Get_F_WebUrl(string strin)
    {
        return System.Web.HttpUtility.UrlDecode(strin, System.Text.Encoding.GetEncoding("GB2312"));
    }
    /// <summary>
    /// 返回已编码的ASCLL文本
    /// </summary>
    /// <param name="strchar"></param>
    /// <returns></returns>
    public static string GetUrl_ASCLL(string strchar)
    {
        return System.Web.HttpUtility.UrlEncode(strchar, System.Text.Encoding.GetEncoding("GB2312"));
    }
    /// <summary>
    /// 返回已编码的UTF8文本
    /// </summary>
    /// <param name="StrChar">要编码的文本</param>
    /// <returns></returns>
    public static string GetUrl_Utf8(string StrChar)
    {
        return System.Uri.EscapeDataString(StrChar);
    }
}

public class Server_TCP
{
    public static string Base64(string Text)
    {
        byte[] b = System.Text.Encoding.ASCII.GetBytes(Text);
        string s = Convert.ToBase64String(b);
        //Debug.WriteLine(s);
        return s;
    }
    public static string Read1(NetworkStream a)
    {
        //return "";
        byte[] data = new Byte[256];
        String responseData = String.Empty;
        Int32 bytes = a.Read(data, 0, data.Length);
        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        //Debug.WriteLine(responseData);
        //Console.WriteLine(responseData);
        return responseData;
    }
    public static int Write1(NetworkStream F, string Text)
    {

        Text = Text + "\n";

        byte[] szData = System.Text.Encoding.ASCII.GetBytes(Text.ToCharArray());
        F.Write(szData, 0, szData.Length);
        return 0;
    }
    public static string RandomText(int Qun)
    {
        string g = "";
        byte[] array = new byte[1];
        Random a = new Random();
        for (; Qun > 0; Qun--)
        {
            array[0] = (byte)(Convert.ToInt32(a.Next(65, 90)));
            g = g + System.Text.Encoding.Default.GetString(array);
        }
        return g;
    }

}


public class JS_Run
{
    private MSScriptControl.ScriptControlClass JS;
    public int Open()
    {
        JS = new MSScriptControl.ScriptControlClass();
        JS.Language = "javascript";
        return 0;
    }
    public int Go(string code)
    {
        JS.AddCode(code);
        return 0;
    }
    public object Run1(string name, object[] met)
    {
        return JS.Run(name, met);
    }
}


public class JS_Eval
{
    private MSScriptControl.ScriptControlClass JS;
    public int Open()
    {
        JS = new MSScriptControl.ScriptControlClass();
        JS.Language = "JScript";
        return 0;
    }
    public int Go(string code)
    {
        JS.ExecuteStatement(code);
        return 0;
    }
    public object Eval(string Met)
    {
        return JS.Eval(Met);
    }
}
public class Whois_query
{
    public bool whois_zhanzhang(string ym, ref string whoisserver, ref string deskey)
    {
        string uri = "http://whois.chinaz.com/" + ym;
        HttpWeb web = new HttpWeb();
        web.Set_Url(uri, 0);
        web.a.Accept = @"application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-shockwave-flash, */*";
        web.Set_Timeout(2000);
        web.a.UserAgent = @"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; EmbeddedWB 14.52 from: http://www.bsalsa.com/ EmbeddedWB 14.52; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; youxihe.2250; InfoPath.2; InfoPath.3; .NET4.0C; .NET4.0E)";
        web.Set_Accept_Encoding();
        string f = "";
        try
        {
            f = web.Get_html(Encoding.GetEncoding("gb2312"));
        }
        catch
        {
            return false;
        }
        string cookie1 = "", head1 = "";
        CookieCollection cookie = new CookieCollection();
        WebHeaderCollection head = new WebHeaderCollection();
        web.Get_cookie(ref cookie1, ref cookie, ref head1, ref head);
        whoisserver = s_文本操作.Between(f, "whoisServer:\'", "\'");
        deskey = s_文本操作.Between(f, "deskey:\'", "\'");
        return true;
    }
    public bool whois_zhanzhang2(string ym, string whoisServer, string deskey, ref string Registrant, ref string Registrant_Name, ref string Contact_Email, ref string Updated_Time, ref string Creation_Time, ref string Expiration_Time, ref string WhoisMesse)
    {

        string post = string.Format("domain={0}&whoisServer={1}&deskey={2}&isupdate=", ym, whoisServer, deskey);
        string uri = "http://whois.chinaz.com/getDetailInfo.ashx";
        HttpWeb web = new HttpWeb();
        web.Set_Url(uri, 1);
        web.a.Accept = @"application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-shockwave-flash, */*";
        web.a.Referer = @"http://whois.chinaz.com/" + ym;
        web.a.UserAgent = @"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; EmbeddedWB 14.52 from: http://www.bsalsa.com/ EmbeddedWB 14.52; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; youxihe.2250; InfoPath.2; InfoPath.3; .NET4.0C; .NET4.0E)";
        web.Set_Accept_Encoding();
        web.Set_ContentType("application/x-www-form-urlencoded; charset=UTF-8");
        web.Set_body(post, Encoding.UTF8, 3000);
        web.Set_Timeout(31000);
        string f = "";
        try
        {
            f = web.Get_html(Encoding.UTF8);
        }
        catch
        {
            return false;
        }
        string cookie1 = "", head1 = "";
        CookieCollection cookie = new CookieCollection();
        WebHeaderCollection head = new WebHeaderCollection();
        web.Get_cookie(ref cookie1, ref cookie, ref head1, ref head);
        Debug.WriteLine(f);
        return true;
    }
}
/// <summary>
/// IP查询接口
/// </summary>
public class IP_query
{
    /// <summary>
    /// http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=1.1.1.1
    /// </summary>
    /// <param name="cxdz">不支持域名</param>
    /// <param name="wldz"></param>
    /// <returns></returns>
    public bool ip_sina(string cxdz, ref string wldz)
    {
        string uri = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + cxdz;
        HttpWeb web = new HttpWeb();
        web.Set_Url(uri, 0);
        web.a.Accept = @"application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-shockwave-flash, */*";
        web.Set_Timeout(2000);
        web.a.UserAgent = @"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; EmbeddedWB 14.52 from: http://www.bsalsa.com/ EmbeddedWB 14.52; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; youxihe.2250; InfoPath.2; InfoPath.3; .NET4.0C; .NET4.0E)";
        web.Set_Accept_Encoding();
        string f = "";
        try
        {
            f = web.Get_html(Encoding.GetEncoding("gb2312"));
        }
        catch
        {
            return false;
        }
        string cookie1 = "", head1 = "";
        CookieCollection cookie = new CookieCollection();
        WebHeaderCollection head = new WebHeaderCollection();
        web.Get_cookie(ref cookie1, ref cookie, ref head1, ref head);
        wldz = f.Replace("-1", "").Replace("1", "").Replace("\t\t\t\t", "").Replace("\t\t\t", "").Replace("\t", " ");
        return true;
    }

    /// <summary>
    /// http://www.ip138.com
    /// </summary>
    /// <param name="cxdz">支持域名</param>
    /// <param name="IP"></param>
    /// <param name="wldz"></param>
    /// <returns></returns>
    public bool ip_ip138(string cxdz, ref string IP, ref string wldz)
    {
        string uri = "http://www.ip138.com/ips138.asp?ip=" + cxdz + "&action=2";
        HttpWeb web = new HttpWeb();
        web.Set_Url(uri, 0);
        web.a.Accept = @"application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-shockwave-flash, */*";
        web.Set_Timeout(2000);
        web.a.UserAgent = @"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; EmbeddedWB 14.52 from: http://www.bsalsa.com/ EmbeddedWB 14.52; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; youxihe.2250; InfoPath.2; InfoPath.3; .NET4.0C; .NET4.0E)";
        web.Set_Accept_Encoding();
        string f = "";
        try
        {
            f = web.Get_html(Encoding.GetEncoding("gb2312"));
        }
        catch
        {
            return false;
        }
        string cookie1 = "", head1 = "";
        CookieCollection cookie = new CookieCollection();
        WebHeaderCollection head = new WebHeaderCollection();
        web.Get_cookie(ref cookie1, ref cookie, ref head1, ref head);
        string ipd = s_文本操作.Between(f, "<ul class=\"ul1\">", "</ul>");
        ipd = ipd.Replace("<li>", "").Replace("</li>", "\r\n");
        string[] fg = ipd.Split("\r\n".ToCharArray());
        string[] fg2;
        foreach (string a in fg)
        {
            if (a.Length > 1)
            {
                fg2 = a.Split("：".ToCharArray());
                wldz += fg2[1] + "\r\n";
            }
        }

        if ((cxdz.Split(".".ToCharArray())).Length == 4)
        {
            IP = cxdz + "\r\n";
        }
        else { IP = s_文本操作.Between(f, ">> ", "</font>"); }
        return true;
    }
    /// <summary>
    /// http://ip.chinaz.com/?adfwkey=nnc47
    /// </summary>
    /// <param name="cxdz">获取的IP或者域名</param>
    /// <param name="ip"></param>
    /// <param name="szip">数字IP</param>
    /// <param name="wldz">物理地址</param>
    /// <returns></returns>
    public bool ip_zhanzhang(string cxdz, ref string ip, ref string szip, ref string wldz)
    {
        string uri = "http://ip.chinaz.com/?adfwkey=nnc47";

        string post = string.Format("ip={0}", cxdz);
        HttpWeb web = new HttpWeb();
        web.Set_Url(uri, 1);
        web.a.Accept = @"application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-shockwave-flash, */*";
        web.a.Referer = @"http://ip.chinaz.com/?adfwkey=nnc47";
        web.a.UserAgent = @"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; EmbeddedWB 14.52 from: http://www.bsalsa.com/ EmbeddedWB 14.52; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; youxihe.2250; InfoPath.2; InfoPath.3; .NET4.0C; .NET4.0E)";
        web.Set_Accept_Encoding();
        web.Set_ContentType("application/x-www-form-urlencoded");
        web.Set_body(post, Encoding.ASCII, 3000);
        web.Set_Timeout(31000);
        string f = "";
        try
        {
            f = web.Get_html(Encoding.UTF8);
        }
        catch
        {
            return false;
        }
        string cookie1 = "", head1 = "";
        CookieCollection cookie = new CookieCollection();
        WebHeaderCollection head = new WebHeaderCollection();
        web.Get_cookie(ref cookie1, ref cookie, ref head1, ref head);
        //关键部分
        f = s_文本操作.Between(f, "<div class=\"WhoIpWrap jspu\">", "<p class=\"lastsearch");
        //不要的部分
        f = f.Replace("<p class=\"WhwtdWrap bg-blue08 col-gray03\">\r\n                    <span class=\"Whwtdhalf w15-0\">域名/IP</span>\r\n                    <span class=\"Whwtdhalf w15-0\">获取的IP地址</span>\r\n                    <span class=\"Whwtdhalf w15-0\">数字地址</span>\r\n                    <span class=\"Whwtdhalf w50-0\">IP的物理位置</span>\r\n                </p>", "");
        string qm = "<span class=\"Whwtdhalf w15-0\">";
        f = f.Replace(qm + cxdz, "");
        string lsip = "";
        while (f.IndexOf(qm) > -1)
        {
            lsip = s_文本操作.Between(f, qm, "</span>");
            ip += lsip + "\r\n";
            f = f.Replace(qm + lsip, "");

            lsip = s_文本操作.Between(f, qm, "</span>");
            if (lsip.IndexOf("WhwtdWrap bor-b1s col-gray03") == -1)
            {
                szip += lsip + "\r\n";
                f = f.Replace(qm + lsip, "");
            }
            lsip = s_文本操作.Between(f, "Whwtdhalf w50-0\">", "</span>");
            if (lsip.IndexOf("WhwtdWrap bor-b1s col-gray03") > -1) { continue; }
            wldz += lsip + "\r\n";
            f = f.Replace("Whwtdhalf w50-0\">" + lsip, "");
        }
        return true;

    }
}
/// <summary>
/// HttpWebRequest方法提交数据
/// </summary>
public class HttpWeb
{

    public HttpWebRequest a = null;
    /// <summary>
    /// 返回重定向地址
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cookie"></param>
    /// <returns></returns>
    public static string Get_Location(string uri, ref string cookie)
    {
        string ret = "";

        HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
        myHttpWebRequest.AllowAutoRedirect = false;
        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
        ret = myHttpWebResponse.Headers.Get("Location");
        cookie = myHttpWebResponse.Headers.Get("Cookie");
        myHttpWebResponse.Close();
        myHttpWebRequest.Abort();
        return ret;
    }
    /// <summary>
    /// 设置HttpWebRequest访问最大连接数,不宜超过1024
    /// </summary>
    /// <param name="b"></param>
    public static void Set_DefaultConnectionLimit(int b)
    {
        ServicePointManager.DefaultConnectionLimit = b;
    }
    /// <summary>
    /// 快速GET方式访问一个网页
    /// </summary>
    /// <param name="Url"></param>
    /// <param name="cookie"></param>
    /// <param name="Timeout"></param>
    /// <returns></returns>
    public static string HttpWebGET(string Url, string cookie, int Timeout)
    {
        string html = String.Empty;
        HttpWebRequest cnbogs = (HttpWebRequest)System.Net.WebRequest.Create(Url);
        cnbogs.Method = "GET";
        cnbogs.Timeout = Timeout;
        string[] ai = null;
        CookieContainer cc = new CookieContainer();
        ai = cookie.Split(";".ToCharArray());
        foreach (string a1 in ai)
        {
            cc.SetCookies(new Uri(Url), a1);
        }
        cnbogs.CookieContainer = cc;
        HttpWebResponse cnblogsRespone = (HttpWebResponse)cnbogs.GetResponse();

        if (cnblogsRespone != null && cnblogsRespone.StatusCode == HttpStatusCode.OK)
        {
            using (StreamReader sr = new StreamReader(cnblogsRespone.GetResponseStream()))
            {
                html = sr.ReadToEnd();

            }
        }

        cnbogs.Abort();
        return html;

    }

    /// <summary>
    /// 设置访问的网页和类型/第一步
    /// </summary>
    /// <param name="url"></param>
    /// <param name="leix"></param>
    public void Set_Url(string url, int leix)
    {
        a = (HttpWebRequest)System.Net.WebRequest.Create(url);
        if (leix > 0) { a.Method = "POST"; }
        else { a.Method = "GET"; }

    }
    /// <summary>
    /// 设置超时值
    /// </summary>
    /// <param name="Timeout"></param>
    public void Set_Timeout(int Timeout)
    {
        a.Timeout = Timeout;
    }
    /// <summary>
    /// 设置cookie
    /// </summary>
    /// <param name="cookie"></param>
    /// <param name="host"></param>
    public void Set_Cookie(string cookie, string host)
    {
        cookie = cookie.Replace("\r\n", "");
        CookieContainer cc = new CookieContainer();
        cc.MaxCookieSize = 500;
        a.Timeout = 3000;
        string[] ai = null;
        ai = cookie.Split(";".ToCharArray());
        foreach (string a1 in ai)
        {
            cc.SetCookies(new Uri(host), a1);
        }
        a.CookieContainer = cc;

    }
    /// <summary>
    /// 设置是否重定向
    /// </summary>
    /// <param name="b"></param>
    public void Set_AllowAutoRedirect(bool b)
    {
        a.AllowAutoRedirect = b;
    }
    /// <summary>
    /// Accept_Encoding设置为gzip,deflate,并设置DecompressionMethods.GZip压缩格式返回
    /// </summary>
    public void Set_Accept_Encoding()
    {
        //要设置解码
        a.Headers["Accept-Encoding"] = "gzip, deflate";
        a.AutomaticDecompression = DecompressionMethods.GZip;
    }
    /// <summary>
    /// 设置一个协议头
    /// </summary>
    /// <param name="Head">协议头名</param>
    /// <param name="ers">协议头值</param>
    /// <param name="b">是否已存在</param>
    public void Set_Headers(string Head, string ers, bool b)
    {
        if (!b) { a.Headers.Add(Head, ers); }
        else
        {
            a.Headers[Head] = ers;
        }
    }
    /// <summary>
    /// 设置ContentType协议头的值,application/x-www-form-urlencoded
    /// </summary>
    /// <param name="ers"></param>
    public void Set_ContentType(string ers)
    {
        a.ContentType = ers;
    }
    /// <summary>
    /// 设置User-Agent的值,Mozilla/5.0
    /// </summary>
    /// <param name="ers"></param>
    public void Set_User_Agent(string ers)
    {
        a.UserAgent = ers;
    }
    /// <summary>
    /// 设置POST数据以及编码格式、写入超时时间,并设置Content-Length属性的值为提交字符的长度
    /// 请在提交以前设置ContentType的值,一般为application/x-www-form-urlencoded
    /// </summary>
    /// <param name="body"></param>
    /// <param name="n"></param>
    /// <param name="c"></param>
    public void Set_body(string body, Encoding n, int c)
    {

        a.ContentLength = body.Length;
        System.IO.Stream outputStream = a.GetRequestStream();
        outputStream.WriteTimeout = c;
        byte[] bs = n.GetBytes(body);
        outputStream.Write(bs, 0, body.Length);
        outputStream.Close();

    }
    /// <summary>
    /// 设置ContentLength头值
    /// </summary>
    /// <param name="ers"></param>
    public void Set_ContentLength(int ers)
    {
        a.ContentLength = ers;
    }

    public HttpWebResponse k = null;
    /// <summary>
    /// Send提交返回网页数据
    /// </summary>
    /// <returns></returns>
    public string Get_html(Encoding n)
    {
        string html;
        html = "";
        try
        {
            k = (HttpWebResponse)a.GetResponse();
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message);
            throw new Exception(e.Message);

        }
        if (k == null) { return html; }
        while (k.StatusCode != HttpStatusCode.OK)
        {
            Thread.Sleep(1000);
        }
        using (StreamReader sr = new StreamReader(k.GetResponseStream(), n))
        {
            html = sr.ReadToEnd();
            sr.Close();
        }

        return html;
    }
    /// <summary>
    /// 设置Accept头信息
    /// </summary>
    /// <param name="Head"></param>
    public void Set_Accept(string Head)
    {
        a.Accept = Head;
    }
    /// <summary>
    /// 获取cookie和完整协议头,CookieCollection,并调用Abort方法
    /// </summary>
    /// <param name="cookie"></param>
    /// <param name="cookie1"></param>
    /// <param name="Head"></param>
    /// <param name="b"></param>
    public void Get_cookie(ref string cookie, ref CookieCollection cookie1, ref string Head, ref WebHeaderCollection b)
    {

        cookie1 = k.Cookies;
        cookie = k.Headers.Get("Set-Cookie");
        b = k.Headers;
        int v = b.Count;

        for (int f = 0; f < v; f++)
        {
            Head = Head + b.GetKey(f) + ":" + b.Get(f) + "\r\n";
        }
        a.Abort();

    }
    /// <summary>
    /// 手动调用Abort方法
    /// </summary>
    public void Turn_off()
    {
        a.Abort();
    }
}


public class S_Web
{
    /// <summary>
    /// 使用IE浏览器打开一个网页
    /// </summary>
    /// <param name="Url"></param>
    public static void Open(string Url)
    {
        Process ps = new Process();
        string yourURL = Url;
        ps.StartInfo.FileName = "iexplore.exe";
        ps.StartInfo.Arguments = yourURL;
        //或者直接用：
        ps.Start();
    }
}




public class Processye
{
    /// <summary>
    /// 远程调用函数
    /// </summary>
    /// <param name="pid">进程PID标识符</param>
    /// <param name="name">函数名字</param>
    /// <param name="DLL">所在DLL库</param>
    /// <param name="func">要注入的DLL路径</param>
    /// <returns></returns>
    bool YC_HS(int pid, string name, string DLL, string func)
    {
        int hProcess = 0;
        hProcess = API.OpenProcess(2035711, 0, pid);
        if (hProcess == 0) { MessageBox.Show("打开进程失败!"); return false; }
        int len = 0, GClen;
        len = func.Length + 1;
        GClen = API.VirtualAllocEx(hProcess, 0, len, 4096 | 8192, 32);
        if (GClen == 0)
        {
            MessageBox.Show("内存申请失败!");
            API.CloseHandle(hProcess);
            return false;
        }
        int bool1 = 0;
        bool1 = API.WriteProcessMemory_string(hProcess, GClen, func, len, 0);
        if (bool1 == 0)
        {
            MessageBox.Show("写入内存失败!");
            API.VirtualFreeEx(hProcess, GClen, len, 32768);
            API.CloseHandle(hProcess);
            return false;
        }
        int mkjb = 0, mkdz = 0;
        mkjb = API.LoadLibraryA(DLL);
        mkdz = API.GetProcAddress(mkjb, name);
        int xchp;
        xchp = 0;
        int xchp1 = API.CreateRemoteThread(hProcess, 0, 0, mkdz, GClen, 0, 0);

        //Debug.WriteLine(API.RtlMoveMemory_int(ref xchp , ref  xchp1 , API.LocalSize_int(xchp1)));
        if (xchp1 == 0) { MessageBox.Show("创建远程线程失败"); }
        API.FreeLibrary(mkjb);
        API.VirtualFreeEx(hProcess, GClen, len, 32768);
        API.CloseHandle(hProcess);
        API.CloseHandle(xchp);
        return true;

    }
}



/// <summary>
/// Com调用类
/// </summary>
public class DxComObject
{

    private System.Type _ObjType;
    private object ComInstance;
    public DxComObject(string ComName)
    {
        //Class1112.CoInitialize(0);//创建对象之前使用这个API就可以多线程
        //根据COM对象的名称创建COM对象
        _ObjType = System.Type.GetTypeFromProgID(ComName);
        if (_ObjType == null)
            throw new Exception("指定的COM对象名称无效");
        ComInstance = System.Activator.CreateInstance(_ObjType);
    }
    public System.Type ComType
    {
        get { return _ObjType; }
    }
    //执行的函数
    public object DoMethod(string MethodName, object[] args)
    {
        try
        {
            return ComType.InvokeMember(MethodName, System.Reflection.BindingFlags.InvokeMethod, null, ComInstance, args);
        }
        catch
        {
            return null;
        }
    }

    public object DoMethod(string MethodName, object[] args, System.Reflection.ParameterModifier[] ParamMods)
    {
        return ComType.InvokeMember(MethodName, System.Reflection.BindingFlags.InvokeMethod, null, ComInstance, args, ParamMods, null, null);
    }
    //获得属性与设置属性
    /// <summary>
    /// 获得属性与设置属性
    /// </summary>
    /// <param name="propName"></param>
    /// <returns></returns>
    public object this[string propName]
    {

        get
        {
            try
            {
                return _ObjType.InvokeMember(propName, System.Reflection.BindingFlags.GetProperty, null, ComInstance, null);
            }
            catch
            {
                return null;
            }
        }
        set
        {
            try
            {
                _ObjType.InvokeMember(propName, System.Reflection.BindingFlags.SetProperty, null, ComInstance, new object[] { value });
            }
            catch
            {
                return;
            }
        }


    }
    public string ResponseBody(DxComObject faf)//这个方法可以无视
    {
        DxComObject AdoStream = new DxComObject("Adodb.Stream");
        AdoStream["Type"] = 1;
        AdoStream["Mode"] = 3;
        AdoStream.DoMethod("Open", new object[] { });
        AdoStream.DoMethod("Write", new object[1] { faf["ResponseBody"] });
        AdoStream["Position"] = 0;
        AdoStream["Type"] = 2;
        AdoStream["Charset"] = "GB2312";
        return AdoStream["ReadText"].ToString();
    }

}

/// <summary>
/// DOM调用JS
/// </summary>
class DomUserJS
{
    public HTMLDocumentClass Dom = new HTMLDocumentClass();
    public int Open()
    {
        IHTMLDocument2 a = Dom;

        //a [0] = "<HTML><BODY></BODY></HTML>";
        a.write("<HTML><BODY></BODY></HTML>");
        return 0;
    }
    public int Jscode(string code)
    {
        Dom.parentWindow.execScript(code);
        return 0;
    }

    public string UserJS(string JSexpress)
    {
        string ret = "document.all.retjs.innerText=" + JSexpress;
        Dom.body.insertAdjacentHTML("BeforeEnd", "<textarea display:none id=retjs></textarea>");
        Dom.parentWindow.execScript(ret);
        IHTMLElement obj = Dom.getElementById("retjs");
        ret = obj.innerText;
        obj.outerHTML = "";

        return ret;
    }
}
/// <summary>
/// 计时类 不要在程序集变量初始化的时候开始
/// </summary>
public class TimeOuta
{

    Stopwatch sw;
    public TimeOuta(int a) { if (a == 1) { start(); } }
    public void start()
    {
        sw = new Stopwatch();
        sw.Start();
    }
    /// <summary>
    /// 获取已经过去的毫秒
    /// </summary>
    /// <returns></returns>
    public long GetTimeandstop()
    {
        sw.Stop();

        return sw.ElapsedMilliseconds;
    }
    /// <summary>
    /// 获取更精准的计时时间(超过毫秒)
    /// </summary>
    /// <returns></returns>
    public decimal Getverytime()
    {
        sw.Stop();
        return (sw.ElapsedTicks / (decimal)Stopwatch.Frequency);
    }

}
