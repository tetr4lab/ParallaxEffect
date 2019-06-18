# ParallaxEffect / ジャイロで視差効果 (uGUI、2D)
Component to get parallax effect with gyro  
tags: Unity uGUI Android iOS

# できること

- Unityの2Dプロジェクト、または、3Dプロジェクトでも、常に最前面に張り付くタイプ(Screen Space - Overlay)のuGUIで、スクリプトなしで簡易的な視差効果を実現します。

# 挙動と例

デバイスを傾けると、向きと傾きに応じて、オブジェクトがスライドします。  
同時に、複数のオブジェクトに対して、個別の設定で効果を付与できます。  
![ScreenRecord-_10_46午後_-5月-26_-2019__2.gif](https://qiita-image-store.s3.ap-northeast-1.amazonaws.com/0/365845/c18929c0-b029-7144-91bf-ccb3114282c3.gif) 　 ![inspecter.png](https://qiita-image-store.s3.ap-northeast-1.amazonaws.com/0/365845/13918234-99c7-65c5-0903-d59d619c1ee5.png)


#### 解説

この例では、大きさの異なる4枚の矩形を中心を合わせて配置し、それぞれにコンポーネントをアタッチして、大きいものから順に「Deepness=1～4」で設定しています。(他のパラメータはデフォルトのままです。)  
※この例のシーンがアセットに付属しています。

# 使い方

- プロジェクトにアセットをインポートしてください。
- 対象のオブジェクト(Imageやスプライト)に、"ParallaxEffect"スクリプトをアタッチしてください。
- インスペクタでパラメータを調整してください。
- 通常、Editor上ではジャイロが機能しないので、動作の確認には実機が必要です。

|パラメータ|意味|
|:---|:---|
|ShiftRatio	|傾きに反応する感度です。逆向きにスライドさせる場合は負数を指定します。	|
|Deepness	|感度に乗じられます。	|
|Movable Radius	|スライドを制限する距離を指定します。0を指定すると制限されません。	|
|Return Ratio	|中点に復帰する速度を指定します。0を指定すると自動復帰しなくなります。	|
|Horizontal	|水平方向のスライドについて有効/無効を切り替えます。	|
|Vertical	|垂直方向のスライドについて有効/無効を切り替えます。	|

ParallaxEffect.Reactable = ture; // or false … 全体の有効/無効を切り替えます。
