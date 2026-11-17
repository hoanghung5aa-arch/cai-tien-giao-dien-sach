$(function () {
  const localUrl = "../data/cities.json"; // JSON mẫu tĩnh cùng cấu trúc, không phải response live.
  const originalUrl = "https://www.randyconnolly.com/funwebdev/services/travel/cities.php"; // Bản HTTPS của endpoint HTTP trong handout.
  const $loader = $(".animLoading");
  const $status = $("#requestStatus");

  function renderCities(data) {
    if (!Array.isArray(data)) {
      $status.text("Response không có dạng mảng JSON như tài liệu mô tả.");
      return false;
    }
    const $list = $("<ul>").addClass("city-list");
    $.each(data, function (index, value) {
      $("<li>").text(value.name).appendTo($list);
    });
    $("#results").empty().append($list);
    return true;
  }

  function requestCities(url, isOriginal) {
    let fallbackPending = false;
    $("#results").empty();
    $status.text("Đang gửi GET request…");
    $loader.show();

    $.get(url)
      .done(function (data) {
        if (renderCities(data)) {
          $status.text(isOriginal ? "Endpoint trong tài liệu đã trả dữ liệu." : "GET JSON cùng origin thành công.");
        }
      })
      .fail(function (xhr, status, error) {
        if (isOriginal) {
          fallbackPending = true;
          $status.text("Endpoint HTTPS lỗi hoặc bị chặn (" + status + "); đang tải JSON local dự phòng.");
          $.get("../data/cities.json")
            .done(function (data) {
              if (renderCities(data)) $status.text("Đã tải JSON local dự phòng sau khi endpoint lỗi.");
            })
            .fail(function (fallbackXhr, fallbackStatus, fallbackError) {
              $status.text("Endpoint và JSON local đều lỗi: " + fallbackStatus + (fallbackError ? " · " + fallbackError : ""));
            })
            .always(function () {
              fallbackPending = false;
              $loader.fadeOut("slow");
            });
        } else {
          $status.text("GET JSON lỗi: " + status + (error ? " · " + error : ""));
        }
      })
      .always(function () {
        if (!fallbackPending) $loader.fadeOut("slow");
      });
  }

  $("#loadLocal").on("click", function () { requestCities(localUrl, false); });
  $("#loadOriginal").on("click", function () { requestCities(originalUrl, true); });
});
