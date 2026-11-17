$(function () {
  const $box = $("#box");
  const $status = $("#complexStatus");

  function resetBox() {
    $box.stop(true, true).css({ left: "0px", top: "0px", opacity: .45, height: "80px", width: "80px" });
    $status.text("Hộp đã được đưa về vị trí ban đầu.");
  }

  $("#moveBox").on("click", function () {
    resetBox();
    $box.animate({ left: "495px" }, 1000);
    $status.text("Đang animate thuộc tính left tới 495px.");
  });

  $("#multiProps").on("click", function () {
    resetBox();
    $box.animate({ left: "495px", opacity: "1", height: "50px" }, 1200);
    $status.text("Đang animate left, opacity và height cùng lúc.");
  });

  $("#sequenceBox").on("click", function () {
    resetBox();
    $box
      .animate({ left: "495px", opacity: "1", height: "50px" }, 1000)
      .animate({ top: "400px", opacity: ".2" }, 1000)
      .animate({ height: "200px" }, 2000, function () {
        $status.text("Chuỗi ba animation đã hoàn tất.");
      });
    $status.text("Đang chạy lần lượt ba animation đã nối chuỗi.");
  });

  $("#resetBox").on("click", resetBox);
});
