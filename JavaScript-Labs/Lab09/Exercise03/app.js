const first = document.getElementById("first");
const addStatus = document.getElementById("addStatus");
const deleteThisOne = document.getElementById("deleteThisOne");

document.getElementById("addParagraph").addEventListener("click", () => {
  const text = document.createTextNode("Đoạn này được tạo bằng document.createTextNode().");
  const paragraph = document.createElement("p");
  paragraph.appendChild(text);
  first.appendChild(paragraph);
  addStatus.textContent = "Đã tạo node p và nối vào #first.";
});

document.getElementById("resetFirst").addEventListener("click", () => {
  first.innerHTML = "<p>Vùng này có nội dung ban đầu.</p>";
  addStatus.textContent = "Đã khôi phục nội dung để thử lại.";
});

document.getElementById("removeItem").addEventListener("click", () => {
  const item = document.getElementById("deleteThisOne");
  if (item) {
    item.parentNode.removeChild(item);
    document.getElementById("removeStatus").textContent = "Đã xóa phần tử li bằng removeChild().";
  } else {
    document.getElementById("removeStatus").textContent = "Phần tử này đã được xóa rồi.";
  }
});

document.getElementById("restoreItem").addEventListener("click", () => {
  if (!document.getElementById("deleteThisOne")) {
    const item = document.createElement("li");
    item.id = "deleteThisOne";
    item.textContent = "📚 Sách (sẽ bị xóa)";
    document.getElementById("iconList").appendChild(item);
  }
  document.getElementById("removeStatus").textContent = "Đã thêm lại phần tử để tiếp tục thử.";
});
