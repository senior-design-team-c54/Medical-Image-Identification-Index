//JS file that takes an input zip file of dicom images and sends their headers to the server.
//

function loadFiles(event) {
    var file =event.target.files[0];
    readZip(file);
}

function readZip(file,zipFileFunc = zipParser) {
    
    var ext = /^.+\.([^.]+)$/.exec(file.name); //get extension
    console.log(file);
    console.log(ext);
	//if zip	
    var zip = new JSZip();
    zip.loadAsync(file)
	.then(data => zipFileFunc(data), function () { alert("Not a valid zip file") }); 
  
	
	

}


function zipParser(zip) {
   // console.log(zip);
    for (f in zip.files) {
		var file = zip.files[f];
		//find extension
		console.log(file);
		if (file.name.endsWith('/')) {
            //is folder
            continue;
        }
		//uses regex expression to find extension, or "" if nothing
		var ext = /^.+\.([^.]+)$/.exec(file.name);
        if (ext == null || ext.toLowerCase() == "dcm") {
            console.log("PARSE");
			var name = file.name;
			//should be a dicom file due to extension. Dont mess it up
            file.internalStream("uint8array")
                .accumulate((metadata) => { }) //metadata contains 'percent' and 'currentFile'
                .then((data) => { saveAs(parseByteArray(data)); });
			
		}
		
		
    }
}


function parseByteArray(byteArray){
    var sansImg = cutOutImgTag(byteArray);
    //return sansImg.buffer;
	var blob = new Blob([sansImg.buffer],{type: "octet/stream"});
    return blob;

	
}

function cutOutImgTag(byteArray){
	var index = findHexArr(byteArray,[0xE0,0x7F,0x10,0x00]);
	console.log(index);
	if(index == -1){
		alert("Could not find Image tag");
		return;
	}
	var sansImg = new Uint8Array(index);
	sansImg.set(byteArray.subarray(0,index));
	return sansImg;
	
}


function findHexArr(data,hexArr){
	var start =0;
	var length = hexArr.length;
	var success = true;
	while(start != -1 && start < data.length - length){
		start = data.indexOf(hexArr[0],start);
		success = true;
		for(var i =1; i < length; i++){
			if(hexArr[i] != data[start+i]){
				success = false; 
				break;
			}
		}
		
		if(success){
			return start;
		}
		start++;
	}
	return -1;
}


