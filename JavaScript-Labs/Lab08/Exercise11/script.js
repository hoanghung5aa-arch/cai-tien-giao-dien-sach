function Country(name, iso, capital, population) {
  this.name = name;
  this.iso = iso;
  this.capital = capital;
  this.population = population;
}

var countries = [
  new Country("Bahamas", "BS", "Nassau", 301790),
  new Country("Canada", "CA", "Ottawa", 33679000),
  new Country("Germany", "DE", "Berlin", 81802257)
];

countries.push(new Country("Spain", "ES", "Madrid", 46505963));
countries.push(new Country("United Kingdom", "GB", "London", 62348447));

var currentNameMode = "dot";

function getCountryName(country, mode) {
  if (mode === "dot") {
    return country.name;
  }
  if (mode === "bracket") {
    return country["name"];
  }
  var temp = "name";
  return country[temp];
}

function renderCountries() {
  var output = document.getElementById("countries-output");
  output.replaceChildren();
  var modeDescription = {
    dot: "Đang dùng country.name.",
    bracket: 'Đang dùng country["name"].',
    variable: 'Đang dùng temp = "name" rồi country[temp].'
  };
  document.getElementById("name-mode-status").textContent =
    modeDescription[currentNameMode] + " Cards được tạo bằng vòng lặp mảng và for..in.";

  for (var i = 0; i < countries.length; i++) {
    var c = countries[i];
    var article = document.createElement("article");
    article.className = "country-card";

    var image = document.createElement("img");
    image.src = "../assets/flags/" + c.iso + ".png";
    image.alt = c.name + " flag";
    var title = document.createElement("strong");
    title.append(image, document.createTextNode(getCountryName(c, currentNameMode)));
    article.append(title);

    var list = document.createElement("dl");
    for (var propertyName in c) {
      var term = document.createElement("dt");
      var value = document.createElement("dd");
      term.textContent = propertyName;
      value.textContent = c[propertyName];
      list.append(term, value);
    }
    article.append(list);
    output.append(article);
  }
}

document.querySelectorAll("[data-name-mode]").forEach(function (button) {
  button.addEventListener("click", function () {
    currentNameMode = button.dataset.nameMode;
    renderCountries();
  });
});

renderCountries();
