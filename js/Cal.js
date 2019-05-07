function onButtonClick() {
  var DPI=Number(document.getElementById('DPI').value);
  var x=Number(document.getElementById('x').value);
  var y=Number(document.getElementById('y').value);
  var ADS =Number(document.getElementById('ADS').value);
  var MSMU=Number(document.getElementById('MSMU').value);
  var XFA =Number(document.getElementById('XFA').value);
  var obj = document.getElementById('Result');
  obj.value=DPI+x+y+ADS+MSMU+XFA;
  var table = document.getElementById('ResTable');
}

function coladd() {
  var table = document.getElementById("ResTable");
  // 行を行末に追加
  var row = table.insertRow(-1);
  //td分追加
  var cell1 = row.insertCell(-1);
  var cell2 = row.insertCell(-1);
  var cell3 = row.insertCell(-1);
  var cell4 = row.insertCell(-1);
  var cell5 = row.insertCell(-1);
  var cell6 = row.insertCell(-1);
  // セルの内容入力
  cell1.innerHTML = '1000';
  cell2.innerHTML = '10';
  cell3.innerHTML = '10';
  cell4.innerHTML = '10';
  cell5.innerHTML = '0.002000';
  cell6.innerHTML = '0.002000';
}
