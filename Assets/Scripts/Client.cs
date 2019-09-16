using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

/*网络游戏中的所有的的逻辑运算和数据处理，全部是由服务器完成的，客户端只负责绘制
在客户端中能看到自己和其他物体的变化*/

public enum MessageType
{
    IdInfo = 0,
    Register,
    Login,
    CreateInfo,
    TextInfo,
    MoveInfo,
    DestroyInfo,
    AsynInfo,
    Skill,
    RoleName
}

public class ChatClient
{
	public GameObject model;
	public List<string> moveDirArray; //移动点数组

	public ChatClient(GameObject obj)
	{
		model = obj;
		moveDirArray = new List<string> ();
	}
}

public class Client : MonoBehaviour {

	private int id;					  //客户id
	private string IP = "127.0.0.1";  //服务器端的Ip地址
	private int Port = 1000;	      //服务器端口号
	private Socket clientSocket = null; //客户端socket
	private string messPackage=""; //消息包
	private List<string> messPackageArray=new List<string>(); //消息包数组
	private byte[] data; 				//消息数据
	private string inputMessage = "";  //输入的信息，用于发送
	private string ReMessage = "";  //接收到的聊天信息
	private Vector2 mess_scrollPos; //聊天信息滚动试图的位置
	private static Hashtable players_obj=new Hashtable(); //所有玩家数据
	public GameObject player_preb;     //主角预设
	private bool _create=false;			//创建模型
	private bool _destroy=false;		//销毁模型
	private bool _disconnected=false;	//服务器断开
	private string callMessage="";		//回调的消息
    private bool _asynInfo = false;
    private bool _register = false;
    private bool _login = false;
    private bool _roleName = false;
    

	//保持socket消息与主线程同步
	private ManualResetEvent allDone = new ManualResetEvent(false);

	public int GetId(){
		return id;
	}

	public Socket GetSocket(){
		return clientSocket;
	}

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartConnect(); 
    }

	void Update()
	{
        if (_register)
        {
            GameObject.Find("UIRoot").GetComponent<MainMenuUIManager>().OnRegisterResult(callMessage);
            _register = false;
            allDone.Set();
        }

        if (_login)
        {
            GameObject.Find("UIRoot").GetComponent<MainMenuUIManager>().OnLoginResult(callMessage);
            _login = false;
            allDone.Set();
        }

        if (_roleName)
        {
            //GameObject.Find("UIRoot").GetComponent<ChooseNameUI>().OnRoleNameResult(callMessage);
            _roleName = false;
            allDone.Set();
        }

		//只能在主线程中创建销毁
		if (_create==true)
		{
			CreatePlayerModel(callMessage);
			_create=false;
			allDone.Set();
		}

		if (_destroy==true)
		{
			DestroyPlayerModel(callMessage);
			_destroy=false;
			allDone.Set();
		}

		if (_disconnected==true)
		{
			Disconnected();
			_disconnected=false;
			allDone.Set ();
		}


		foreach(DictionaryEntry elem in players_obj)
		{
			int playerID=(int)elem.Key;
			ChatClient player=(ChatClient)elem.Value;

			while (player.moveDirArray.Count>0)
			{
				string dir=player.moveDirArray[0];
				player.moveDirArray.RemoveAt(0);
				
				//((ChatClient)players_obj[playerID]).model.SendMessage("Moving",dir); //效率不太好
				//((ChatClient)players_obj[playerID]).model.GetComponent<CubeController>().Moving(dir);
			}
		}

        if(_asynInfo)
        {
            //string[] tmpArray = callMessage.Split('_');
            //int playerID = int.Parse(tmpArray[1]);
            //Debug.Log("同步玩家模型位置，玩家id:" + tmpArray[1]);

            //Vector3 pos = new Vector3(float.Parse(tmpArray[2]), float.Parse(tmpArray[3]), float.Parse(tmpArray[4]));
            //Quaternion rot = Quaternion.identity;
            //rot.eulerAngles = new Vector3(float.Parse(tmpArray[5]), float.Parse(tmpArray[6]), float.Parse(tmpArray[7]));

            //Transform tr = ((ChatClient)players_obj[playerID]).model.transform;
            //tr.position = pos;//Vector3.Lerp(tr.position, pos,0.5f);
            //tr.rotation = rot;//Quaternion.Lerp(tr.rotation,rot,0.5f);
            //_asynInfo = false;
            //allDone.Set();
        }
	}

	void OnClient()
	{
		IPEndPoint clientipe = (IPEndPoint)clientSocket.LocalEndPoint;
		GUILayout.Label("客户端id:"+id);
		GUILayout.Label("客户端ip地址:"+clientipe.Address);
		//断开连接
		if (GUILayout.Button("断开连接"))
		{
			//断开连接
			Debug.Log("成功的从服务器断开连接");
			Disconnected();
		}
		
		//创建水平方向视图
		GUILayout.BeginHorizontal(GUILayout.Width(300));  
		inputMessage = GUILayout.TextArea(inputMessage); //编辑输入的内容
		
		//发送消息
		if (GUILayout.Button("发送消息"))
		{
			//消息类型+聊天内容
			string message=((int)MessageType.TextInfo).ToString()+"_"+inputMessage;
			Send(message);
		}
		//接收水平方向视图
		GUILayout.EndHorizontal();
		
		
		//创建一个滚动试图，用来显示接收到的聊天信息
		mess_scrollPos = GUILayout.BeginScrollView (mess_scrollPos, GUILayout.Width (300), GUILayout.Height (300));
		//显示玩家聊天信息
		GUILayout.Label (ReMessage);
		GUILayout.EndScrollView ();
	}

	//连接服务器
	void StartConnect()
	{
		//服务器ip地址
		IPAddress ipAddress = IPAddress.Parse (IP);
		IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);
		// Create a TCP/IP socket.     
		clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		// Connect to the remote endpoint.     
		clientSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectServerCallback), clientSocket);
	}
	
	//异步连接的回调函数，连接成功后，调用服务器端的accept函数
	void ConnectServerCallback(IAsyncResult ar)
	{
		try
		{
			// Retrieve the socket from the state object.     
			Socket client = (Socket)ar.AsyncState;
			// Complete the connection.     
			client.EndConnect(ar);
			Debug.Log("成功连接到服务器");
			client.NoDelay=true;

			data = new byte[256];
			//从服务器端获取消息
			client.BeginReceive(data, 0, data.Length, 0, new AsyncCallback(ReceiveCallback), client);

        }
        catch (Exception)
        {
            Debug.Log("服务器未启动");
            Disconnected();
        }
	}
	
	//接受消息的回调函数,用户缓冲区获得socket缓冲区中的数据后调用,接收服务器端消息并非由此函数决定,而是由tcp协义决定
	void ReceiveCallback(IAsyncResult ar)
	{
		Socket client = (Socket)ar.AsyncState;
		// Read data from the client socket.     
		int bytesRead = client.EndReceive(ar);
		if (bytesRead <= 0)
		{
			//服务器已断开
			Debug.Log("服务器断开");
			allDone.Reset();
			_disconnected=true;
			//等待主线程执行
			allDone.WaitOne();
			return;
		}
		else
		{
			//获取消息
			string message=Encoding.ASCII.GetString(data, 0, bytesRead);
			Debug.Log("消息:"+message);

			messPackageArray.Clear();
			//解析包
			AnalyzePackage(message);

			foreach (string mess in messPackageArray)
			{
				string[] tmpArray = mess.Split('_');

                //注册消息
                if ((MessageType)(int.Parse(tmpArray[0]))==MessageType.Register)
                {
                    allDone.Reset();      
                    _register = true;
                    callMessage = mess;
                    //等待主线程执行
                    allDone.WaitOne();
                }
                //登陆消息
                else if ((MessageType)(int.Parse(tmpArray[0])) == MessageType.Login)
                {
                    allDone.Reset();
                    _login = true;
                    callMessage = mess;
                    //等待主线程执行
                    allDone.WaitOne();
                }
                //创建角色消息
                else if ((MessageType)(int.Parse(tmpArray[0])) == MessageType.RoleName)
                {
                    allDone.Reset();
                    _roleName = true;
                    callMessage = mess;
                    //等待主线程执行
                    allDone.WaitOne();
                }
				//设置id
				else if ((MessageType)(int.Parse(tmpArray[0]))==MessageType.IdInfo)
				{
					id=int.Parse(tmpArray[1]);
				}
				//创建模型消息
				else if ((MessageType)(int.Parse(tmpArray[0]))==MessageType.CreateInfo)
				{
					allDone.Reset();
					callMessage=mess;
					_create=true;
					//等待主线程执行
					allDone.WaitOne();
				}
				//聊天消息
				else if ((MessageType)(int.Parse(tmpArray[0]))==MessageType.TextInfo)
				{
					//消息类型+用户id+聊天消息
					if (id==int.Parse(tmpArray[1]))
						ReMessage+="\n"+"发送者为自已id:" + tmpArray[1] + "[" + tmpArray[2] + "]";
					else
						ReMessage+="\n"+"发送者id:" + tmpArray[1] + "[" + tmpArray[2] + "]";
				}
				//移动消息
				else if ((MessageType)(int.Parse(tmpArray[0]))==MessageType.MoveInfo)
				{
					//消息类型+用户id+移动方向
					int playerId=int.Parse(tmpArray[1]);
                    ((ChatClient)players_obj[playerId]).moveDirArray.Add(tmpArray[2]);   
				}
				//销毁某一玩家模型
				else if ((MessageType)(int.Parse(tmpArray[0]))==MessageType.DestroyInfo)
				{
					allDone.Reset();
					callMessage=mess;
					_destroy=true;
					//等待主线程执行
					allDone.WaitOne();
				}
                //同步消息
                else if((MessageType)(int.Parse(tmpArray[0]))==MessageType.AsynInfo)
                {
                    allDone.Reset();
                    callMessage = mess;
                    _asynInfo = true;
                    allDone.WaitOne();
                }
			}
		}

		data = new byte[256];
		//继续从服务器端获取消息
		client.BeginReceive(data, 0, data.Length, 0, new AsyncCallback(ReceiveCallback), client);
	}


	//向服务器端发送消息
	public void Send(string data)
	{
		//加上一个结束符
		data+="#";
		// Convert the string data to byte data using ASCII encoding.     
		byte[] byteData = Encoding.ASCII.GetBytes(data);
		// Begin sending the data to the remote device.     
		clientSocket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), clientSocket);
	}

	//发送消息的回调函数,此函数在socket缓冲区中放入数据后调用,发送消息给服务器端并不由此函数决定,而是由tcp协义决定
	void SendCallback(IAsyncResult ar)
	{
		Socket client = (Socket)ar.AsyncState;
		// Complete sending the data to the remote device.     
		int bytesSent = client.EndSend(ar);
		//Debug.Log("发送 {"+bytesSent+"} 字节数据 到服务器端");
	}
	

	//断开与服务器的连接
	void Disconnected () {
		ReMessage = "";  //归零聊天信息

		if (players_obj.Count > 0)
		{
			Debug.Log ("销毁玩家id:"+ id);
			//销毁所有玩家模型
			foreach (ChatClient value in players_obj.Values) {
				Destroy (value.model);
			}
			players_obj.Clear ();
		}

		clientSocket.Close ();
		clientSocket = null;
	}



	//实例化创建玩家模型
	public GameObject CreatePlayerModel(string Message)
	{
        //string[] tmpArray = Message.Split('_');
        //Debug.Log ("创建一个客户端模型，玩家id:" + tmpArray[1]);
        //Vector3 pos = new Vector3 (float.Parse (tmpArray [2]), float.Parse (tmpArray [3]), float.Parse (tmpArray [4]));
        //Quaternion rot = Quaternion.identity;
        //rot.eulerAngles = new Vector3 (float.Parse (tmpArray [5]), float.Parse (tmpArray [6]), float.Parse (tmpArray [7]));
        //Color col = new Color (float.Parse(tmpArray[8]),float.Parse(tmpArray[9]),float.Parse(tmpArray[10]));
		
        //GameObject obj=(GameObject)Instantiate(player_preb, pos, rot);
        //obj.GetComponent<Renderer>().material.color = col;

        //obj.GetComponent<CubeController>().SetId(int.Parse(tmpArray[1]));

        //players_obj.Add (int.Parse(tmpArray[1]), new ChatClient(obj));

        //return obj;
        return null;
	}


	//销毁玩家模型
	void DestroyPlayerModel(string Message)
	{
		string[] tmpArray = Message.Split('_');
		int playerId = int.Parse (tmpArray [1]);
		Debug.Log ("销毁一个客户端模型，玩家id:" + playerId);
		Destroy (((ChatClient)players_obj[playerId]).model);
		players_obj.Remove (playerId);
	}
	


	//解析包的函数
	void AnalyzePackage(string message)
	{
		messPackage+=message;//"1_1#3_2_2_2#"
		//#作为一条消息的结尾
		int index=-1;
		while((index=messPackage.IndexOf("#"))!=-1)
		{
			messPackageArray.Add(messPackage.Substring(0,index));
			messPackage=messPackage.Substring(index+1);

		}
	}


	void OnDestroy()
	{
		if (clientSocket!=null)
		{
			Debug.Log ("失去连接到服务器");
			//断开连接
			Disconnected();
		}
	}
	
}
