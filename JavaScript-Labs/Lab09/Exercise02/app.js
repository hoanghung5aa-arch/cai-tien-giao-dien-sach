const stories = document.getElementById("stories");
const result = document.getElementById("result");
const report = document.getElementById("nodeReport");

document.getElementById("inspectTree").addEventListener("click", () => {
  const rows = [];
  for (let i = 0; i < stories.childNodes.length; i += 1) {
    const node = stories.childNodes[i];
    const label = node.nodeType === Node.TEXT_NODE ? "text" : node.nodeName.toLowerCase();
    rows.push(`child ${i} = ${label}; nodeType=${node.nodeType}`);
    console.log(`child ${i} =`, node, "nodeType=", node.nodeType);
  }
  report.textContent = rows.join("\n");
  result.textContent = `Đã duyệt ${stories.childNodes.length} childNodes; xem thêm trong Console.`;
});

document.getElementById("highlightNodes").addEventListener("click", () => {
  let changed = 0;
  for (let i = 0; i < stories.childNodes.length; i += 1) {
    const node = stories.childNodes[i];
    if (node.nodeType !== Node.TEXT_NODE) {
      node.style.backgroundColor = "#dceee5";
      changed += 1;
    }
  }
  result.textContent = `Đã đổi nền ${changed} node không phải text.`;
});

document.getElementById("familyRelations").addEventListener("click", () => {
  const first = document.getElementById("first");
  const firstChild = first.firstChild;
  const next = firstChild.nextSibling;
  result.textContent = `firstChild của #first là ${firstChild.nodeName} (nodeType ${firstChild.nodeType}); sibling kế tiếp là ${next.nodeName} (nodeType ${next.nodeType}); parentNode là ${first.parentNode.nodeName.toLowerCase()}.`;
  console.log("#first:", first);
  console.log("firstChild:", firstChild, "nextSibling:", next, "parentNode:", first.parentNode);
});
