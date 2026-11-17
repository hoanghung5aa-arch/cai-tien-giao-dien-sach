function init() {
  document.getElementById("sampleForm").addEventListener("submit", checkForEmptyFields);
}

window.addEventListener("load", init);

function checkForEmptyFields(e) {
  const errorArea = document.getElementById("errors");
  const cssSelector = "input[type=text], input[type=password]";
  const fields = document.querySelectorAll(cssSelector);
  const fieldList = [];

  errorArea.className = "form-message hidden";
  fields.forEach((field) => {
    field.classList.remove("field-error");
    if (field.value.trim() === "") fieldList.push(field);
  });

  if (fieldList.length > 0) {
    e.preventDefault();
    const missingNames = fieldList.map((field) => field.labels[0].childNodes[0].textContent.trim());
    errorArea.innerHTML = `<p>Các trường sau không được để trống:</p><ul>${missingNames.map((name) => `<li>${name}</li>`).join("")}</ul>`;
    errorArea.className = "form-message visible";
    fieldList.forEach((field) => field.classList.add("field-error"));
    fieldList[0].focus();
    return;
  }

  e.preventDefault();
  errorArea.textContent = "Các trường đã có dữ liệu. Đây là ví dụ kiểm tra ở phía trình duyệt; chưa gửi lên máy chủ.";
  errorArea.className = "form-message success";
}
