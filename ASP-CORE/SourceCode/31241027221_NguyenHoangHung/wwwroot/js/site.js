document.addEventListener("DOMContentLoaded", () => {
    const defaultBottomBannerHeight = 64;
    let bannerMeasureFrame = 0;
    const observedBannerElements = new Set();
    const bannerResizeObserver = "ResizeObserver" in window
        ? new ResizeObserver(scheduleBottomBannerMeasure)
        : null;

    function scheduleBottomBannerMeasure() {
        if (bannerMeasureFrame) return;

        bannerMeasureFrame = window.requestAnimationFrame(() => {
            bannerMeasureFrame = 0;
            measureBottomBanner();
        });
    }

    function measureBottomBanner() {
        const viewportWidth = document.documentElement.clientWidth;
        const viewportHeight = window.innerHeight;
        const maximumBannerHeight = Math.min(360, viewportHeight * 0.6);
        const minimumBannerWidth = Math.min(320, viewportWidth * 0.6);
        let bannerHeight = defaultBottomBannerHeight;
        const currentBannerElements = new Set();

        document.body.querySelectorAll("*").forEach((element) => {
            const style = window.getComputedStyle(element);
            if (style.position !== "fixed" || style.display === "none" || style.visibility === "hidden") return;

            const rect = element.getBoundingClientRect();
            if (rect.width < minimumBannerWidth || rect.height < 32 || rect.height > maximumBannerHeight) return;
            if (rect.bottom < viewportHeight - 8 || rect.top > viewportHeight - 32) return;

            currentBannerElements.add(element);
            if (bannerResizeObserver && !observedBannerElements.has(element)) {
                bannerResizeObserver.observe(element);
                observedBannerElements.add(element);
            }

            bannerHeight = Math.max(bannerHeight, Math.ceil(Math.min(rect.bottom, viewportHeight) - Math.max(rect.top, 0)));
        });

        observedBannerElements.forEach((element) => {
            if (currentBannerElements.has(element)) return;
            bannerResizeObserver?.unobserve(element);
            observedBannerElements.delete(element);
        });

        const measuredHeight = `${bannerHeight}px`;
        if (document.documentElement.style.getPropertyValue("--fixed-bottom-banner-height") !== measuredHeight) {
            document.documentElement.style.setProperty("--fixed-bottom-banner-height", measuredHeight);
        }
    }

    const bannerMutationObserver = new MutationObserver(scheduleBottomBannerMeasure);
    bannerMutationObserver.observe(document.body, { childList: true, subtree: true, attributes: true, attributeFilter: ["class", "style", "hidden"] });
    window.addEventListener("resize", scheduleBottomBannerMeasure, { passive: true });
    window.visualViewport?.addEventListener("resize", scheduleBottomBannerMeasure, { passive: true });
    window.addEventListener("load", scheduleBottomBannerMeasure, { once: true });
    scheduleBottomBannerMeasure();

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
