function handleMovingBox(e) {
  document.getElementById("eventResult").textContent = `Sự kiện ${e.type} phát sinh từ phần tử #${e.target.id}. Xem thêm đối tượng trong Console.`;
  console.log("mouseover target:", e.target);
}

window.addEventListener("load", function () {
  const divs = document.querySelectorAll(".movingDiv");
  for (let i = 0; i < divs.length; i += 1) {
    divs[i].addEventListener("mouseover", handleMovingBox);
  }
});

document.getElementById("clearResult").addEventListener("click", () => {
  document.getElementById("eventResult").textContent = "Chưa có mouseover nào.";
});
