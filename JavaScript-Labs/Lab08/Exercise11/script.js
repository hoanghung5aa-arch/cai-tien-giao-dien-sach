var countries=[
{name:"Bahamas",capital:"Nassau",population:"khoảng 400 nghìn",iso:"BS",continent:"North America"},
{name:"Canada",capital:"Ottawa",population:"khoảng 40 triệu",iso:"CA",continent:"North America"},
{name:"Germany",capital:"Berlin",population:"khoảng 84 triệu",iso:"DE",continent:"Europe"},
{name:"Spain",capital:"Madrid",population:"khoảng 49 triệu",iso:"ES",continent:"Europe"},
{name:"United Kingdom",capital:"London",population:"khoảng 69 triệu",iso:"GB",continent:"Europe"},
{name:"Ghana",capital:"Accra",population:"khoảng 35 triệu",iso:"GH",continent:"Africa"},
{name:"Greece",capital:"Athens",population:"khoảng 10 triệu",iso:"GR",continent:"Europe"},
{name:"Hungary",capital:"Budapest",population:"khoảng 9.6 triệu",iso:"HU",continent:"Europe"},
{name:"Italy",capital:"Rome",population:"khoảng 59 triệu",iso:"IT",continent:"Europe"},
{name:"United States",capital:"Washington, D.C.",population:"khoảng 340 triệu",iso:"US",continent:"North America"}
];
function renderCountries(){var output=document.getElementById("countries-output");output.replaceChildren();for(var i=0;i<countries.length;i++){var country=countries[i],article=document.createElement("article");article.className="country-card";
var image=document.createElement("img");image.src="../assets/flags/"+country.iso+".png";image.alt=country.name+" flag";var title=document.createElement("strong");title.append(image,document.createTextNode(country.name));article.append(title);
var list=document.createElement("dl");for(var propertyName in country){if(propertyName==="iso")continue;var dt=document.createElement("dt"),dd=document.createElement("dd");dt.textContent=propertyName;dd.textContent=country[propertyName];list.append(dt,dd);}article.append(list);output.append(article);}}
renderCountries();