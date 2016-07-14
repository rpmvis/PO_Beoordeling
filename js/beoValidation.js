		var cookies = new Object();

		function SetValidations(FormName){
			validations = new Array();
			cross_validations = new Array();
			switch (FormName){
				case 'example_form':
					validations[0] = ["username", "notblank"];
					validations[1] = ["useremail", "validemail"];
					validations[2] = ["favoritenumber", "isnumber"];
					return true;
					break;
				default:
					if (FormName !== 'beoWelkom_TL.aspx'){ 
						alert(FormName + " niet gevonden in 'SetValidations'."); 
					}
					return false; // no validations set
			}
		}

		var whitespace = " \t\n\r"; // incl. space char
		var FirstInvalidField = null;
		var InvalidMsg = "";

		function isEmpty(field){
			var i;
			var s = field.value;
			if((s === null) || (s.length === 0)){
				return true;
			}

			// Search string looking for characters that are not whitespace
			for (i = 0; i < s.length; i=i+1){   
				var c = s.charAt(i);
				if (whitespace.indexOf(c) == -1){ 
					return false;
				}
			}
			// All characters are whitespace.
			return true;
		}

		function isEmail(field){ 
			var positionOfAt;
			var s = field.value;

			positionOfAt = s.indexOf('@',1);  
			if (( positionOfAt == -1) || (positionOfAt == (s.length-1))){
				return false;
			}
			return true;
		}
		
		function isEmail_reg(field){ 
			/*
			valid email:
			- 1) Contains a least one character preceding the "@"
			- 2) Contains a "@" following 1)
			- 3) Contains at least one character following 2)
				followed by a dot (.), followed by either a two character
				or three character string (a two character country code or the standard three character US code, such as com, edu etc)
			*/

			var sTest = field.value;
			var filter="/^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i";
			if (filter.test(sTest)){
				return true;
			}
			else{
				InvalidMsg += "Het E-mail adres in veld '" + field.id + "' is ongeldig.\n\n";
				return false;
			}
		}

		function isDigit(c){   
			return ((c >= "0") && (c <= "9"));
		}

		function isInteger(field, sFld_in_msg){
			sFld_in_msg
		 
			var i, c;
			var s = field.value;
			if (isEmpty(field)){
				InvalidMsg += "Veld '" + sFld_in_msg + "' mag niet leeg zijn.\n\n";
				return false;
			}
			for (i = 0; i < s.length; i=i+1){
				// Check if current character is number.
				c = s.charAt(i);
				if (!isDigit(c)){
					InvalidMsg += "Veld '" + sFld_in_msg + "' kan alleen cijfers bevatten.\n\n";
					return false;
				}
			}
			return true;
		}

		function validate(FormName){
			var bSet = SetValidations(FormName);
			
			if (bSet === false){
				alert("geen validaties ingesteld voor formulier '" + FormName + "'");
				// no validations set, so form is valid 
				return true;
			}

			var i;
			var checkToMake;
			var field;
			var val1, val2; // cross validate val2 of field2 on val1 of field1
			var sFld1 , sFld2;
			var field1, field2; // cross validations
			var listPrefixName="";
			var sFld_in_msg;

			FirstInvalidField= null;
			InvalidMsg = "";

			for (i = 0; i < validations.length; i=i+1){
				listPrefixName = validations[i][0];
				checkToMake = validations[i][1];
			
				if (checkToMake == "getradio"){
					if (GetRadioValue(listPrefixName) == null){
						alert("Een keuze uit de opties bij veld '" + validations[i][2] + "' is verplicht.");
						return false;
					}
				}
				else{
					field = eval(getEl(validations[i][0]));

					if (!FieldExists(validations[i][0], field, FormName)) return false;

					if (typeof field === 'undefined'){
						alert ("Veld '" + validations[i][0] + 
									"' is niet gedefinieerd in formulier '" + FormName + "'!");
						return false;
					} 
					
					if(validations[i].length >=3)
						sFld_in_msg = validations[i][2];
					else
						sFld_in_msg = "";
						
										
					checkField(field, checkToMake, sFld_in_msg);
				}
			} // for validations
			
			// run CROSS validations: if first field 'not empty'  then second field be also 'not empty'
			for (i = 0; i < cross_validations.length; i=i+1){
				// cross_validations[0] = ["optNive", "getradio", "nee", "txtNivT" "Toelichting op 'vervulling van functie' is verplicht"];
				if(cross_validations[i][1] === "getradio"){
					listPrefixName = cross_validations[i][0];
					sFld2 = cross_validations[i][3];
					field2 = eval(getEl(sFld2));
					if (!FieldExists(sFld2, field2, FormName)) return false;
					
					val1 = cross_validations[i][2];
					if (GetRadioValue(listPrefixName) === val1){
						// dan moeten we de kruisvalidatie uitvoeren
						if (cross_validations[i].length >=6)
							sFld_in_msg = cross_validations[i][5];
						else
							sFld_in_msg="";

						checkToMake = cross_validations[i][4];
						checkField(field2, checkToMake, sFld_in_msg);
					}
				}
				else{
					sFld1 = cross_validations[i][0];
					sFld2 = cross_validations[i][1];
					field1 = eval(getEl(sFld1));
					field2 = eval(getEl(sFld2));
					
					if (!FieldExists(sFld1, field1, FormName)) return false;
					if (!FieldExists(sFld2, field2, FormName)) return false;
					
					if (!isEmpty(field1)){ // if field1 not empty and field2 empty
						if (cross_validations[i].length >=4)
							sFld_in_msg = cross_validations[i][3];
						else
							sFld_in_msg="";
					
						checkToMake = cross_validations[i][2];
						checkField(field2, checkToMake);
					}
				}
			}

			if (FirstInvalidField === null){
				return true;
			}
			else{
				alert(InvalidMsg);
				FirstInvalidField.focus();	
				return false;
			}
		}
		
		function checkField(field, checkToMake, sFld_in_msg){
			// alert("field.id " + field.id); 
			if (sFld_in_msg =="") sFld_in_msg = field.id.toString();
			switch (checkToMake){
				case 'notblank':
					if (isEmpty(field)){
						InvalidMsg += "Veld '" + sFld_in_msg + "' mag niet leeg zijn.\n\n";
						if (FirstInvalidField === null) {FirstInvalidField = field;}
					}
					break;
				case 'validemail':
					if (!isEmail(field)){
						InvalidMsg += "Het E-mail adres in veld '" + sFld_in_msg + "' is ongeldig.\n\n";
						if (FirstInvalidField === null) {FirstInvalidField = field;}
					}
					break;
				case 'isnumber':
					if (!isInteger(field, sFld_in_msg)){
						InvalidMsg += "Veld '" + sFld_in_msg + "' moet een geheel getal bevatten.\n\n"; 
						if (FirstInvalidField === null) {FirstInvalidField = field;}
					}
					break;
			}
		}
		
    function FieldExists(fldName, field, FormName){
			if (typeof field === 'undefined'){
				alert ("Veld '" + fldName + 
					      "' is niet gedefinieerd in formulier '" + FormName + "'!");
				return false;
			}
			else return true;
		}
		
		function CookiesEnabled(){
			document.cookie = "cookiesenabled=true";
			extractCookies();
			if (cookies["cookiesenabled"]){
				return true;
			}
			else{
				alert("U dient cookies aan te zetten om deze website te kunnen gebruiken!\n\n" +
					    "In Internet Explorer kun u dit doen via Extra/Opties (Tools/Options)");  
				return false;
			}
		}
		
		function extractCookies(){
			var name, value;
			var beginning, middle, end;

			// if there are entries in cookies, remove them
			for (name in cookies){
				cookies = new Object();
				break;
			}

			beginning = 0;
			while (beginning < document.cookie.length){
				middle = document.cookie.indexOf('=', beginning);
				end = document.cookie.indexOf(';', beginning);
				if (end == -1){
					end = document.cookie.length;
				}
				if ( (middle > end) || (middle == -1) ){
					name = document.cookie.substring(beginning, end);
					value = "";
				}
				else{
					name = document.cookie.substring(beginning, middle);
					value = document.cookie.substring(middle + 1, end);
				}
				cookies[name] = unescape(value);
				beginning = end + 2;
			}
		}

		// Browser Detect  v2.1.6
		// documentation: http://www.dithered.com/javascript/browser_detect/index.html
		// license: http://creativecommons.org/licenses/by/1.0/
		// code by Chris Nott (chris[at]dithered[dot]com)


		function BrowserDetect() {
			var ua = navigator.userAgent.toLowerCase(); 

			// browser engine name
			this.isGecko       = (ua.indexOf('gecko') != -1 && ua.indexOf('safari') == -1);
			this.isAppleWebKit = (ua.indexOf('applewebkit') != -1);

			// browser name
			this.isKonqueror   = (ua.indexOf('konqueror') != -1); 
			this.isSafari      = (ua.indexOf('safari') != - 1);
			this.isOmniweb     = (ua.indexOf('omniweb') != - 1);
			this.isOpera       = (ua.indexOf('opera') != -1); 
			this.isIcab        = (ua.indexOf('icab') != -1); 
			this.isAol         = (ua.indexOf('aol') != -1); 
			this.isIE          = (ua.indexOf('msie') != -1 && !this.isOpera && (ua.indexOf('webtv') == -1) ); 
			this.isMozilla     = (this.isGecko && ua.indexOf('gecko/') + 14 == ua.length);
			this.isFirebird    = (ua.indexOf('firebird/') != -1);
			this.isNS          = ( (this.isGecko) ? (ua.indexOf('netscape') != -1) : ( (ua.indexOf('mozilla') != -1) && !this.isOpera && !this.isSafari && (ua.indexOf('spoofer') == -1) && (ua.indexOf('compatible') == -1) && (ua.indexOf('webtv') == -1) && (ua.indexOf('hotjava') == -1) ) );
		   
			// spoofing and compatible browsers
			this.isIECompatible = ( (ua.indexOf('msie') != -1) && !this.isIE);
			this.isNSCompatible = ( (ua.indexOf('mozilla') != -1) && !this.isNS && !this.isMozilla);
		   
			// rendering engine versions
			this.geckoVersion = ( (this.isGecko) ? ua.substring( (ua.lastIndexOf('gecko/') + 6), (ua.lastIndexOf('gecko/') + 14) ) : -1 );
			this.equivalentMozilla = ( (this.isGecko) ? parseFloat( ua.substring( ua.indexOf('rv:') + 3 ) ) : -1 );
			this.appleWebKitVersion = ( (this.isAppleWebKit) ? parseFloat( ua.substring( ua.indexOf('applewebkit/') + 12) ) : -1 );
		   
			// browser version
			this.versionMinor = parseFloat(navigator.appVersion); 
		   
			// correct version number
			if (this.isGecko && !this.isMozilla) {
					this.versionMinor = parseFloat( ua.substring( ua.indexOf('/', ua.indexOf('gecko/') + 6) + 1 ) );
			}
			else if (this.isMozilla) {
					this.versionMinor = parseFloat( ua.substring( ua.indexOf('rv:') + 3 ) );
			}
			else if (this.isIE && this.versionMinor >= 4) {
					this.versionMinor = parseFloat( ua.substring( ua.indexOf('msie ') + 5 ) );
			}
			else if (this.isKonqueror) {
					this.versionMinor = parseFloat( ua.substring( ua.indexOf('konqueror/') + 10 ) );
			}
			else if (this.isSafari) {
					this.versionMinor = parseFloat( ua.substring( ua.lastIndexOf('safari/') + 7 ) );
			}
			else if (this.isOmniweb) {
					this.versionMinor = parseFloat( ua.substring( ua.lastIndexOf('omniweb/') + 8 ) );
			}
			else if (this.isOpera) {
					this.versionMinor = parseFloat( ua.substring( ua.indexOf('opera') + 6 ) );
			}
			else if (this.isIcab) {
					this.versionMinor = parseFloat( ua.substring( ua.indexOf('icab') + 5 ) );
			}
		   
			this.versionMajor = parseInt(this.versionMinor); 
		   
			// dom support
			this.isDOM1 = (document.getElementById);
			this.isDOM2Event = (document.addEventListener && document.removeEventListener);
		   
			// css compatibility mode
			this.mode = document.compatMode ? document.compatMode : 'BackCompat';

			// platform
			this.isWin    = (ua.indexOf('win') != -1);
			this.isWin32  = (this.isWin && ( ua.indexOf('95') != -1 || ua.indexOf('98') != -1 || ua.indexOf('nt') != -1 || ua.indexOf('win32') != -1 || ua.indexOf('32bit') != -1 || ua.indexOf('xp') != -1) );
			this.isMac    = (ua.indexOf('mac') != -1);
			this.isUnix   = (ua.indexOf('unix') != -1 || ua.indexOf('sunos') != -1 || ua.indexOf('bsd') != -1 || ua.indexOf('x11') != -1)
			this.isLinux  = (ua.indexOf('linux') != -1);
		   
			// specific browser shortcuts
			this.isNS4x = (this.isNS && this.versionMajor == 4);
			this.isNS40x = (this.isNS4x && this.versionMinor < 4.5);
			this.isNS47x = (this.isNS4x && this.versionMinor >= 4.7);
			this.isNS4up = (this.isNS && this.versionMinor >= 4);
			this.isNS6x = (this.isNS && this.versionMajor == 6);
			this.isNS6up = (this.isNS && this.versionMajor >= 6);
			this.isNS7x = (this.isNS && this.versionMajor == 7);
			this.isNS7up = (this.isNS && this.versionMajor >= 7);
		   
			this.isIE4x = (this.isIE && this.versionMajor == 4);
			this.isIE4up = (this.isIE && this.versionMajor >= 4);
			this.isIE5x = (this.isIE && this.versionMajor == 5);
			this.isIE55 = (this.isIE && this.versionMinor == 5.5);
			this.isIE5up = (this.isIE && this.versionMajor >= 5);
			this.isIE55up = (this.isIE && this.versionMajor >= 5.5);
			this.isIE6x = (this.isIE && this.versionMajor == 6);
			this.isIE6up = (this.isIE && this.versionMajor >= 6);
		   
			this.isIE4xMac = (this.isIE4x && this.isMac);
		}
		var browser = new BrowserDetect();

		function noPostBack(doc, sNewFormAction)
		// 'doc' is document form specific frame
		// sNewFormAction is new form action for current form in 'doc'
		{
			if (doc.forms.length == 0){
				return;
			}
		
			if(browser.isNS4x) //The browser is Netscape 4
			{
				doc.layers['Content'].doc.forms[0].__VIEWSTATE.name = 'NOVIEWSTATE';
				doc.layers['Content'].doc.forms[0].action = sNewFormAction;
				// doc.layers['Content'].doc.forms[0].method = "get";
			}
			else //It is some other browser that understands the DOM
			{
				doc.forms[0].action = sNewFormAction;
				doc.forms[0].__VIEWSTATE.name = 'NOVIEWSTATE'; // disable VIEWSTATE
				// doc.forms[0].method = "get";
			}
		}

		function xReplace(SourceString, Old, New)
		{
			return (SourceString.split(Old).join(New));
		}

		function trim(str){
			var b  = true;
			var temp;
			
			temp = str;
			while (b){
				var iPos = str.indexOf(" ");  	
				switch (iPos){
					case 1:
						str = str.substring(iPos+1);
						b= true;	
						break;
					case str.length:
						if (iPos===1) str = "";
						else str = str.substring(iPos, str.length-1);					
						b= true;	
					default:
						b = false;
				}
			}
			return temp;	
		}
		
		function CheckLen(el, len)
		{
			if (el == null) return (false);
			
			if (el.value.length > len)
			{
				var sMsg = "Lengte van veld '" + el.name + " 'mag niet groter zijn dan " + len + "!";
				alert(sMsg);
				el.focus();  
				el.value = el.value.substring(0, len);
				return (false);
			}
			return (true);
		}
