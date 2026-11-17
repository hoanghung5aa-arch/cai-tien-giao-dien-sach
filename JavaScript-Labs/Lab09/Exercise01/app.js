const msg = document.getElementById("msgArea");
const status = document.getElementById("actionStatus");

document.querySelectorAll("[data-action]").forEach((button) => {
  button.addEventListener("click", () => {
    const action = button.dataset.action;
    if (action === "title") {
      const title = document.getElementById("pageTitle");
      title.innerHTML = title.innerHTML === "DOM Example" ? "New Title" : "DOM Example";
      status.textContent = "Đã đổi tiêu đề bằng innerHTML.";
    } else if (action === "html") {
      msg.value = document.getElementById("first").innerHTML;
      status.textContent = "innerHTML giữ lại markup bên trong #first.";
    } else if (action === "text") {
      msg.value = document.getElementById("first").innerText;
      status.textContent = "innerText lấy nội dung chữ hiển thị trong #first.";
    } else if (action === "attribute") {
      msg.value = "Class của textarea là " + msg.getAttribute("class");
      status.textContent = "Đã đọc thuộc tính class bằng getAttribute().";
    } else if (action === "tag") {
      const textareas = document.getElementsByTagName("textarea");
      textareas[0].value = "Đã chọn phần tử đầu tiên theo tên tag textarea.";
      status.textContent = `Tìm thấy ${textareas.length} textarea.`;
    } else if (action === "spans") {
      const spans = document.getElementsByTagName("span");
      msg.value = Array.from(spans, (span) => span.innerText).join("\n");
      status.textContent = `Đã đọc ${spans.length} phần tử span.`;
    } else if (action === "class") {
      const counts = document.getElementsByClassName("count-number");
      msg.value = Array.from(counts, (count) => count.innerText).join("\n");
      status.textContent = `Đã tìm thấy ${counts.length} phần tử .count-number.`;
    } else if (action === "query-one") {
      const paragraph = document.querySelector("#first p");
      msg.value = paragraph.innerHTML;
      status.textContent = "querySelector trả về phần tử đầu tiên khớp selector.";
    } else if (action === "query-all") {
      const paragraphs = document.querySelectorAll("#first p");
      msg.value = Array.from(paragraphs, (paragraph) => paragraph.innerHTML).join("\n");
      status.textContent = `querySelectorAll trả về ${paragraphs.length} đoạn.`;
    } else if (action === "hide") {
      msg.setAttribute("class", "demo-textarea hidden-demo");
      status.textContent = "Textarea đã được ẩn bằng setAttribute(). Bấm Hiện textarea để tiếp tục.";
    } else if (action === "show") {
      msg.setAttribute("class", "demo-textarea");
      status.textContent = "Textarea đã hiện lại.";
    } else if (action === "green") {
      document.getElementById("features").style.backgroundColor = "#dceee5";
      status.textContent = "Đã đổi backgroundColor của #features.";
    }
  });
});
