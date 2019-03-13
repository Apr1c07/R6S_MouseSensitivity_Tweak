
## マウス感度の計算式

R6Sのマウス感度は、  
**（マウスのDPI）×（ゲーム内感度）×（MouseSensitivityMultiplierUnit）**  
で決定されます。

## MouseSensitivityMultiplierUnitとは
（ゲーム内感度）×（MouseSensitivityMultiplierUnit）  
はセットで考えます。デフォルトの設定は  
　　・ゲーム内感度 ： 50  
　　・MouseSensitivityMultiplierUnit : 0.020000  
ですがこれらの積は、  
　　**50 × 0.020000 = 1**  
となり、マウスのDPIがそのままゲーム内のマウス感度になります。  
<br>
もしも、ゲーム内感度を25にしたとすると、  
　　**25 × 0.020000 = 0.5**  
となり、DPIの半分の値がゲーム内のマウス感度になります。

