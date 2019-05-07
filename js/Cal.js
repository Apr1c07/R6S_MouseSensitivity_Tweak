function onButtonClick() {
  alert('計算します');
  form1.Result.value=(from1.DPI.value + form1.x.value + form1.y.value+form1.ADS.value+form1.MSMU.value+form1.XFA.value);
  alert(form1.Result.value);
}
