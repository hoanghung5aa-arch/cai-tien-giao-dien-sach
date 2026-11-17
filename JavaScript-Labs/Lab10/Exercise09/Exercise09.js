$(function () {
  const localUrl = "../data/cities.json"; // JSON mẫu tĩnh cùng cấu trúc, không phải response live.
  const originalUrl = "https://www.randyconnolly.com/funwebdev/services/travel/cities.php"; // Bản HTTPS của endpoint HTTP trong handout.

  function showCities(data) {
    if (!Array.isArray(data)) {
      $("#requestStatus").text("Response không có dạng mảng JSON như tài liệu mô tả.");
      return false;
    }
    const $list = $("<ul>").addClass("city-list");
    $.each(data, function (index, value) {
      $("<li>").text(value.name).appendTo($list);
    });
    $("#results").empty().append($list);
    return true;
  }

  function loadCities(url, isOriginal, isFallback) {
    $("#results").empty();
    $("#requestStatus").text(isFallback ? "Đang tải dữ liệu JSON local dự phòng…" : "Đang gửi GET request…");

    $.get(url, function (data, status) {
      if (status === "success" && showCities(data)) {
        $("#requestStatus").text(isFallback
          ? "Endpoint trong tài liệu không truy cập được; đã tải danh sách JSON local dự phòng."
          : (isOriginal ? "GET endpoint thành công; đã hiển thị danh sách thành phố." : "GET JSON cùng origin thành công."));
      }
    }).fail(function (xhr, status, error) {
      if (isOriginal) {
        $("#requestStatus").text("Endpoint HTTPS lỗi hoặc bị chặn (" + status + "); đang chuyển sang JSON local.");
        loadCities(localUrl, false, true);
      } else {
        $("#requestStatus").text("GET JSON lỗi: " + status + (error ? " · " + error : ""));
      }
    });
  }

  $("#loadLocal").on("click", function () { loadCities(localUrl, false, false); });
  $("#loadOriginal").on("click", function () { loadCities(originalUrl, true, false); });
});
