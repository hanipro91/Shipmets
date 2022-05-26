
/*
************************************
*      version 2.3.1               *
************************************
* version 2.2.3 Remark
* 	- Filtering function DoFilter(<target selector>, <attribute selector>, <value>) added. 
* version 2.3.0 Remark
*   -IndexOf function Added
*   -ObjectOf function Added
* version 2.3.1 Remark
*   -Function Date.Add added.
*/

function hasClass(e, cls){ return elm(e).className.indexOf(cls) >= 0;}
function removeClass(e, cls){ e.classList.remove(cls); }
function addClass(e, cls){ e.className+= " " + cls; }
function replaceClass(e, cls1, cls2){ removeClass(e, cls1); addClass(e, cls2); }
function toggleClass(e, c1, c2){ if(hasClass(e,c1)) replaceClass(e,c1,c2); else replaceClass(e, c2, c1); }
    
function New(tag){return document.createElement(tag);}

function elm(id){ return typeof id == "string"? document.getElementById(id): id; }

function get(id){ 
       if(typeof id ==='string')
		  var e = document.getElementById(id);
       else e=id;
		var n = e.tagName.toLocaleLowerCase();
		var t = e.type;
		if(n=='input' && t=='checkbox')
			return !e.checked?false: e.value!=null && e.value != undefined && e.value.toString().length>0? e.value:true; //getAttribute("checked");
		else if(n=='img'||n=='image'||(n=='input' && t=='image'))
			return e.src;
		else if(n=='input' && t=='radio')
			{
				var radios = document.getElementsByName(e.name);
				for(var i=0; i<radios.length; i++)
					if(radios[i].checked)
						return radios[i].value;	
			}
		else if(n=='input' || n=="textarea" || n=="select")
			return e.value;
		else 
			return e.innerHTML;
	}

function set(Field, v) {
	    if(v === 'false') v = false;
		if(!Field) return;
		if(typeof (Field) =='string') Field = document.getElementById(Field);
    	var n = Field.tagName.toLocaleLowerCase();
    	var t = Field.type;
        if(t=='datetime-local') v = v.replace(' ', 'T');
        if(t=='date' && v.length>12) v = v.substr(0, 10);
		if(n == 'img'|| n=='image'||(n=='input' && t == 'image'))
            Field.src = v;
		else if(n=='input' && t=="checkbox")
			 Field.checked = v;
		else if(n=='input' && t=="radio")
			{
				var radios = document.getElementsByName(Field.getAttribute("name"));
				for(var ir = 0; ir < radios.length; ir++)
					if(radios[ir].value == v)
						radios[ir].setAttribute("checked","on");
			}
		else if(n=='input'||n=="select"||n=='textarea')
			Field.value = v;
		else Field.innerHTML = v;
	 }

function getCountPerLine (e, s){ e = elm(e); return Math.floor( e.offsetWidth / s ); }

//is x child of y.
function isChild(x,y){
	
	while(x.parentNode != null && x.parentNode != window){
		if (x.parentNode == y) return true;
		x = x.parentNode;
	}
	return false;
}

function isBelongTo(x,y){
	return x == y || isChild(x,y);
}

function isBelongToClass(x,cls){
	
	while(x != null && x != window){
		if(hasClass(x,cls)) return true;
		x = x.parentNode;
	}
	return false;
}

function title(e){ return elm(e).title; }

function SelectedOptionText(element){ element = elm(element); return  element.options[ element.selectedIndex ].text}
function SelectedOption(element) { element = elm(element); return element.options[element.selectedIndex]; }
function Options(element, i) { element = elm(element); return i!=undefined? element.options[i]: SelectedOption(element); }
function OptionsText(element, i) {  element = elm(element);  return i!=undefined? element.options[i].text:SelectedOptionText(element); }
	var OptionText = optionText = OptionsText;
	var Option = Options;
function html(obj, data){ elm(obj).innerHTML = data; }

function attr(e, a, v){ e=elm(e); if(v !== undefined) e.setAttribute(a, v);  return e.getAttribute(a); }
function hasAttr(e, a){ return e.getAttribute(a) !== null }

function def(v){ return v!== undefined; }

function Hide(obj){elm(obj).style.display="none";}
function Hidea(obj){ elm(obj).setAttribute("hidden", "true"); }
function vHide(obj){elm(obj).style.visibility="hidden"}

function Show(obj){ elm(obj).style.display="block"; }

function Showa(obj){ elm(obj).removeAttribute( "hidden" ); }

function Showi(obj){elm(obj).style.display="inline";}

function Showib(obj){elm(obj).style.display="inline-block";}

function vShow(obj){elm(obj).style.visibility = "visible";}

function Display(obj, v){elm(obj).style.display=v;}

function Disable(obj){elm(obj).setAttribute("disabled", "true"); }

function Enable(obj){elm(obj).removeAttribute("disabled"); }

function rect( element ) {
    return element.getBoundingClientRect();
}

function NDigits(v, n){
	v = v.toString();
	while(v.length < n) v = '0' + v;
	return v;
}
/*Date/Time functions*/
if(Date.toInputDate === undefined )
    Date.prototype.toInputDate = function (ms){
        return  this.getFullYear() + "-" + NDigits(this.getMonth() + 1,2) +"-" + NDigits(this.getDate(),2);
};
if(Date.toInputTime === undefined )
        Date.prototype.toInputTime = function (ms){
            return  NDigits(this.getHours(),2) + ":" + NDigits(this.getMinutes(),2) + ":" + NDigits(this.getSeconds(),2);
};


if(Date.Add === undefined )
    Date.prototype.Add = function (value, interval){
	    if(typeof interval == 'string') interval = interval.toLowerCase();
		switch(interval){
			
			case 'ms', 'mili', 'milisecond', 'miliseconds': return  new Date(this.getTime() + value);
			case 's', 'sec', 'second', 'seconds': return  new Date(this.getTime() + 1000 * value);
			case 'm', 'min', 'minute', 'minutes': return  new Date(this.getTime() + 60000 * value);
			case 'h', 'hr', 'hour', 'hours': return  new Date(this.getTime() + 3600000 * value);
			case 'day', 'days', 'd': return  new Date(this.getTime() + 3600000 * 24 * value);
			
		}
		//else return days
        return  new Date(this.getTime() + 3600000 * 24 * value);
};
//End date/time functions
/**
 * Get index of element in array of objects.
 * @param Array target array to search in.
 * @param key object key name.
 * @param value specified value to search for in array.  
 * @returns index of first element that contain key with value equal to specified value. 
 */
function IndexOf(Array, key, value){
	for(var i=0; i<Array.length; i++)
		for(var k in Array[i])
			if(k == key && Array[i][k] == value)
				return i;
	return -1;
}

function ObjectOf(Array, key, value){
  var i = IndexOf(Array, key, value);
  return i==-1? null: Array[i];
}
/**
 * This function searches for the existence of a specified CSS selector in a given stylesheet.
 *
 * @param (string) styleSheetName - This is the name of the stylesheet you'd like to search if not specified then search all.
 * @param (string) selector - This is the name of the selector you'd like to find
 * @return (bool) - Returns true if the selector is found, false if it's not found
 * @example - console.log(selectorInStyleSheet ('myStyleSheet.css', '.myClass'));
 */    

function selectorInStyleSheet(selector, styleSheetName) {
	
    /*
     * Get the index of 'styleSheetName' from the document.styleSheets object
     */
    for (var i = 0; i < document.styleSheets.length; i++) {
        var thisStyleSheet = document.styleSheets[i].href ? document.styleSheets[i].href.replace(/^.*[\\\/]/, '') : '';
        if (thisStyleSheet == styleSheetName || styleSheetName == undefined || styleSheetName==null || styleSheet == ""){
			var idx = i; 
			/*
     		 * Check the stylesheet for the specified selector
     		 */
    		var styleSheet = document.styleSheets[idx];
    		var cssRules = styleSheet.rules ? styleSheet.rules : styleSheet.cssRules;
    		for (var j = 0; j < cssRules.length; ++j) {
        		if(cssRules[j].selectorText == selector) return true;
    		}
			if(styleSheetName != null && styleSheet!= undefined && styleSheet != "")
			break; 
		}
    }
    //if (!idx) return false; // We can't find the specified stylesheet

    
    return false;
}
//load image ------------------------------------------------------------------------
    var ImageTarget = null;
    var Rdr = new FileReader();
    var ImageLoadComplete = false;
    var Loaded_File_Name = "";
    var OnLoadFileEnd = function(content, filename){ /* set as this syntax */}
    Rdr.onloadend = function(evt){
		   if(typeof (ImageTarget) == 'object');
		   		set(ImageTarget, evt.target.result);
		   OnLoadFileEnd( evt.target.result, Loaded_File_Name );
		   ImageLoadComplete = true;
	  }
    function LoadImage(target){
        ImageTarget = target;
        var fl = New("input");
        fl.setAttribute("type","file");
        fl.onchange = function (){ all_change(fl)};
        ImageLoadComplete = false;
        fl.click();
     }
    function all_change(b){Rdr.readAsDataURL(b.files[0]); b.remove(); }
//-----------------------------------------------------------------------------------

//Load Any File and put content in the target element and send it to complete function.
//@param encoding: Determine type of data as { [text|txt|UTF-8|ANSI] | [base64|base-64|base 64] | [bin|binary] | [ arr|array|bytes|byte[] ]}
function UpLoadFile(encoding, content_target_elm ,func){
	var fl = New("input");
	    ImageTarget = content_target_elm;
	    if(func) OnLoadFileEnd = func;
        fl.type="file";
        fl.onchange = function (){ 
			var enc = encoding.toLowerCase();
			if( enc == "base64" || enc == 'base-64' || enc == 'base 64' )
				Rdr.readAsDataURL( fl.files[0] );
			else if( enc == 'text' || enc == 'txt' )
				Rdr.readAsText(fl.files[0],"UTF-8");
			else if( enc == 'ansi' )
				Rdr.readAsText(fl.files[0], 'ANSI')
			else if( enc == "bin" || enc == "binary" )
				Rdr.readAsBinaryString(fl.files[0]);
			else if( enc == 'array' || enc == 'arr' || enc == 'byte' || enc == 'bytes' || enc == 'byte[]' )
				Rdr.readAsArrayBuffer( fl.files[0] );
			else
			Rdr.readAsText(fl.files[0], encoding.toUpperCase()); 
			ImageLoadComplete = false;
			Loaded_File_Name = fl.value;
			Loaded_File_Name = Loaded_File_Name.substr(Loaded_File_Name.lastIndexOf('\\') + 1);
			fl.remove(); 
		};
		fl.click();
}
function call(fun, arg1, arg2, arg3, arg4, arg5){
    if(fun) fun(arg1, arg2, arg3, arg4, arg5);
}

function select(obj, q){if(typeof obj=='string')obj=elm(obj); return obj.querySelector(q); }

function selectAll(obj, q){if(typeof obj=='string')obj=elm(obj); return obj.querySelectorAll(q); }

//****************////****************///****************//
function isVisible( e ) {
    var rect = e.getClientRects()[0];
    return rect.top >= 0 && rect.bottom <= window.innerHeight && 
           rect.left >= 0 && rect.right <= window.innerWidth;   
}

function ScrollIntoView( elem ) { elem=elm(elem); if(!isVisible(elem)) elem.scrollIntoView(); }
function ScrollTo( elem ) { elem=elm(elem); if(!isVisible(elem)) window.scrollTo( elem.offsetLeft, elem.offsetTop ); }

//====Generate Options of Select Element===========================================================================================

const AUTO=["YEARS", "MONTHS", "DAYS"]
/**
 * @param arg1: Start | Values array
 * @param arg2: End   | Text array
 * @param arg3: Label number for numeric Options Text, text to add to number e.g: Deg1, Deg2, ... 
 * @param arg4: Inc/Dec value, Value of increment or decrement if sequence options: default value is 1
 * @example <select data-start=2 data-end=10 data-text="Level " data-delta="2"></select> 
 * @example <select data-values="['apple','orange', 'banana']" data-texts="['apple fruit','orange fruit', 'banana fruit']"></select>
 * */
function GenerateOptions(arg1, arg2, arg3, arg4, attrs){
	var Ops = "";
	if(arg3 === undefined || arg3 === null) arg3 = "";
	var delta = isNaN( parseInt(arg4) )? 1: parseInt(arg4); 
	if(typeof arg1 == 'number'){
	  if(arg1 <= arg2)
		  for(var i=arg1; i<= arg2; i+=delta)
				Ops += '<option value="'+i+'">'+ arg3+i+'</options>';
	  else
		  for(var i=arg1; i>= arg2; i-=delta)
				Ops += '<option value="'+i+'">'+ arg3+i+'</options>';
	}
	else if(arg1 instanceof Array && arg2 instanceof Array)
		for(var i=0; i< arg1.length; i++){
			var atrs = "";
			for(var a in attrs)
				atrs += a +'="' + attrs[a][i] + '" ';
			Ops += '<option value="'+arg1[i]+'" ' + atrs + '>'+ arg2[i]+'</options>';
		}
			
	return Ops;
}

function AutoGenerateOptions(){
	var dds = selectAll(document, "select");
	for(var i=0; i<dds.length; i++){
		var s = dds[i];
		var attrs = {};
		
		if (hasAttr(s, 'data-cls')){
			attrs.Class = eval(attr(s, 'data-cls'));
		}
		
		if (hasAttr(s, 'data-titles')){
			attrs.Title = eval(attr(s, 'data-titles'));
		}
		
		if( hasAttr(s, 'data-start') && hasAttr(s, 'data-end'))
			s.innerHTML += GenerateOptions(parseInt( attr(s, 'data-start')),parseInt( attr(s, 'data-end')), attr(s, "data-text"), hasAttr(s, 'data-delta')? parseInt(attr(s, 'data-delta')): 1, attrs);
		else if( hasAttr(s, 'data-texts') && hasAttr(s, 'data-values'))
			s.innerHTML += GenerateOptions(eval(attr(s, 'data-values')), eval(attr(s, 'data-texts')), undefined, undefined, attrs);
	   else if( hasAttr(s, 'data-en') && hasAttr(s, 'data-values') && typeof Language != 'undefined' && Language == En)
			s.innerHTML += GenerateOptions(eval(attr(s, 'data-values')), eval(attr(s, 'data-en')), undefined, undefined, attrs);
	   else if( hasAttr(s, 'data-ar') && hasAttr(s, 'data-values') && typeof Language != 'undefined' && Language == AR)
			s.innerHTML += GenerateOptions(eval(attr(s, 'data-values')), eval(attr(s, 'data-ar')), undefined, undefined, attrs);
	   else if (hasAttr(s, 'data-values'))
		   s.innerHTML += GenerateOptions(eval(attr(s, 'data-values')), eval(attr(s, 'data-values')), undefined, undefined, attrs);
			
	}
	
}
//--------------------------------------------------------------------------------------------------------------------------------
/**
 * Apply filtering on lists, selects, demo elements
 * @param selector1 CSS Selector to select element
 * @param selector2 CSS Selector to select children of elements
 * @param value value of selector2 that allow to view child
 * @param viewStyle Display child CSS style, one of display property values.
 * @returns nothing
 */
function DoFilter(selector1, selector2, value, viewStyle){
	if(viewStyle == undefined) viewStyle = 'list-item';
	var ps = selector1 instanceof Array? selector1: typeof selector=='string'? selectAll(document, selector1):[selector1];
	ps.forEach(function(e){
		var cs = selectAll(e, selector2);
		cs.forEach(function(x){
			Hide(x);
		});
		cs = selectAll(e, selector2+'='+value);
		cs.forEach(function(x){
			x.style.display = viewStyle;
		});
	});
}
//=================================================================================================================================

/**
 Validate input dataType if correct or not.
 DataType must set in attribute 'type'.
 e: elemnt object or id
*/

var VError={
    NULL:'Element not found',
    LESS:'Value length less than requered.',
    DATE:'Invalid Date',
    NUMBER: 'Invalid Number',
    RANG: 'Value Out Of range',
    INT: 'Float value not Accept.',
    MAIL:'Invalid e-mail address',
    TEL:'Invalid Phone Number.',
    TIME:'Invalid time format',
    LETTER:'Invalid Charector. Use letters only',
    LETNUM:'Invalid Charector. Use letters and numbers only',
    WORD:"Invalid Charector.",
    MAX:"Value bigger than Expacted.",
    MIN:"Value Less than Expacted",
};
if(typeof Arabic != 'undefined')
    VError = {
        NULL:'العنصر غير موجود',
        LESS:' عدد حروف القيمة اقل من المطلوب',
        DATE:'صيغة التاريخ غير صحيحة',
        NUMBER: 'القيمة ليست رقمية',
        RANG: 'القيمة اعلى او اقل من المطلوب',
        INT: 'لايسمح باستخدام القيمة العشرية هنا',
        MAIL:'صيغة بريد الالكتروني غير صحيحة',
        TEL:'رقم هاتف غير صحيح',
        TIME:'صيغة الوقت غير صحيحة',
        LETTER:'رمز غير مقبول. استخدم حروف الابجدية فقط',
        LETNUM:'رمز غير مقبول. استخدم حروف الابجدية والارقام فقط',
        WORD:'رمز غير مقبول',
        MAX:"قيمة اكبر من الازم",
        MIN:"قيمة اقل من المفروض",
    };
function ValidateInput(elm, errCallback, minLength){
    "use strict";
    var e, v, nv;
    
    if(typeof (elm) === 'string') {e = document.getElementById(elm);} else {e=elm;}
    
    if(e === null){call(errCallback, VError.NULL, null, elm, minLength); return null;}
    if(!e.checkValidity())
        {
            call(errCallback,e.validationMessage, e, elm);
            return null;
        }
    v = get(e);
    if(e.tagName === 'INPUT')
        {
            if(t == 'search') return true;
            /*Validate length*/
            if(v.length < minLength || v.length == 0 && e.getAttribute("required")!==null ) { call(errCallback, VError.LESS, e, elm, minLength); return null;}
            else if(v=="") return "";
            /*validate type*/
            var t = e.type; 
            if(t === 'text' || t === 'password')
                {
                    if(e.hasAttribute("data-float"))
                        {
                            if( !isNaN(Number(v)) ) return v;
                            call(errCallback, VError.NUMBER, e, elm);
                            return null;
                        }
                    if(e.getAttribute("data-letter") !== null && !/^[A-Za-zؤـا-يألإلآىآءئ]+$/.test(v))
                        {
                            call(errCallback,VError.LETTER, e, elm);
                            return null;
                        }
                    
                    if(e.getAttribute("data-letter-number") !== null && !/^[0-9a-zA-Zؤـا-يألإلآىآءئ]+$/.test(v))
                        {
                            call(errCallback,VError.LETNUM, e, elm);
                            return null;
                        }
                    if(e.getAttribute("data-words") !== null && !/^[0-9a-zA-Zؤـا-يألإلآىآءئ \.-_\*\+\%\$\\/\^\#\!\~\(\)\&\|\@,;:\?]+$/.test(v))
                        {
                            call(errCallback,VError.WORD, e, elm);
                            return null;
                        }
                    return e.value;//success
                }
            if(t === 'date'|| t==='datetime')
                {
                    var d = new Date(Date.parse(v));
                    if(!d)
                        {
                            call(errCallback, VError.DATE, e, elm, minLength);
                            return null;
                        }
                    nv = (d.getMonth()+1)+"/"+ d.getDate()+"/"+d.getFullYear();
                    if(t==='datetime') nv = nv+" "+d.getHours()+":"+d.getMinutes()+":"+d.getSeconds();
                    
                    return nv;
                }
            if(t==='number')
                {
                    var Max = parseFloat(e.max), Min = parseFloat(e.min);
                    if(isNaN(Max)) Max = Infinity;
                    if(isNaN(Min)) Min = -Infinity;
                    if(  !isNaN(Number(v))  )
                        {
                            if(v > Max)
                                {
                                    call(errCallback, VError.MAX, e, elm);
                                    return null;
                                }
                            if( v < Min )
                                {
                                    call(errCallback, VError.MIN, e, elm);
                                    return null;
                                }
                            /*if int*/
                            if(e.getAttribute("data-int")!== null && v.indexOf(".") !== -1)
                                {
                                    call(errCallback, VError.INT, e, elm);
                                    return null;
                                }
                            return v;//success
                        }
                    else{
                        call(errCallback, VError.NUMBER, e, elm);
                        return null;
                    }
                }
            if(t==='email')
                {
                    if(!(/^\w+([\.-]?\ w+)*@\w+([\.-]?\ w+)*(\.\w{2,3})+$/.test(v)))
                        {
                            call(errCallback, VError.MAIL, e,elm);
                            return null;
                        }
                    return v;
                }
            if( t === 'tel'){
                if(!(/^\d*$/.test(v)))
                    {
                        call(errCallback, VError.TEL, e, elm);
                        return null;
                    }
                return v;
            }
            if(t==='time')
                {
                    if(/^\s*([01]?\d|2[0-3]):?([0-5]\d)\s*$/.test(v))
                        return v;
                    call(errCallback, VError.TIME,e, elm);
                    return null;
                }
            if(t==='range')
                {
                    var Max = parseFloat(e.max), Min = parseFloat(e.min);
                    if(isNaN(Max)) Max = Infinity;
                    if(isNaN(Min)) Min = -Infinity;
                    if(!isNaN(Number(v)))
                        {
                            if(v > Max || v < min){ return v; }
                            call(errCallback, VError.RANG, e, elm);
                            return null;
                        }
                }
        }
    
    return v;
}

function ValidateForm(frm, err){
	var inputs = selectAll(frm, "input" );
	for(var i=0; i<= inputs.length; i++ )
		{
			if(inputs[i] && ValidateInput(inputs[i], err)===null) return false;
		}
	return true;
}

function AutoValidate(err, msgonly){
     var ps = document.getElementsByTagName("input");
    for(var i = 0; i<ps.length; i++)
        if(ps[i].type == 'search') return;
    
        else if(ps[i].getAttribute("type")==="number"|| ps[i].type==="tel" || ps[i].hasAttribute("data-float") || ps[i].hasAttribute("data-int")){
                ps[i].addEventListener("blur", function(){
                    if(ValidateInput(this, err)===null){
                        if(this.OldValue !== undefined)
                            this.value = this.OldValue;
                        else 
                            this.value = "";
                        }
        
                        this.OldValue = get(this);
                    });
          
                ps[i].addEventListener("keydown", function(ev){
                        if((ev.key > '9' || ev.key<'0') && ev.keyCode != 8 && ev.keyCode != 27 && ev.keyCode != 13 && 
                            (ev.key != "Decimal" || this.type == "tel" || this.hasAttribute("data-int") )
                          )
                        ev.preventDefault()  
                });
        }
  else
     ps[i].addEventListener("change", function(){
        if(!ValidateInput(this, err) && !msgonly)
           this.style.background = "rgba(50,0,0,1.00)";
        else if(!msgonly) this.style.background = "rgba(15,34,9,1.00)";
    });
}
