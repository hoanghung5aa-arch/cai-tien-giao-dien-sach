$(function () {
  const $message = $("#eventMessage");

  $("#shortcutPanel").click(function () {
    $message.html("Bạn đã click panel A (click shorthand).");
  });

  $("#onPanel").on("click", function () {
    $message.html("Bạn đã click panel B bằng .on().");
  });

  $("#chainPanel")
    .on("mousemove", function (event) {
      $message.html("x=" + event.pageX + " · y=" + event.pageY);
    })
    .on("mouseleave", function () {
      $message.html("goodbye!");
    })
    .on("click", function () {
      $message.html("Đã dừng theo dõi mousemove.");
      $("#chainPanel").off("mousemove");
    });
});
