// Script này được nạp đồng bộ trong phần head để minh họa truy xuất quá sớm.
const earlyLookup = document.getElementById("div1");

window.addEventListener("load", function () {
  const earlyResult = document.getElementById("earlyResult");
  const loadedResult = document.getElementById("loadedResult");
  const divToGet = document.getElementById("div1");

  earlyResult.textContent = earlyLookup
    ? "Lúc script chạy, #div1 đã có sẵn."
    : "Lúc script chạy trong head, chưa tìm thấy #div1 (giá trị là null).";
  loadedResult.textContent = `Sau sự kiện load, nội dung #div1 là: ${divToGet.innerHTML}`;
  console.log("DOM lookup before load:", earlyLookup);
  console.log("DOM lookup after load:", divToGet);
});
