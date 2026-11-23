document.addEventListener("DOMContentLoaded", () => {
    // The server validator accepts ReleaseDate only when it is after today.
    document.querySelectorAll('input[type="date"][name="ReleaseDate"]').forEach((input) => {
        if (input.dataset.minDateTomorrow === "true") return;
        const parts = input.min.split("-").map(Number);
        if (parts.length !== 3 || parts.some(Number.isNaN)) return;

        const tomorrow = new Date(Date.UTC(parts[0], parts[1] - 1, parts[2] + 1));
        const year = tomorrow.getUTCFullYear();
        const month = String(tomorrow.getUTCMonth() + 1).padStart(2, "0");
        const day = String(tomorrow.getUTCDate()).padStart(2, "0");
        input.min = `${year}-${month}-${day}`;
    });

    document.querySelectorAll("[data-image-preview-input]").forEach((input) => {
        const preview = input.closest(".field")?.querySelector("[data-image-preview]");
        if (!preview) return;

        let previewUrl = null;
        input.addEventListener("change", () => {
            if (previewUrl) {
                URL.revokeObjectURL(previewUrl);
                previewUrl = null;
            }

            const file = input.files?.[0];
            if (!file || !file.type.startsWith("image/")) {
                preview.removeAttribute("src");
                preview.classList.remove("is-visible");
                return;
            }

            const objectUrl = URL.createObjectURL(file);
            previewUrl = objectUrl;
            preview.onload = () => {
                URL.revokeObjectURL(objectUrl);
                if (previewUrl === objectUrl) previewUrl = null;
            };
            preview.src = objectUrl;
            preview.classList.add("is-visible");
        });
    });
});
