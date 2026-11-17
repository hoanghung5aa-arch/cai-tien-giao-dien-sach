document.getElementById("console-result").textContent =
  "Khi Console mở: count → 2; output → " + window.output +
  "; var temp = count * 7 → undefined; temp → 14.";

document.getElementById("trigger-typo").addEventListener("click", function () {
  document.getElementById("error-status").textContent =
    "Đang gọi doccumment.write(...) để tạo ReferenceError. Hãy xem Console.";
  doccumment.write("this text was written from JS");
});
