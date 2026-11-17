const button = document.getElementById("testButton");
const content = document.getElementById("content");
const image = document.getElementById("mainImage");
const eventStatus = document.getElementById("eventStatus");

button.addEventListener("click", function simpleHandler() {
  if (button.innerHTML === "Hide") {
    button.innerHTML = "Show";
    content.className = "makeItNormal makeItDisappear";
    eventStatus.textContent = "Đang chạy hiệu ứng ẩn; sau 1 giây đoạn văn sẽ được thu gọn.";
    window.setTimeout(() => {
      content.classList.add("hidden-demo");
    }, 1000);
  } else {
    button.innerHTML = "Hide";
    content.className = "makeItNormal";
    content.style.display = "block";
    content.classList.remove("hidden-demo");
    content.classList.add("makeItDisappear");
    window.setTimeout(() => {
      content.className = "makeItNormal";
      eventStatus.textContent = "Đã hiện lại nội dung.";
    }, 30);
  }
});

image.addEventListener("mouseover", function (event) {
  event.currentTarget.className = "exercise-image makeItGray";
});
image.addEventListener("mouseout", function (event) {
  event.currentTarget.className = "exercise-image makeItNormal";
});
