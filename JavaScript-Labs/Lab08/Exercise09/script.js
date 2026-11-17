var isCanadian = true;

function taxRate() {
  return isCanadian ? 0.05 : 0.0;
}

function calculateTax(amount) {
  return amount * taxRate();
}

function calculateTotal(price, quantity) {
  var amount = price * quantity;
  return amount + calculateTax(amount);
}

function calculateTaxNested(amount) {
  function taxRate() {
    return isCanadian ? 0.05 : 0.0;
  }
  return amount * taxRate();
}

function calculateTotalNested(price, quantity) {
  var amount = price * quantity;
  return amount + calculateTaxNested(amount);
}

function calculateTaxNamedExpression(amount) {
  var tax = function taxRate() {
    return isCanadian ? 0.05 : 0.0;
  };
  return amount * tax();
}

function calculateTaxAnonymousExpression(amount) {
  var tax = function () {
    return isCanadian ? 0.05 : 0.0;
  };
  return amount * tax();
}

function calculateTaxCallback(amount, tax) {
  return amount * tax();
}

function calculateTotalCallback(price, quantity) {
  var amount = price * quantity;
  return amount + calculateTaxCallback(amount, function () {
    return isCanadian ? 0.05 : 0.0;
  });
}

function renderApproaches() {
  var price = Math.max(0, Number(document.getElementById("price").value) || 0);
  var quantity = Math.max(0, Number(document.getElementById("quantity").value) || 0);
  isCanadian = document.getElementById("is-canadian").checked;
  var amount = price * quantity;
  var rate = taxRate();
  var approaches = [
    ["Named global function", calculateTax(amount), calculateTotal(price, quantity), "taxRate()"],
    ["Nested named function", calculateTaxNested(amount), calculateTotalNested(price, quantity), "taxRate()"],
    ["Named function expression", calculateTaxNamedExpression(amount), amount + calculateTaxNamedExpression(amount), "taxRate()"],
    ["Anonymous function expression", calculateTaxAnonymousExpression(amount), amount + calculateTaxAnonymousExpression(amount), "function () { … }"],
    ["Anonymous callback parameter", calculateTaxCallback(amount, function () { return rate; }), calculateTotalCallback(price, quantity), "tax function parameter"]
  ];

  document.getElementById("tax-summary").textContent =
    "Subtotal $" + amount.toFixed(2) + " · tax rate " + (isCanadian ? "5%" : "0%") +
    " · isCanadian = " + isCanadian;
  var tbody = document.getElementById("tax-approaches");
  tbody.replaceChildren();
  approaches.forEach(function (row) {
    var tr = document.createElement("tr");
    [row[0], "$" + row[1].toFixed(2), "$" + row[2].toFixed(2), row[3]].forEach(function (value) {
      var td = document.createElement("td");
      td.textContent = value;
      tr.append(td);
    });
    tbody.append(tr);
  });
}

document.getElementById("price").addEventListener("input", renderApproaches);
document.getElementById("quantity").addEventListener("input", renderApproaches);
document.getElementById("is-canadian").addEventListener("change", renderApproaches);
renderApproaches();
