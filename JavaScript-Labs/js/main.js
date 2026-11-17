const year = document.querySelector("[data-current-year]");
if (year) year.textContent = new Date().getFullYear();

const navLinks = document.querySelectorAll(".nav-links a[href^='#']");
navLinks.forEach((link) => {
  link.addEventListener("click", () => {
    const target = document.querySelector(link.getAttribute("href"));
    if (target) target.scrollIntoView({ behavior: "smooth", block: "start" });
  });
});
