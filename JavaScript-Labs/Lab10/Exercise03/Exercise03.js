$(function () {
  const $status = $("#advancedStatus");
  const $code = $("#selectorCode");

  // The handout asks to comment out each tested selector before the next one.
  // This stepper clears the previous effect and shows the commented line in the code preview.
  function clearDemoStyles() {
    $(":password").css("background-color", "");
    $("form :nth-child(4n)").css("background-color", "");
    $("label:contains('word')").css("color", "");
  }

  $("#passwordSelector").on("click", function () {
    clearDemoStyles();
    $(":password").css("background-color", "yellow");
    $code.text("$(':password').css('background-color', 'yellow');");
    $("#nthSelector").prop("disabled", false);
    $status.text("Bước 1 xong. Tài liệu yêu cầu comment selector :password trước khi làm bước 2.");
  });

  $("#nthSelector").on("click", function () {
    clearDemoStyles();
    $("form :nth-child(4n)").css("background-color", "yellow");
    $code.text("// $(':password').css('background-color', 'yellow');\n$('form :nth-child(4n)').css('background-color', 'yellow');");
    $("#containsSelector").prop("disabled", false);
    $status.text("Bước 2 xong. Selector :password đã được comment out; các phần tử con thứ 4 được tô vàng.");
  });

  $("#containsSelector").on("click", function () {
    clearDemoStyles();
    $("label:contains('word')").css("color", "black");
    $code.text("// $(':password').css('background-color', 'yellow');\n// $('form :nth-child(4n)').css('background-color', 'yellow');\n$('label:contains(\"word\")').css('color', 'black');");
    $status.text("Bước 3 xong. Hai selector trước đã được comment out; nhãn chứa 'word' chuyển sang màu đen.");
  });

  $("#clearSelectors").on("click", function () {
    clearDemoStyles();
    $("#nthSelector, #containsSelector").prop("disabled", true);
    $code.text("// Nhấn Bước 1 để chạy selector :password.");
    $status.text("Đã làm lại. Bắt đầu ở bước 1.");
  });
});
