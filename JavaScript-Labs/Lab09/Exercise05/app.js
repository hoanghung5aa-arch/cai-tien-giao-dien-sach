// Đăng ký phần xử lý load trước khi thử truy cập sớm, để trang còn tự hiện được kết quả sau lỗi.
const earlyLookup = document.getElementById("div1");
window.addEventListener("load", function () {
  const earlyResult = document.getElementById("earlyResult");
  const loadedResult = document.getElementById("loadedResult");
  const alertButton = document.getElementById("showLoadAlert");
  const divToGet = document.getElementById("div1");

  earlyResult.textContent = earlyLookup
    ? "Lúc script chạy, #div1 đã có sẵn."
    : "Lúc script chạy trong head, chưa tìm thấy #div1 (giá trị là null).";
  loadedResult.textContent = `Sau sự kiện load, nội dung #div1 là: ${divToGet.innerHTML}`;
  alertButton.disabled = false;
  alertButton.addEventListener("click", function () {
    alert(divToGet.innerHTML);
  });
  console.log("DOM lookup before load:", earlyLookup);
  console.log("DOM lookup after load:", divToGet);
});

// Giống bước thử đầu trong handout: script chạy ở head trước khi #div1 được phân tích.
// Lỗi TypeError xuất hiện trong Console; listener load ở trên vẫn được gọi sau khi trang tải.
const divToGet = document.getElementById("div1");
const earlyHTML = divToGet.innerHTML;
console.log("This line is reached only if #div1 existed early:", earlyHTML);
