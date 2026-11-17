window.count=2; window.output="JavaScript Console";
console.log("script linked from the body"); console.warn("sample warning from Exercise 8.5"); console.error("sample error message from Exercise 8.5");
document.getElementById("console-result").textContent="Mở console và thử count, output, var temp = count * 7, temp (kết quả temp là 14).";
document.getElementById("console-error-button").addEventListener("click",function(){console.error("Exercise 8.5: đây là dòng lỗi mẫu để tập đọc console.");});