$(function () {
  const $figure = $("figure");
  const $status = $("#animationStatus");
  $figure.hide();

  $("#btnFadeIn").click(function () {
    $figure.stop(true, true).fadeIn(1000, function () {
      $status.text("fadeIn hoàn tất.");
    });
    $status.text("Đang chạy fadeIn(1000).");
  });
  $("#btnFadeOut").click(function () {
    $figure.stop(true, true).fadeOut(1000, function () {
      $status.text("fadeOut hoàn tất.");
    });
    $status.text("Đang chạy fadeOut(1000).");
  });
  $("#btnFade").click(function () {
    $figure.stop(true, true).fadeToggle(500, function () {
      $status.text("fadeToggle hoàn tất.");
    });
    $status.text("Đang chạy fadeToggle(500).");
  });
  $("#btnSlideDown").click(function () {
    $figure.stop(true, true).slideDown("slow", function () {
      $status.text("slideDown hoàn tất.");
    });
    $status.text("Đang chạy slideDown('slow').");
  });
  $("#btnSlideUp").click(function () {
    $figure.stop(true, true).slideUp("slow", function () {
      $status.text("slideUp hoàn tất.");
    });
    $status.text("Đang chạy slideUp('slow').");
  });
  $("#btnSlide").click(function () {
    $figure.stop(true, true).slideToggle(2000, function () {
      $status.text("slideToggle hoàn tất.");
    });
    $status.text("Đang chạy slideToggle(2000).");
  });
});
