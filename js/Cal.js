var dpi;
var x;
var y;
var ads;
var ads_mag;
var DPI_HIP;
var msmu;
var xfa;
var DPI_ADS;
var dpi_val_min;
var ItemList = new Array();

function Cal(){
  var dpi=Number(document.getElementById('DPI').value);
  var x=Number(document.getElementById('x').value);
  var y=Number(document.getElementById('y').value);
  var ads =Number(document.getElementById('ADS').value);
  var msmu=Number(document.getElementById('MSMU').value);
  var xfa =Number(document.getElementById('XFA').value);
  
  DPI_HIP = dpi*x*msmu;
  DPI_ADS = (dpi * x * msmu) * ads * xfac;
  dpi_val_min = Math.trunc(DPI_HIP)-500;
  if (dpi_val_min <= 0) dpi_val_min = 1;
  var x_mag = x*msmu;
  
  for(var DPI_HIP_temp = dpi_val_min;DPI_HIP_temp <= Math.Truncate(DPI_HIP) + 500; DPI_HIP_temp++){
    
    if (msmu_dpi_check(DPI_HIP_temp))
    {
      if (ads_xfac_check(DPI_ADS / DPI_HIP_temp))
      {
        var x_new;
        var msmu_new;
        var ads_new;
        var xfac_new;
        x_msmu_cal(out msmu_new, out x_new, DPI_HIP_temp);
        ads_xfac_cal(out xfac_new, out ads_new, DPI_ADS / DPI_HIP_temp);
        ItemList.push(DPI_HIP_temp);
      }
    }
  }
  create_low();
}

function create_low(){
  for (var j = 0; j <= ItemList.length - 1; j++)
  {
    //comboBox1.SelectedIndex = j;
    if (msmu_dpi_check(ItemList[j]))//仮のdpiが実現可能かどうか
    {    
      if (ads_xfac_check(DPI_ADS / dpi))//そのdpiの場合にxfacの設定が可能か
      {
        var x_new;
        var msmu_new;
        var ads_new;
        var xfac_new;
        let{msmu_new, x_new} = x_msmu_cal(dpi);
        let{xfac_new, ads_new} = ads_xfac_cal(DPI_ADS / dpi);
        
        var table = document.getElementById("ResTable");
        var row = table.insertRow(-1);
        var cell1 = row.insertCell(-1);
        var cell2 = row.insertCell(-1);
        var cell3 = row.insertCell(-1);
        var cell4 = row.insertCell(-1);
        var cell5 = row.insertCell(-1);
        var cell6 = row.insertCell(-1);
        
        cell1.innerHTML = dpi * x_new * msmu_new;
        cell2.innerHTML = dpi * x_new * msmu_new * ads_new * xfac_new;
        cell3.innerHTML = x_new;
        cell4.innerHTML = ads_new;
        cell5.innerHTML = msmu_new.toFixed(6);
        cell6.innerHTML = xfac_new.toFixed(6);
      }
    }
  }
}

function msmu_dpi_check(temp_dpi) //変更可能かどうかだけを出す
{
  var test;
  var test_bool = false;
  for (var i = 1; i <= 100; i++)
  {
    test = temp_dpi / dpi / i;
    
    
    if (test > 0 && getDecimalPointLength(test) <= 6 && test < 10)
    {
      test_bool = true;
    }
  }
  if (test_bool) return true;
  else return false;
}

function ads_xfac_check(ads_mag_temp) //変更可能かどうかだけを出す
{
  var test;
  var test_bool = false;
  for (var i = 1; i <= 100; i++)
  {
    test = ads_mag_temp / (Decimal)i;
    if (test > 0 && getDecimalPointLength(test) <= 6 && test < 10)
    {
      test_bool = true;
    }
  }
  if (test_bool) return true;
  else return false;
}

function x_msmu_cal(temp_dpi) //元々のxfacに近い値を計算
{
  var test;//仮のmsmu
  var diff = 10000;
  let re1 = 0;
  let re2 = 0;
  
  for (int i = 1; i <= 100; i++)
  {
    test = temp_dpi / (Decimal)dpi / (Decimal)i;
    if (test > 0 && getDecimalPointLength(test) <= 6 && test < 10)
    {
      //MessageBox.Show(test.ToString());
      if (Math.abs(test - msmu) < diff)
      {
        // MessageBox.Show("(test - msmu) < diff = (" + test + " - " + msmu + ") < " + diff + "   test = " + test + ", i = " + i);
        diff = Math.abs(test - msmu);
        re1 = test; //msmu
        re2 = i;　//ads
      }
    }
  }
  return {re1,re2}
}

var ads_xfac_cal(ads_mag_temp)
{
  var test;
  var diff = 10000;
  let re1 = 0;
  let re2 = 0;
  
  for (int i = 1; i <= 100; i++)
  {
    test = ads_mag_temp / (Decimal)i;
    if (test > 0 && getDecimalPointLength(test) <= 6 && test < 10)
    {
      if (Math.abs(test - xfac) < diff)
      {
        diff = Math.abs(test - xfac);
        re1 = test;
        re2 = i;
      }
    }
  }
  return {re1,re2}
}

var getDecimalPointLength = function(number) {
    var numbers = String(number).split('.'), result = 0;
 
    if (numbers[1]) result = numbers[1].length;
 
    return result;
};

