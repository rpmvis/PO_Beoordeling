var IFrameObj; // our IFrame object

function GoTo(sWebPage){
	if (typeof(CanSubmit) !== 'undefined') // er moet gevalideerd / data opgeslagen worden
	{
		if (!CanSubmit()){return;}
	}

	// from here we can submit	
	var sSessionId = query_value(document.location.search, 'sessionid');
	if (sSessionId==="") sSessionId = null; // lblSessionId maakt van een null waarde een lege string
	var sPathName = document.location.pathname;
	var qs = QueryString_goto('goto', sSessionId, sPathName, sWebPage); // querystring

	// send request for a 'goto webpage' to Server via inline frame document
	// alert("qs van 'GoTo'" + qs);
	// escape methode om alle niet alfa-numerieke karakters te converteren 
	// naar hun hexadecimale equivalenten
	var URL = '../beoIFrame_server.aspx' + qs;
	// alert("URL:" + URL);
	callToServer(URL);
}

function FVs_ToServer_Plus_GoTo(arrFieldValues, sNextPage){
	// sWebPage + Field Values to Server
	
	var sSessionId = query_value(document.location.search, 'sessionid');
	var sPathName = document.location.pathname;
	
	if (sNextPage == undefined)
		alert("Variabele 'sNextPage' svp wel definiëren in Javascript proc. 'FVs_GoTo_ToServer'");

	var qs = QueryString_FVs('update_values_goto', sSessionId, 
	                          sPathName, sNextPage, 
	                          arrFieldValues);
	var URL = '../beoIFrame_server.aspx' + qs;
	return callToServer(URL);
}

function callToServer(URL) {
	if (!document.createElement) {return true};

	var IFrameDoc;
	
	// als IFrameObj nog niet ingesteld en we deze wel kunnen creëren
	if (!IFrameObj && document.createElement) {
		// create the IFrame and assign a reference to the
		// object to our global variable IFrameObj.
		// this will only happen the first time callToServer() is called
		try {
			var tempIFrame=document.createElement('iframe');
			tempIFrame.setAttribute('id','RSIFrame');
			tempIFrame.style.border='0px';
			tempIFrame.style.width='0px';
			tempIFrame.style.height='0px';
			IFrameObj = document.body.appendChild(tempIFrame);

			if (document.frames) {
				// this is for IE5 Mac,
				// because it will onlyallow access to the document object of the IFrame
				// if we access it through the document.frames array
				IFrameObj = document.frames['RSIFrame'];
			}
		} catch(exception) {
		
			// This is for IE5 PC, which does not allow dynamic creation
			// and manipulation of an iframe object.
			// Instead, we'll fake it up by creating our own objects.
			// make string "<iframe id=\"RSIFrame\" style=\"border:0px;width:0px;height:0px;\"></iframe>"
			iframeHTML='<iframe id="RSIFrame" style="';
			iframeHTML+='border:0px;';
			iframeHTML+='width:0px;';
			iframeHTML+='height:0px;';
			iframeHTML+='"><\/iframe>';
			document.body.innerHTML+=iframeHTML;
			IFrameObj = new Object();
			IFrameObj.document = new Object();
			IFrameObj.document.location = new Object();
			IFrameObj.document.location.iframe = document.getElementById('RSIFrame');
			IFrameObj.document.location.replace = function(location) {
			this.iframe.src = location;
			}
		}
	}

	if (navigator.userAgent.indexOf('Gecko') !=-1 && !IFrameObj.contentDocument) {
		// we have to give NS6 a fraction of a second
		// to recognize the new IFrame
		setTimeout('callToServer("'+ document.forms[0].id +'")',10);
		return false;
	}

	// instellen van document binnen het IFrame object
	if (IFrameObj.contentDocument) {
		// For NS6
		IFrameDoc = IFrameObj.contentDocument;
	} else if (IFrameObj.contentWindow) {
		// For IE5.5 and IE6
		IFrameDoc = IFrameObj.contentWindow.document;
	} else if (IFrameObj.document) {
		// For IE5
		IFrameDoc = IFrameObj.document;
	} else {
		return true;
	}

	IFrameDoc.location.replace(URL);
	return false;
}

function QueryString_goto(sReqType, sSessionId, sPathName, sWebPage) {
	// opbouwen Query string met naam/waarde paren uit formulier
	// LET OP: "formname + frm.id" gaan ook mee als naam/waarde paar
	var qs = ""; // querystring

	qs = qs + '?requesttype=' + encodeURI(escape(sReqType));
	qs = qs + '&sessionid=' + encodeURI(escape(sSessionId));
	qs = qs + '&pathname=' + encodeURI(escape(sPathName));
	qs = qs + '&menuitemid_to=' + encodeURI(escape(sWebPage));
	
	// iterate over form elements
	if (document.forms.length > 0){
		var frm = document.forms[0];
		var el = null;
		for (i=0;i<frm.elements.length;i++) {
			el = frm.elements[i]; 
			if (el.id != '') {
				switch(el.type){
					case "radio":
						// only save radio value if item of radiolist (lst_0, lst_1 etc.) is checked
						if (el.checked == true){
							var sListName = radioListName(el.id);
							qs = qs + '&' + sListName + '=' + encodeURI(escape(el.value));
						}
						break;
					default:
						qs = qs + '&' + el.id + '=' + encodeURI(escape(el.value));		
						break; 
				}
			}
		}
	}
	return qs;
}

function QueryString_FVs(sReqType, sSessionId, 
                         sPathName, sWebPage,
                         arrFieldValues) {
	// opbouwen Query string met naam/waarde paren
	// LET OP: "formname + frm.id" gaan ook mee als naam/waarde paar
	var qs = '?requesttype=' + sReqType;

	// function 'query_value' staat in General.js
	var sSessionId = query_value(document.location.search, 'sessionid');
	qs = qs + '&sessionid=' + encodeURI(escape(sSessionId));
	qs = qs + '&pathname=' + encodeURI(escape(sPathName));
	if (sWebPage !== -1) qs = qs + '&menuitemid_to=' + encodeURI(escape(sWebPage));

	// iterate over fieldvalues array
	if (arrFieldValues !== undefined) {
		for (i=0;i<arrFieldValues.length;i++)
		{
			aFV = arrFieldValues[i];
			qs = qs + '&' + aFV[0] + '=' + encodeURI(escape(aFV[1])); 
		}
	}
	return qs;
}

function handleResponse(sReqType, sWebPage, sSessionId) {
	// sent from beoIFrame_server.aspx to parent form
	// alert("handleResponse");
	var bGoTo = false;
	switch(sReqType){
		case "goto":
			bGoTo = true;
			break;
		case "update_values_goto":
			bGoTo = true;
			break;
		case "update_values":
			bGoTo = false;
			break;
	}		
	if (bGoTo){
		if (sWebPage =="") return;
		// alert("document.clear();");
		// document.clear();
		document.location = sWebPage + "?sessionid=" + sSessionId;
	}
}

function CloseApp(){
	alert("U wordt doorverbonden naar de site van DRO Amsterdam.");
	window.top.location.href = "http://www.dro.amsterdam.nl"; 
}
			
