# TLMForwarder

// DLL files are added for each Satellite.<br>
#############<br>
 Attached DLL List<br>
#############<br>

53106.dll	GreenCube	(IO-117)<br>
49069.dll	LEDSAT<br>
59508.dll	KASHIWA<br>
57175.dll	CubeBel-2	(EU11S)<br>
47959.dll	GRBAlpha<br>
58567.dll	HADES-D		(SO-121)<br>

// Sample of NEW Satellite's TLE file which is Wrote yourself as handmade.txt.<br>
####################<br>
Handmade TLE List<br>
<br>
Replace TLE with your List.<br>
But never change title strings include '#' lines.<br>
And TLE List should start under one empty line.<br>

リストをお望みのものに置き換えてください。<br>
但し、この"#"のラインで囲まれたタイトル部分は編集しないでください。<br>
また､リストは"############"の下に一行開けて貼り付けてください。<br>
<br>
####################<br>
<br>
UTMN2<br>
1 57203U 23091AP  24106.55338904  .00010443  00000+0  66907-3 0  9994<br>
2 57203  97.6213 160.0408 0011937 324.4676  35.5756 15.08668119 44104<br>
KILICK-1<br>
1 98908U 98067A   24109.87569424  .00025888  00000-0  45352-3 0  9994<br>
2 98908  51.6387 249.4981 0004825  77.1403  58.5009 15.50400337449349<br>
QMSAT<br>
1 98909U 98067A   24109.87569424  .00025888  00000+0  45352-3 0  9993<br>
2 98909  51.6387 249.4981 0004825  77.1403  58.5009 15.50400337449349<br>
VIOLET<br>
1 59597U 98067WQ  24161.92048320  .00119462  00000-0  13274-2 0  9999<br>
2 59597  51.6381 349.1183 0007316 247.6270 112.3949 15.61984435  8198<br>
Nanozond-1<br>
1 57190U 23091AA  24130.57390210  .00015468  00000-0  90888-3 0  9994<br>
2 57190  97.6141 184.3669 0010261 235.3155 124.7108 15.11740894 47760<br>
HADES-D<br>
1 58567U 23174CY  24183.17961421  .00058899  00000-0  19343-2 0  9998<br>
2 58567  97.4543 258.9627 0013472 177.0274 183.1050 15.31276393 35926<br>
/EX

// バージョンアップと変更点 ( Release Note )<br>
<span style="width:20%;">2024-07-24</span><span style="width:20%;">ver 2.3.0</span><span style="width:60%;">Visual Studio Codeでソース変更（機能変更無し）。<br>.NET runtimeと分離した。（インストールを促すメッセージを表示。)</span>

2024-07-07	ver 2.2.4	衛星を新たに選択した時、画面・ファイルを初期化するよう変更

2024-07-05 	ver 2.2.3	フィルタ無しのテスト用衛星[ZZ/ TEST /ZZ]を選択できるようにした。
						（転送はせずファイル出力をチェックするように使用してください。）

2024-07-03	ver 2.2.2	TLEリストをダイアログで選択できるようにした。
						daily-bulletin.txtについては直接WEB指定でもローカルファイルの
						指定でもOK。また homemade.txtという自作ローカルファイルも指定
						可(サンプル添付)

2024-06-29　ver 2.2.1	ローカルフォルダ（例：Documents）にhomemade.txtの名前で作った
						TLEリストからサテライトリストを作成出来るようにした
			iclude DLL	IO-117(53106), LEDSAT(49069), KASHIWA(59508), CubeBel-2(57175)
						GRBAlpha(47959)

2024-06-26	ver 2.2.0	Pythonの仮想環境を又は実環境を必要としない、コンパイルされた
			post_request.exeを使用したモデルに変更
</dl>

