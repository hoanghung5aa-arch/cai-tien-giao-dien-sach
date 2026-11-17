document.getElementById("hello-button").addEventListener("click", function () {
  window.showHelloWorld();
  document.getElementById("hello-status").textContent =
    "Đã gọi lại alert từ hàm được định nghĩa trong phần head.";
});
