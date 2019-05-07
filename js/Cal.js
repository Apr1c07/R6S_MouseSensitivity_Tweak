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
  alert('calling');
  dpi=Number(document.getElementById('DPI').value);
  x=Number(document.getElementById('x').value);
  y=Number(document.getElementById('y').value);
  ads =Number(document.getElementById('ADS').value);
  msmu=Number(document.getElementById('MSMU').value);
  xfa =Number(document.getElementById('XFA').value);

  DPI_HIP = dpi*x*msmu;
  DPI_ADS = (dpi * x * msmu) * ads * xfa;
  dpi_val_min = Math.trunc(DPI_HIP)-500;
  if (dpi_val_min <= 0) dpi_val_min = 1;
  var x_mag = x*msmu;

  for(var DPI_HIP_temp = dpi_val_min; DPI_HIP_temp <= Math.trunc(DPI_HIP) + 500; DPI_HIP_temp++){
    if(msmu_dpi_check(DPI_HIP_temp)){
      alert('ok');
      if (ads_xfac_check(DPI_ADS / DPI_HIP_temp))
      {
        alert('ok');
        let{msmu_new, x_new} = x_msmu_cal(DPI_HIP_temp);
        alert('ok');
        let{xfa_new, ads_new} = ads_xfac_cal(DPI_ADS / DPI_HIP_temp);
        alert('ok');
        var table = document.getElementById("ResTable");
        var row = table.insertRow(-1);
        var cell1 = row.insertCell(-1);
        var cell2 = row.insertCell(-1);
        var cell3 = row.insertCell(-1);
        var cell4 = row.insertCell(-1);
        var cell5 = row.insertCell(-1);
        var cell6 = row.insertCell(-1);

        cell1.innerHTML = dpi * x_new * msmu_new;
        //cell2.innerHTML = dpi * x_new * msmu_new * ads_new * xfa_new;
        cell2.innerHTML = x_new;
        cell3.innerHTML = x_new;
        cell4.innerHTML = ads_new;
        cell5.innerHTML = msmu_new.toFixed(6);
        cell6.innerHTML = xfa_new.toFixed(6);
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
    test = ads_mag_temp / i;
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
  let msmu_new = 0;
  let x_new = 0;

  for (var i = 1; i <= 100; i++)
  {
    test = temp_dpi / dpi / i;
    if (test > 0 && getDecimalPointLength(test) <= 6 && test < 10)
    {
      if (Math.abs(test - msmu) < diff)
      {
        diff = Math.abs(test - msmu);
        msmu_new = test; //msmu
        x_new = i;　//ads
      }
    }
  }
  return {msmu_new, x_new}
}

function ads_xfac_cal(ads_mag_temp)
{
  var test;
  var diff = 10000;
  let xfa_new = 0;
  let ads_new = 0;

  for (var i = 1; i <= 100; i++)
  {
    test = ads_mag_temp / i;
    if (test > 0 && getDecimalPointLength(test) <= 6 && test < 10)
    {
      if (Math.abs(test - xfa) < diff)
      {
        diff = Math.abs(test - xfa);
        xfa_new = test;
        ads_new = i;
      }
    }
  }
  return {xfa_new, ads_new}
}

var getDecimalPointLength = function(number) {
  var numbers = String(number).split('.'), result = 0;

  if (numbers[1]) result = numbers[1].length;

  return result;
};
