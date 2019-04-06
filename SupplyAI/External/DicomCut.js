//Defined hex values for searchable tags needed
//Note: due to endianess the tags  need to be in reverse byte order to be found
//For example, pixel data is (7FE0,0010) but is represented as E07F + 10000
//



FileInput.onchange = function() {
	var file = this.files[0];
	var ext = /^.+\.([^.]+)$/.exec(file.name);
	
	//if zip
	if(ext == "zip"){
		var zip = new JSZip();
		zip.loadAsync(zipF /* = file blob */)
		.then(zipParser, function () { alert("Not a valid zip file") }); 
	}else{
		//should be single dicom file
		var reader = new FileReader();
		reader.onload = function(e) {
			var contents = e.target.result;
			console.log(contents);
			console.log(file);
			var bytes = new Uint8Array(contents);
			parseByteArray(bytes,file.name);
		};
		reader.readAsArrayBuffer(file);
		
		
	}
	
	

}


function zipParser(zip) {
    //console.log(zip);
    for (f in zip.files) {
		var file = zip.files[f];
		//find extension
		//console.log(file);
		if (file.name.endsWith('/')) {
            //is folder
            continue;
        }
		//uses regex expression to find extension, or "" if nothing
		var ext = /^.+\.([^.]+)$/.exec(file.name);
        if(ext == null || ext.toLowerCase() == "dcm"){
			var name = file.name;
			//should be a dicom file due to extension. Dont mess it up
			file.internalStream("uint8array")
			.accumulate((metadata) =>{}) //metadata contains 'percent' and 'currentFile'
			.then((data)=>{parseByteArray(data,name);});
			
		}
		
		
    }
}


function parseByteArray(byteArray,name){
	var sansImg = cutOutImgTag(byteArray);
	var blob = new Blob([sansImg.buffer],{type: "octet/stream"});
	console.log(name);
	saveAs(blob,name);
	
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


