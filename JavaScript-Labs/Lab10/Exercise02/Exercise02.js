$(function () {
  const $status = $("#basicStatus");

  $("#setValue").on("click", function () {
    $("#msgArea").val("Đây là nội dung mới được đặt bằng val().");
    $status.text("Đã đặt giá trị textarea bằng .val().");
  });

  $("#setHtml").on("click", function () {
    $("#first").html("Tiêu đề mới");
    $status.text("Đã thay nội dung tiêu đề bằng .html().");
  });

  $("#readAttr").on("click", function () {
    const className = $("#msgArea").attr("class");
    $("#attributeResult").text("Class của textarea: " + className);
    $status.text("Đã đọc thuộc tính class bằng .attr().");
  });

  $("#styleButtons").on("click", function () {
    $("button").css("background-color", "#bd553d");
    $status.text("Các button trên trang được tô màu đỏ bằng selector $('button').");
  });

  $("#styleBody").on("click", function () {
    $("body").css("background-color", "ivory");
    $status.text("Đã đổi màu nền body thành ivory bằng .css().");
  });

  $("#addClass").on("click", function () {
    $(".center-icons").addClass("selected");
    $status.text("Đã thêm class selected cho các phần tử .center-icons.");
  });
});
