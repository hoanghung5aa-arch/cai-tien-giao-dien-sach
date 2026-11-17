$(function () {
  const $status = $("#advancedStatus");
  function clearMarks() { $(".selector-mark").removeClass("selector-mark"); }

  $("#passwordSelector").on("click", function () {
    clearMarks();
    $(":password").addClass("selector-mark");
    $status.text("Đã chọn các trường password bằng $(':password').");
  });

  $("#nthSelector").on("click", function () {
    clearMarks();
    $("form :nth-child(4n)").addClass("selector-mark");
    $status.text("Đã chọn các phần tử con thứ 4, 8, ... trong form bằng :nth-child(4n).");
  });

  $("#containsSelector").on("click", function () {
    clearMarks();
    $("label:contains('word')").addClass("selector-mark");
    $status.text("Đã chọn nhãn chứa chuỗi 'word' bằng :contains().");
  });

  $("#clearSelectors").on("click", function () {
    clearMarks();
    $status.text("Đã xóa highlight.");
  });
});
