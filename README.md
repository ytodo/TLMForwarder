# TLMForwarder
<pre>
&lt;LICENSE>
著作権は私、藤堂善春(JE3HCZ)に属しています。ただし、クレジットを継承し、商用には供せず、
変更箇所についても同様のライセンス形態を継承する限り自由に改変、配布することが可能です。
CC BY-NC-SA&nbsp;&nbsp;&nbsp;
<a href="https://creativecommons.org/share-your-work/cclicenses/" target="_blank">ABOUT CC LICENSES</a>

//-------------------------------------------------------------------------------
// DLL files are added for each Satellite.
//-------------------------------------------------------------------------------

###################
 Attached DLL List
###################
47959.dll	GRBAlpha
49069.dll	LEDSAT
53106.dll	GreenCube	(IO-117)
57175.dll	CubeBel-2	(EU11S)
58470.dll	ENSO
58567.dll	HADES-D		(SO-121)
60243.dll	ROBUSTA-3A
60246.dll	CatSat
60954.dll	Sakura

//-------------------------------------------------------------------------------
// Sample of NEW Satellite's TLE file which is Wrote yourself as handmade.txt.
//-------------------------------------------------------------------------------

####################
Handmade TLE List

Replace TLE with your List.
But never change title strings include '#' lines.
And TLE List should start under one empty line.

リストをお望みのものに置き換えてください。
但し、この"#"のラインで囲まれたタイトル部分は編集しないでください。
また､リストは"############"の下に一行開けて貼り付けてください。

####################

UTMN2
1 57203U 23091AP  24106.55338904  .00010443  00000+0  66907-3 0  9994
2 57203  97.6213 160.0408 0011937 324.4676  35.5756 15.08668119 44104
KILICK-1
1 98908U 98067A   24109.87569424  .00025888  00000-0  45352-3 0  9994
2 98908  51.6387 249.4981 0004825  77.1403  58.5009 15.50400337449349
QMSAT
1 98909U 98067A   24109.87569424  .00025888  00000+0  45352-3 0  9993
2 98909  51.6387 249.4981 0004825  77.1403  58.5009 15.50400337449349
VIOLET
1 59597U 98067WQ  24161.92048320  .00119462  00000-0  13274-2 0  9999
2 59597  51.6381 349.1183 0007316 247.6270 112.3949 15.61984435  8198
Nanozond-1
1 57190U 23091AA  24130.57390210  .00015468  00000-0  90888-3 0  9994
2 57190  97.6141 184.3669 0010261 235.3155 124.7108 15.11740894 47760
HADES-D
1 58567U 23174CY  24183.17961421  .00058899  00000-0  19343-2 0  9998
2 58567  97.4543 258.9627 0013472 177.0274 183.1050 15.31276393 35926
/EX

	
//-------------------------------------------------------------------------------
// バージョンアップと変更点 ( Release Note )
//-------------------------------------------------------------------------------
2024-09-30	ver 2.3.5	Digipeater用のグリッド名変更とフィールド幅調整を行った。また各DLLでの
						Digipeater表示用アルゴリズムを見直した。

2024-09-13	ver 2.3.4	前回の異常終了によりTLEファイルが残っている場合起動しないのを修正した。

2024-08-06	ver 2.3.3	FrmInfoを見直し GitHub と SatNOGS へのリンクを設けた。

2024-08-03	ver 2.3.2	ソース見直し。ファイル一部さらに分割。結果、再起動無しで衛星の切替可能にした。

2024-07-31	ver 2.3.1	ソース見直しFrmMain.cs のメソッドを他ファイルに分割
	
2024-07-24	ver 2.3.0	Visual Studio Codeでソース変更（機能変更無し）。.NET runtimeと分離した。
				（インストールを促すメッセージを表示。)
	
2024-07-07	ver 2.2.4	衛星を新たに選択した時、画面・ファイルを初期化するよう変更
	
2024-07-05	ver 2.2.3	フィルタ無しのテスト用衛星[ZZ/ TEST /ZZ]を選択できるようにした。
				（転送はせずファイル出力をチェックするように使用してください。）
	
2024-07-03	ver 2.2.2	TLEリストをダイアログで選択できるようにした。
				daily-bulletin.txtについては直接 WEB 指定でもローカルファイルの指定でもOK。
				また homemade.txtという自作ローカルファイルも指定可(サンプル添付)
	
2024-06-29	ver 2.2.1	ローカルフォルダ（例：Documents）にhomemade.txtの名前で作ったTLEリストから
				サテライトリストを作成出来るようにした
				付属DLL	IO-117(53106), LEDSAT(49069), KASHIWA(59508), CubeBel-2(57175),
					GRBAlpha(47959)
	
2024-06-26	ver 2.2.0	Pythonの仮想環境を又は実環境を必要としない、コンパイルされた post_request.exeを
				使用したモデルに変更
</pre>
