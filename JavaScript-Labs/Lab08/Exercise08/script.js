var boxClass="movingDiv";
function outputBox(num){var box='<div class="'+boxClass+'" id="div'+num+'">This is div '+num+"</div>";return box;}
function renderBoxes(){var output=document.getElementById("boxes-output"),markup="";for(var count=1;count<6;count++){markup+=outputBox(count);}output.innerHTML=markup;document.getElementById("scope-output").textContent="Tạo 5 hộp bằng outputBox(count). box là biến cục bộ; boxClass nằm ngoài hàm.";}
document.getElementById("boxes-button").addEventListener("click",renderBoxes);renderBoxes();console.log("Exercise 8.8: box is local inside outputBox().");