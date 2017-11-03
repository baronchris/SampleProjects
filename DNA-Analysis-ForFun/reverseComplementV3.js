function info(){
    alert("Sequence analysis software V3 \n By Chris Williams");
}

function Complementfinder(initSequence){
var initSequence= document.forms["sequencesubmit"]["initSequence"].value;
var complement = [];
var current="";
var resultString="";
var reverseComplement=[];
var initupper =initSequence.toUpperCase();
var strandLength =initupper.length;
var Acount=0;
var Tcount=0;
var Ccount=0;
var Gcount=0;
var Ncount=0;
var finalLength=0;
var Apercent =0;
var Tpercent=0;
var Cpercent=0;
var Gpercent=0;
    for(var i=0; i<strandLength; i++){
        if(initupper[i] !== "A" || initupper[i] !=="C" 
        || initupper[i]!=="T" ||initupper[i]!=="G" || initupper[i]!=="N"){
            current="";
        }
        if(initupper[i]=="C"){
            current="G";
            Ccount++;
        }
        if(initupper[i]=="G"){
            current="C";
            Gcount++;
        }
        if(initupper[i]=="A"){
            current="T";
            Acount++;
        }
        if(initupper[i]=="T"){
            current="A";
            Tcount++;
        }
        if(initupper[i]=="N"){
            current="N";
            Ncount++;
        }
        complement[i]=current;
    }
    for(var j=0; j<(complement.length); j++){
        var revIndex=((complement.length)-j);
        reverseComplement[revIndex]=complement[j];
    }
    reverseComplement.splice(0,1);
    resultString=reverseComplement.join("");
    finalLength=resultString.length;
    Apercent=((Acount/finalLength)*100);
    Tpercent=((Tcount/finalLength)*100);
    Cpercent=((Ccount/finalLength)*100);
    Gpercent=((Gcount/finalLength)*100);
    var readingFrame =" ";
    readingFrame=TSSFinder(initupper);
    var ORFLength=readingFrame.length;
    var RNASeq=" "
    RNASeq=readingFrame.replace(/T/g,"U");
    document.getElementById("resultsHolder").style.display ="block";
    document.getElementById("seqLength").innerText = strandLength;
    document.getElementById("Aprev").innerText = Apercent;
    document.getElementById("Tprev").innerText = Tpercent;
    document.getElementById("Cprev").innerText = Cpercent;
    document.getElementById("Gprev").innerText = Gpercent;
    document.getElementById("ORFSize").innerText = ORFLength;
    document.getElementById("longinput").innerHTML = initSequence;
    document.getElementById("longResult").innerHTML =resultString;
    document.getElementById("ORF").innerHTML =readingFrame;
    document.getElementById("mRNAResult").innerHTML =RNASeq;
    return false;
}

function transcription(resultString){
    var resultlength =resultString.length;
    var mRNASeq= resultString.replace("T","U");
    for(var m=0; m < resultlength; m++ ){
        if(mRNASeq[m]=="T"){
            mRNASeq.splice(mRNASeq.length,1,"U");
        }
    }
    return mRNASeq;
}
function TSSFinder(initSequence){
    var TSS=initSequence.indexOf("ATG");
    var ORFArray =[];
    var TSSadjust=TSS+1;  //since base counting starting at zero is not biological convention
    var finalArray=[];
    document.getElementById("TSSloc").innerText = TSSadjust;
    for(i=TSS; i<initSequence.length; i++){
        ORFArray[ORFArray.length]=initSequence[i];
    }
    console.log(ORFArray);  
    var tripletstring =ORFArray.join("");
    var codonArray=[];
    codonArray=tripletstring.match(/.{1,3}/g);
    var amber =codonArray.indexOf("TAG");
    var ochre =codonArray.indexOf("TAA");
    var opal =codonArray.indexOf("TGA")
    var stopArray =[amber, ochre, opal];
    for(j=0; j<3; j++){
        if(stopArray[j]<1){
            stopArray.splice(j,1);
        }
    }
    document.getElementById("stopCodonLoc").innerText=stopArray;  
    var stopCodon =Math.min(...stopArray);
    for(var k=0; k<stopCodon+3; k++){
        finalArray[finalArray.length]=codonArray[k];
    }
    var finalString=finalArray.join("");
    return finalString;
}
/*  for splitting strings into chunks of 3 try as codonArray
var str = 'abcdefghijkl';
 console.log(str.match(/.{1,3}/g)); */

function resetForm() {
    complement =[];
    reverseComplement=[];
    resultString = "";
     document.getElementById("resultsHolder").style.display ="none";
    document.forms["sequencesubmit"]["initSequence"].value = "";
    document.getElementById("submitButton").innerText = "Submit";
    document.forms["sequencesubmit"]["initSequence"].focus();
}