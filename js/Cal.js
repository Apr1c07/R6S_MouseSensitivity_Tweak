function onButtonClick() {
  var DPI=Number(document.getElementById('DPI').value);
  var x=Number(document.getElementById('x').value);
  var y=Number(document.getElementById('y').value);
  var ADS =Number(document.getElementById('ADS').value);
  var MSMU=Number(document.getElementById('MSMU').value);
  var XFA =Number(document.getElementById('XFA').value);
  var obj = document.getElementById('Result');
  obj.value=DPI+x+y+ADS+MSMU+XFA;
}
