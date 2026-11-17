const button = document.getElementById("testButton");
const content = document.getElementById("content");
const image = document.getElementById("mainImage");
const eventStatus = document.getElementById("eventStatus");
let hideTimer = null;

button.addEventListener("click", function simpleHandler() {
  if (hideTimer !== null) {
    window.clearTimeout(hideTimer);
    hideTimer = null;
  }

  if (button.innerHTML === "Hide") {
    button.innerHTML = "Show";
    content.className = "makeItNormal makeItDisappear";
    eventStatus.textContent = "Đang chạy hiệu ứng ẩn; sau 1 giây đoạn văn sẽ được thu gọn.";
    hideTimer = window.setTimeout(() => {
      content.classList.add("hidden-demo");
      hideTimer = null;
    }, 1000);
  } else {
    button.innerHTML = "Hide";
    content.classList.remove("hidden-demo");
    content.className = "makeItNormal makeItDisappear";
    // Reflow at the collapsed state so the transition can run when the class changes.
    void content.offsetHeight;
    window.setTimeout(() => {
      content.className = "makeItNormal";
      eventStatus.textContent = "Đã hiện lại nội dung.";
    }, 0);
  }
});

image.addEventListener("mouseover", function (event) {
  event.currentTarget.className = "exercise-image makeItGray";
});
image.addEventListener("mouseout", function (event) {
  event.currentTarget.className = "exercise-image makeItNormal";
});
