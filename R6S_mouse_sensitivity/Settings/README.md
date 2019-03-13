
# R6S Mouse Sensitivity Tweak

PC版Rainbow Six Siegeのマウス感度の調節を主な目的としたツールです。

## 概要

GameSettingファイルを読み込み、ユーザーの希望に合わせて感度の調節を行います。
選択したデータはGameSettingファイルへ上書きが可能です。


## 機能

- 感度を変えずにマウスのDPIを調節する。
- ADS感度を変えずに腰だめ感度を調節する。
- MouseSensitivityMultiplierUnitとXFactorAimingを調節する。
- サーバーリージョンの変更



## 対応環境

- Windows10 64bitでのみ確認をしています。


## 使い方

- 【感度を変えずにマウスのDPIを調節する。】  
左側の変更前データを埋めて【計算する】を押して下さい。
右上のボックスに変更可能な値の一覧が表示されますので、希望するDPIを選択して下さい。
選択すると同時に変更後のデータが表示されます。表示されているデータを
GameSettingsファイルに上書きする場合は、右下の【適応】を押して下さい。  
 
- 【ADS感度を変えずに腰だめ感度を調節する。】  
上記の機能と同様の使い方です。  

- 【MouseSensitivityMultiplierUnitとXFactorAimingを調節する。】   

- 【サーバーリージョンの変更】  
メニュー画面の左下で変更が可能です。機能を使う場合は設定でデフォルトの参照場所を設定して下さい。


## その他、注意
・仕組み上、このソフトからマウスのDPIとOSのマウス感度の調節はできません。この2つに関しては手作業で変更されるようにお願いします。  
・OSの感度の値の見付け方  
1. デスクトップ左下の検索ボックスに[マウス]と入力します。[マウス設定を変更する]が表示されるのでクリックします。  
2. [マウス]の設定が表示されますので、右上の[その他のマウスオプション]をクリックします。  
3. [マウスのプロパティ]が表示されます。[ポインターオプション]タブにある[速度]の項目を見ます。  
    ![Alt text](C:\Users\annin\source\repos\R6S_mouse_sensitivity\R6S_mouse_sensitivity\Resources\os_sen_img.png)
## 製作者

[@Annin(Twitter)](https://twitter.com/Annin_game_)