$(function () {
  const $panel = $("#insertPanel");
  const $image = $("<img>", {
    src: "../assets/art/thumbs/13030.jpg",
    alt: "Ảnh mẫu được tạo bằng jQuery",
    class: "inserted-photo"
  });

  $panel.after($image);

  $("[data-action]").on("click", function () {
    const action = $(this).data("action");
    if (action === "after") $panel.after($image);
    if (action === "before") $panel.before($image);
    if (action === "append") $panel.append($image);
    if (action === "prepend") $panel.prepend($image);
    if (action === "appendTo") $image.appendTo($panel);
    if (action === "prependTo") $image.prependTo($panel);
    if (action === "insertBefore") $image.insertBefore($panel);
    $("#insertStatus").text("Đã chuyển ảnh bằng phương thức " + action + ".");
  });
});
