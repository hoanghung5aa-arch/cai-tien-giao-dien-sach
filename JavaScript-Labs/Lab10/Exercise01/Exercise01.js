$(function () {
  const version = $.fn.jquery;
  $("#jqueryVersion").text(version);
  $("#jqueryStatus").text("jQuery đã sẵn sàng. Tải thành công phiên bản " + version + ".");
});
