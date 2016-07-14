function PrintIt()
{
	window.focus();
	window.print();  
}

function BP(num){
	if(num !== 0)
		return;
}

		function xReplace(SourceString, Old, New)
		{
			return (SourceString.split(Old).join(New));
		}


		function ChangeClass(elName, className){
			document.getElementById(elName).className = className; 
		}


		function ChangeElClass(elName, className, withAlert){
			var el = getEl(elName, withAlert);
			if (el==null) return;
			if (el.className !== className) el.className = className;
		}

		function ChangeFrElClass(frameName, elName, className, withAlert){
			var el = getFrEl(frameName, elName, withAlert);
			if(el == null) return;
			if (el.className !== className) el.className = className;
		}


		function focusFirst(){
			if ((document.forms.length > 0) && (document.forms[0].length > 0)){
				// set focus to first not hidden element 
				for(i=0; i < document.forms[0].length; i=i+1){
					var el = document.forms[0].elements[i];
					if (el.type != "hidden" && !(el.disabled)){
						if (!el.readOnly){
							el.focus(); 
							break;
						}
					}
				}
			}
		}

function href_filename(href){
	var temp, v, map;

	// mappen eraf
	v = href.split("/");

	temp = v[v.length-1];
	// querystring eraf
	v = temp.split("?");
	return v[0];
} 

function href_querystring(href){
	// afsplitsen queystring uit een href
	var v = href.split("?");
	var sRet ="";
	switch (v.length){
		case 1:
			break;
		case 2:
			sRet = v[1];
			break;
		default:
			alert("wel erg veel vraagtekens in href string: " + href + "!");
			break;
	}
	return sRet;
}

function radioListName(radioItemName){
	// in: 'lst_0' or 'lst_1'
	// or in: 'lst_example_0', 'lst_example_1'
	// out 'lst' or 'lst_example'
	var arr
	
	arr = radioItemName.split("_");
	var s = "";
	// t/m 1 na laatste
	for(var i=0; i < arr.length-1; i=i+1){
		s = s + arr[i];
	}
	return s;
}

function query_value(astring, query_key){
	// ophalen waarde uit een querystring (mag een href zijn) mbv de key 
	var temp, vNameValue, v, result =null;

	temp = href_querystring(astring);
	
	vNameValue = temp.split("&");
	
	// split NameValues in Name and Value array's (v)
	for(var i=0; i < vNameValue.length; i =i+1){
		v = vNameValue[i].split("=");
		if (v.length == 2 && v[0] === query_key){
			result = unescape(decodeURI(v[1]));
			break;
		} 		
	}
	return result;
}

function Strip(s, toStrip){
	var iPos = s.lastIndexOf(toStrip);
	if(iPos > -1) return s.substring(0, s.length - iPos);
	else return iPos;
}

function MaxWindow()
{
	window.moveTo(0,0);
	window.resizeTo(screen.width,screen.height);
}

function CloseWindow()
{
	window.close();
}

function GetAspRadioValue(ListName)
// het option value from asp radiobuttonlist control
{
	var elList = getEl(ListName);
	var optName;
	var elOption;
	for(i=0; i <  elList.rows.length; i++){
		optName = elList.id + "_" + i.toString();
		elOption = getEl(optName); 
		if(elOption.checked){
			return elOption.value;
			break;
		}
	}
	return null;
}

function GetRadioValue(listPrefixName)
// get radio value from controls like radio0, radio1, radio2 etc. ...
// "radio" is prefix here
{
	var radio = "";
	var i = -1;
	var val = null;
	
	while(true)
	{
		i++;
		radio = getEl(listPrefixName + i, false);
		if (radio == null){ 
			// minimaal moet <naam>0 er zijn
			if (i ==0){
				alert("element '" + listPrefixName + i + "' NIET gevonden!");
			}
			break;
		}

		if (radio.checked){
			val = radio.value;
			break;
		}
	}
	return nz(val,"");
}


function GetListIndex(value, arrOptions){
	// return index of value in arrOptions
	// if absent return -1
	var idx = -1;
	for(var i=0; i < arrOptions.length; i++){
		if(value != null)
		{
			if (arrOptions[i].toLowerCase() === value.toLowerCase())
			{
				idx = i;
				break;
			}
		}
	}
	return idx;
}
					
function SetListIndex(listPrefixName, value, arrOptions){
	var idx = GetListIndex(value, arrOptions);
	if (idx > -1)
		getEl(listPrefixName+idx).checked = true;
}
					
function IsNumeric(sText)
{
  var ValidChars = "0123456789.";
  var IsNumber=true;
  var Char;

  for (i = 0; i < sText.length && IsNumber == true; i++){ 
		Char = sText.charAt(i); 
		if (ValidChars.indexOf(Char) == -1){
			IsNumber = false;
    }
  }
  return IsNumber;
}

function nz(value, valueIfNull)
{
	if (value == null)
	{
		return valueIfNull;
	}
	else return value;
}

function isEl(elName, withAlert)
// check if element exists
{
	if (document.getElementById(elName)){
		return true;
	}
	else{
		if (typeof withAlert === 'undefined') withAlert = true;

		if(withAlert)
		{	
			alert("Element '" + elName + "' is niet gevonden binnen document '" + fr.document.location.href + "'.");
		}
		return false;
	}
}

function isFrEl(frameName, elName, withAlert)
// check if element 'elName' exists in frame 'frameName'
{
	if (typeof frameName === 'undefined'){
		alert("Vul een waarde in voor parameter 'frameName' in functie 'isFrEl'");
		return false;
	}
	
	if (typeof withAlert === 'undefined') withAlert = true;
	
	var fr = null;
	try{
		fr = top.frames(frameName);
	}
	catch(er) {
		alert("Frame '" + frameName + "' bestaat niet.");
		return false;
	}
	
	if (fr.document.getElementById(elName)){
		return true;
	}
	else{
		if(withAlert)
		{	
			alert("Element '" + elName + "' is niet gevonden binnen frame '" + frameName + "' en document '" + fr.document.location.href + "'.");
		}
		return false;
	}
}

function getFrEl(frameName, elName, withAlert)
{
	if (isFrEl(frameName, elName, withAlert))
		return top.frames(frameName).document.getElementById(elName);
	else return null;
}

function getEl(elName, withAlert)
{
	if(isEl(elName, withAlert))
		return document.getElementById(elName);
	else return null;
}

function getVal(elName, frameName)
{
	var el = getEl(elName);
	if(el !== null) return el.value;
	else return null;
}

function setVal(elName, value)
{
	var el = getEl(elName);
	if(el !== null) el.value = value;
}

function getInTxt(elName)
{
	var el = getEl(elName);
	if(el !== null) return el.innerText;
	else return null;
}

function setInTxt(elName, value)
{
	var el = getEl(elName);
	if(el !== null) el.innerText = value;
}

function getInHTML(elName)
{
	var el = getEl(elName);
	if(el !== null) return el.innerHTML;
	else return null;
}

function setInHTML(elName, value)
{
	var el = getEl(elName);
	if(el !== null) el.innerHTML = value;
}


function getChk(elName)
{
	var el = getEl(elName);
	if(el !== null) return el.checked;
	else return null;
}

function setChk(elName, value)
{
	var el = getEl(elName);
	if(el !== null) el.checked = value;
}


function enable(elName, bEnable)
{
	var el = getEl(elName);
	if(el !== null) el.disabled = !bEnable;
}

function setVis(elName, bVisible)
{
	var el = getEl(elName);
	if(el !== null){
		if (bVisible)
			el.style.visibility = 'visible';
		else
			el.style.visibility = 'hidden'; 
	}
}

function maxLen(elName, maxLimit){
	var el = getEl(elName);
	if (el === null) return;
	if (el.value.length > maxLimit){
		alert("De maximum lengte van " + maxLimit + " is bereikt voor dit tekstveld.\n\n" +
		      "De tekst wordt hierop aangepast.");
		 // if too long...trim it!
		el.value = el.value.substring(0, maxLimit);
	}
}

function confirm_delete(){
	// bevestigen verwijderen record
	if (confirm("Wilt u dit record echt verwijderen?")==true)
		return true;
	else
		return false;
}

function CloseApp(){
	window.top.location.href = "http://www.dro.amsterdam.nl"; 
}


