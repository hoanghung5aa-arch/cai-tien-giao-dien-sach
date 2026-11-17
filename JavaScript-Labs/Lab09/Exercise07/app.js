function setBackground(e) {
  if (e.type === "focus") {
    e.target.style.backgroundColor = "#FFE393";
    document.getElementById("focusStatus").textContent = `Đang focus: #${e.target.id}`;
  } else if (e.type === "blur") {
    e.target.style.backgroundColor = "white";
    document.getElementById("focusStatus").textContent = `Đã blur khỏi #${e.target.id}; nền được trả về trắng.`;
  }
}

window.addEventListener("load", function () {
  const cssSelector = "input[type=text], input[type=password]";
  const fields = document.querySelectorAll(cssSelector);
  for (let i = 0; i < fields.length; i += 1) {
    fields[i].addEventListener("focus", setBackground);
    fields[i].addEventListener("blur", setBackground);
  }
});
