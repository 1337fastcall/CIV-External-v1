/*The Body onload event*/
function fl(){
document.getElementById("Aimcardselect").style.backgroundColor="#3b61a1";
document.getElementById("Espcardselect").style.backgroundColor="#6CDAAE";
document.getElementById("Misccardselect").style.backgroundColor="#A3A3A3";
}
/*Function for the Slider, being able to put a value through sliding and tiping*/
var RCS = 0;
var Nospread = 0;
var FoV = 0;
var Smooth = 0;


function getValueSliderFoV(val) {
      
        FoV = val;
        document.getElementById("textInputFoV").value = FoV;
    
}

function setValueSliderFoV(e){
    
        if(e.keyCode == 13){
        FoV = document.getElementById("textInputFoV").value;
        document.getElementById("FoVslider").value  = FoV; 
        }
}

function setValueSliderFoVc(){
    
        
        FoV = document.getElementById("textInputFoV").value;
        document.getElementById("FoVslider").value  = FoV; 
        
}



function getValueSliderSmooth(vale) {
      
        Smooth = vale;
        document.getElementById("textInputSmooth").value = Smooth;
    
}

function setValueSliderSmooth(e){
    
        if(e.keyCode == 13){
        Smooth = document.getElementById("textInputSmooth").value;
        document.getElementById("Smoothslider").value  = Smooth; 
        }
}

function setValueSliderSmoothc(){
    
        
        Smooth = document.getElementById("textInputSmooth").value;
        document.getElementById("Smoothslider").value  = Smooth; 
        
}




function setValueRCS(){
    
    var RCSpuffer = document.getElementById("RCS").checked;
    
    
    if (RCSpuffer){
    RCS = 1;
    }
    else{
    RCS = 0;
    }

        
}



function setValueNospread(){
    
    var Nospreadpuffer = document.getElementById("Nospread").checked;
    
    
    if (Nospreadpuffer){
    Nospread = 1;
    }
    else{
    Nospread = 0;
    }
    
    
    
}




function foc(ncs){  /*ncs Number of the Cardselector*/
    
    if(ncs == 1){
        document.getElementById("Aimcardselect").style.backgroundColor="#3b61a1";
        document.getElementById("Espcardselect").style.backgroundColor="#6CDAAE";
        document.getElementById("Misccardselect").style.backgroundColor="#A3A3A3";
        
        document.getElementById("leftaim").style.marginTop="0%";
        document.getElementById("leftesp").style.marginTop="1000%";
        document.getElementById("leftmisc").style.marginTop="1000%";
        
        document.getElementById("rightaim").style.marginTop="0%";
        document.getElementById("rightesp").style.marginTop="1000%";
        document.getElementById("rightmisc").style.marginTop="1000%";
        
        document.getElementById("UPaim").style.marginTop="0%";
        document.getElementById("UPesp").style.marginTop="1000%";
        document.getElementById("UPmisc").style.marginTop="1000%";
    }
    
    if(ncs == 2){
        document.getElementById("Espcardselect").style.backgroundColor="#248f64";
        document.getElementById("Aimcardselect").style.backgroundColor="#7694D0";
        document.getElementById("Misccardselect").style.backgroundColor="#A3A3A3";
        
        document.getElementById("leftesp").style.marginTop="0%";
        document.getElementById("leftaim").style.marginTop="1000%";
        document.getElementById("leftmisc").style.marginTop="1000%";
        
        document.getElementById("rightesp").style.marginTop="0%";
        document.getElementById("rightaim").style.marginTop="1000%";
        document.getElementById("rightmisc").style.marginTop="1000%";
        
        document.getElementById("UPesp").style.marginTop="0%";
        document.getElementById("UPaim").style.marginTop="1000%";
        document.getElementById("UPmisc").style.marginTop="1000%";
    }
    
    if(ncs == 3){
        document.getElementById("Misccardselect").style.backgroundColor="#4a4a4a";
        document.getElementById("Espcardselect").style.backgroundColor="#6CDAAE";
        document.getElementById("Aimcardselect").style.backgroundColor="#7694D0";
        
        document.getElementById("leftmisc").style.marginTop="0%";
        document.getElementById("leftaim").style.marginTop="1000%";
        document.getElementById("leftesp").style.marginTop="1000%";
        
        document.getElementById("rightmisc").style.marginTop="0%";
        document.getElementById("rightaim").style.marginTop="1000%";
        document.getElementById("rightesp").style.marginTop="1000%";
        
        document.getElementById("UPmisc").style.marginTop="0%";
        document.getElementById("UPaim").style.marginTop="1000%";
        document.getElementById("UPesp").style.marginTop="1000%";
    }
}



function FoVre(){
        return FoV;
}

function Smoothre(){
        return Smooth;
}

function RCSre(){
        return RCS;
}

function Nospreadre(){
        return Nospread;
}