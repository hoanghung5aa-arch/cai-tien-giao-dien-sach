var boxClass = "movingDiv";

function outputBoxDirect() {
  var boxElement = document.createElement("div");
  boxElement.className = boxClass;
  boxElement.id = "div1";
  boxElement.textContent = "This is div 1";
  document.getElementById("boxes-output").replaceChildren(boxElement);
}

function outputBoxReturn() {
  var box = '<div class="' + boxClass + '" id="div1">This is div 1</div>';
  return box;
}

function outputBox(num) {
  var box = '<div class="' + boxClass + '" id="div' + num + '">This is div ' + num + "</div>";
  return box;
}

function outputBoxWithoutVar(num) {
  box = '<div class="' + boxClass + '" id="global-div' + num + '">This is div ' + num + "</div>";
  return box;
}

function showDirectOutput() {
  outputBoxDirect();
  document.getElementById("scope-output").textContent =
    "Bước 1: hàm tự tạo và xuất một hộp vào DOM.";
}

function showReturnedOutput() {
  var output = document.getElementById("boxes-output");
  output.innerHTML = outputBoxReturn();
  document.getElementById("scope-output").textContent =
    "Bước 2: outputBoxReturn() trả chuỗi HTML; trang chèn chuỗi trả về.";
}

function showParameterizedOutput() {
  var output = document.getElementById("boxes-output");
  var markup = "";
  for (var count = 1; count < 6; count++) {
    markup += outputBox(count);
  }
  output.innerHTML = markup;
  document.getElementById("scope-output").textContent =
    "Bước 3: outputBox(num) được gọi trong vòng lặp từ 1 đến 5.";
}

function inspectBox() {
  try {
    console.log(box);
    document.getElementById("scope-output").textContent =
      "Console đã đọc được box. Nếu chưa chạy bản bỏ var, kiểm tra lỗi ReferenceError trong Console.";
  } catch (error) {
    console.error(error);
    document.getElementById("scope-output").textContent =
      "Đọc box ngoài hàm gây " + error.name + ". box là biến cục bộ của outputBox().";
  }
}

document.getElementById("direct-output").addEventListener("click", showDirectOutput);
document.getElementById("return-output").addEventListener("click", showReturnedOutput);
document.getElementById("parameter-output").addEventListener("click", showParameterizedOutput);
document.getElementById("inspect-box").addEventListener("click", inspectBox);
document.getElementById("without-var").addEventListener("click", function () {
  outputBoxWithoutVar(1);
  console.log("After removing var, box is now global:", box);
  document.getElementById("boxes-output").innerHTML = box;
  document.getElementById("scope-output").textContent =
    "Bản bỏ var đã tạo biến global box. Mở Console và nhập box; sau đó tải lại trang để xóa biến thử nghiệm.";
});
showParameterizedOutput();
