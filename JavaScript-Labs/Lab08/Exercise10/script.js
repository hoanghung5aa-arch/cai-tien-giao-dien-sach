var currentOrders = [];

function Order(product, price, quantity) {
  this.product = product;
  this.price = price;
  this.quantity = quantity;
  this.total = function () {
    return this.price * this.quantity;
  };
  this.output = function () {
    return "Product=" + this.product + " · Price=$" + this.price.toFixed(2) +
      " · Quantity=" + this.quantity + " · Total=$" + this.total().toFixed(2);
  };
}

function addOutputMethod(order) {
  order.output = function () {
    return "Product=" + this.product + " · Price=$" + this.price.toFixed(2) +
      " · Quantity=" + this.quantity + " · Total=$" + this.total().toFixed(2);
  };
  return order;
}

function makeOneOrder(method) {
  var order;
  if (method === "new") {
    order = new Object();
    order.product = "Self Portrait in a Straw Hat";
    order.price = 15;
    order.quantity = 2;
    order.total = function () { return this.price * this.quantity; };
  } else if (method === "bracket") {
    order = {};
    order["product"] = "Self Portrait in a Straw Hat";
    order["price"] = 15;
    order["quantity"] = 2;
    order["total"] = function () { return this.price * this.quantity; };
  } else {
    order = {
      product: "Self Portrait in a Straw Hat",
      price: 15,
      quantity: 2,
      total: function () { return this.price * this.quantity; }
    };
  }
  return addOutputMethod(order);
}

function renderOrders() {
  var list = document.getElementById("order-list");
  list.replaceChildren();
  currentOrders.forEach(function (order, index) {
    var card = document.createElement("article");
    card.className = "order-card";
    var heading = document.createElement("h3");
    heading.textContent = currentOrders.length > 1 ? "example" + (index + 1) : "order";
    var output = document.createElement("p");
    output.textContent = order.output();
    card.append(heading, output);
    list.append(card);
  });
  document.getElementById("method-result").textContent =
    "Đã chọn object. Chọn một nút để đọc hoặc gọi total.";
}

function selectMethod(method) {
  if (method === "constructor") {
    window.example1 = new Order("Self Portrait in a Straw Hat", 15, 2);
    window.example2 = new Order("Untitled #23", 10, 4);
    currentOrders = [window.example1, window.example2];
  } else {
    currentOrders = [makeOneOrder(method)];
  }
  renderOrders();
}

document.querySelectorAll("[data-order-method]").forEach(function (button) {
  button.addEventListener("click", function () {
    selectMethod(button.dataset.orderMethod);
  });
});

document.getElementById("read-method").addEventListener("click", function () {
  if (!currentOrders.length) return;
  document.getElementById("method-result").textContent =
    "order.total = " + String(currentOrders[0].total);
});

document.getElementById("call-method").addEventListener("click", function () {
  if (!currentOrders.length) return;
  document.getElementById("method-result").textContent =
    "order.total() = " + currentOrders[0].total().toFixed(2);
});

selectMethod("new");
