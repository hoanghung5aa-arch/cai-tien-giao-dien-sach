$(function () {
  $("figure img").on("mouseover", function (event) {
    $("#preview").remove();
    const alt = $(this).attr("alt");
    const src = $(this).attr("src");
    const previewSrc = src.replace("small", "medium");
    const $preview = $("<div>", { id: "preview" });
    const $image = $("<img>", { src: previewSrc, alt: alt });
    const $caption = $("<p>").text(alt);

    $preview.append($image, $caption);
    $("body").append($preview);
    $(this).addClass("gray");
    $("#preview").fadeIn(250);
    movePreview(event);
    $("#previewStatus").text("Đang xem preview: " + alt + ".");
  });

  function removePreview() {
    $(this).removeClass("gray");
    $("#preview").remove();
    $("#previewStatus").text("Preview đã đóng khi con trỏ rời ảnh.");
  }

  function movePreview(event) {
    $("#preview")
      .css("top", (event.pageY - 10) + "px")
      .css("left", (event.pageX + 30) + "px");
  }

  $("figure img").on("mouseleave", removePreview);
  $("figure img").on("mousemove", movePreview);
});
