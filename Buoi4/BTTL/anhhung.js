// ==================================================
// ANH HÙNG JAVASCRIPT - MÈO OGGY ĐẠI CHIẾN RỒNG GIÀ
// Nguyễn Hoàng Hưng - MSSV 31241027221
// ==================================================

var name = "Nguyễn Hoàng Hưng";
var MSSV = 31241027221;
var hero = "Mèo Oggy";

// ---------- QUEST 1 + 2: TẠO NHÂN VẬT VÀ LÊN CẤP ----------
var level, xp, skillPoints, strength, agility, intel, health, phancap;

function taoNhanVat() {
    level = 1;
    xp = 0;
    skillPoints = 5;

    level++;
    xp = 150 * (level - 1) + 200;
    skillPoints = skillPoints + xp / 10;

    strength = 0.6 * skillPoints + 0.1 * xp;
    agility = 0.2 * skillPoints + 0.2 * xp;
    intel = 0.6 * skillPoints + 10 * level;

    // LEVEL UP lên cấp 3
    level = 3;
    xp = xp + 300;
}

// ---------- RỒNG GIÀ ----------
/*
==================================================
Rồng Già
--------------------------------------------------
Chậm chạp và yếu ớt hơn so với đồng loại, nhưng vẫn
nguy hiểm với khả năng phun lửa có thể gây sát thương
mạng rất cao
==================================================
*/
var DRAGON_MAX_HP = 600;
var dragonHealth, heroMaxHealth;
var dragonStr = 100;
var dragonAgi = 15;
var dragonInt = 80;

var gameContinue = false;
var dangDanh = false; // khóa nút trong lúc 1 lượt đang diễn ra
var luot = 0;

// ==================================================
// ÂM THANH (tạo bằng Web Audio API, không cần file mp3)
// ==================================================
var amThanh = true;
var audioCtx = null;

function ctx() {
    if (!audioCtx) audioCtx = new (window.AudioContext || window.webkitAudioContext)();
    if (audioCtx.state === "suspended") audioCtx.resume();
    return audioCtx;
}

// Một nốt nhạc: kiểu sóng, tần số đầu/cuối, thời lượng, âm lượng, độ trễ
function tone(type, f1, f2, dur, vol, delay) {
    var c = ctx(), t = c.currentTime + (delay || 0);
    var o = c.createOscillator(), g = c.createGain();
    o.type = type;
    o.frequency.setValueAtTime(f1, t);
    o.frequency.exponentialRampToValueAtTime(f2, t + dur);
    g.gain.setValueAtTime(vol, t);
    g.gain.exponentialRampToValueAtTime(0.001, t + dur);
    o.connect(g).connect(c.destination);
    o.start(t);
    o.stop(t + dur);
}

// Tiếng ồn qua bộ lọc: dùng cho tiếng chém, tiếng gió, tiếng lửa
function noise(dur, filterType, f1, f2, vol, delay) {
    var c = ctx(), t = c.currentTime + (delay || 0);
    var buf = c.createBuffer(1, Math.floor(c.sampleRate * dur), c.sampleRate);
    var data = buf.getChannelData(0);
    for (var i = 0; i < data.length; i++) data[i] = Math.random() * 2 - 1;
    var src = c.createBufferSource(), f = c.createBiquadFilter(), g = c.createGain();
    src.buffer = buf;
    f.type = filterType;
    f.frequency.setValueAtTime(f1, t);
    f.frequency.exponentialRampToValueAtTime(f2, t + dur);
    g.gain.setValueAtTime(vol, t);
    g.gain.exponentialRampToValueAtTime(0.001, t + dur);
    src.connect(f).connect(g).connect(c.destination);
    src.start(t);
}

var sound = {
    click: function () { tone("square", 660, 880, 0.08, 0.08); },
    slash: function () { noise(0.18, "bandpass", 3000, 600, 0.6); tone("triangle", 220, 90, 0.15, 0.25); },
    crit: function () { tone("square", 880, 1760, 0.12, 0.12, 0.12); tone("square", 1320, 2640, 0.18, 0.1, 0.22); noise(0.25, "highpass", 4000, 1500, 0.35, 0.1); },
    miss: function () { noise(0.3, "bandpass", 400, 2500, 0.4); },
    fire: function () { noise(0.6, "lowpass", 1800, 120, 0.8); tone("sawtooth", 160, 45, 0.5, 0.25); },
    win: function () { [523, 659, 784, 1047].forEach(function (f, i) { tone("square", f, f + 1, 0.22, 0.12, i * 0.15); }); tone("square", 1047, 1048, 0.6, 0.12, 0.6); },
    lose: function () { [392, 349, 311, 262].forEach(function (f, i) { tone("sawtooth", f, f * 0.97, 0.35, 0.12, i * 0.3); }); }
};

function play(ten) {
    if (!amThanh) return;
    try { sound[ten](); } catch (e) { /* trình duyệt không hỗ trợ âm thanh thì bỏ qua */ }
}

// ==================================================
// GIAO DIỆN
// ==================================================
function $(id) { return document.getElementById(id); }

function hienIntro() {
    $("intro").innerHTML =
        "<p>Tôi tên là <b>" + name + "</b> - MSSV: <b>" + MSSV + "</b> sẽ quyết tâm trở thành siêu anh hùng <b>" + hero + "</b></p>" +
        "<p>" + name + " sẽ hóa thân thành " + hero + " · Cấp độ hiện tại: <b>2</b></p>" +
        "<p class='lvl'>LEVEL UP! Chúc mừng " + hero + " lên cấp 3! XP +200</p>";
}

function ghiLog(text, kieu) {
    var p = document.createElement("p");
    p.className = kieu;
    p.textContent = text;
    $("log").appendChild(p);
    $("log").scrollTop = $("log").scrollHeight;
}

function capNhat() {
    var heroHp = Math.max(0, Math.round(health));
    var drgHp = Math.max(0, Math.round(dragonHealth));
    $("heroHpText").textContent = heroHp + " / " + heroMaxHealth;
    $("dragonHpText").textContent = drgHp + " / " + DRAGON_MAX_HP;
    $("heroHpBar").style.width = (heroHp / heroMaxHealth * 100) + "%";
    $("dragonHpBar").style.width = (drgHp / DRAGON_MAX_HP * 100) + "%";
}

function rung(id) {
    var el = $(id);
    el.classList.remove("shake", "flash");
    void el.offsetWidth; // khởi động lại hiệu ứng
    el.classList.add("shake", "flash");
}

// ---------- QUEST 3 + 4: NHẬP TÊN, CHỌN PHÂN CẤP ----------
var chonVai = "";

document.querySelectorAll(".class-btn").forEach(function (btn) {
    btn.addEventListener("click", function () {
        play("click");
        document.querySelectorAll(".class-btn").forEach(function (b) { b.classList.remove("active"); });
        btn.classList.add("active");
        chonVai = btn.dataset.class;
        $("btnStart").disabled = false;
    });
});

$("btnStart").addEventListener("click", batDau);

function batDau() {
    play("click");
    taoNhanVat();
    $("log").innerHTML = "";

    var nhapTen = $("inpName").value.trim();
    if (nhapTen == name) {
        ghiLog("Chính xác! Bạn có trí nhớ rất tốt (INT +5)", "info");
        intel = intel + 5;
    } else {
        ghiLog("Tệ thật! Tên chính mình mà không nhớ (INT -5)", "info");
        intel = intel - 5;
    }

    switch (chonVai) {
        case "a":
            phancap = "Chiến Binh";
            strength = strength + 15;
            agility = agility - 10;
            intel = intel - 5;
            break;

        case "b":
            phancap = "Sát Thủ";
            strength = strength - 15;
            agility = agility + 10;
            intel = intel + 5;
            break;

        case "c":
            phancap = "Thợ Săn";
            strength = strength - 15;
            intel = intel + 15;
            break;
    }

    // LEVEL UP lên cấp 4
    level = 4;
    xp = xp + 700;
    ghiLog("LEVEL UP! Chúc mừng " + hero + " lên cấp 4! XP +700", "info");

    // ---------- QUEST 5: ĐẤU TRƯỜNG SINH TỬ ----------
    level = 5; // Đảm bảo bạn sẽ ở cấp độ 5
    health = level + 100; // Khi lượng máu <= 0 thì bạn thua
    heroMaxHealth = health;
    dragonHealth = DRAGON_MAX_HP;

    console.log(strength); // Quyết định mức sát thương
    console.log(agility);  // Quyết định xác suất né đòn
    console.log(intel);    // Quyết định xác suất chí mạng

    $("heroName").textContent = name;
    $("heroClass").textContent = hero + " · " + phancap;
    $("heroStr").textContent = Math.round(strength);
    $("heroAgi").textContent = Math.round(agility);
    $("heroInt").textContent = Math.round(intel);
    $("dragonStr").textContent = dragonStr;
    $("dragonAgi").textContent = dragonAgi;
    $("dragonInt").textContent = dragonInt;
    capNhat();

    ghiLog("Rồng Già xuất hiện! Bấm TẤN CÔNG để bắt đầu trận đấu.", "info");

    luot = 0;
    gameContinue = true;
    dangDanh = false;
    $("btnAttack").disabled = false;
    $("setup").classList.add("hidden");
    $("arena").classList.remove("hidden");
}

// ---------- MỘT LƯỢT ĐÁNH (mỗi lần bấm TẤN CÔNG) ----------
$("btnAttack").addEventListener("click", function () {
    if (!gameContinue || dangDanh) return;
    dangDanh = true;
    $("btnAttack").disabled = true;
    luot++;
    ghiLog("--- Lượt " + luot + " ---", "turn");

    // Anh hùng tấn công!
    var heroHit = Math.random() * 100 > dragonAgi;

    if (heroHit) {
        dragonHealth = dragonHealth - strength;
        ghiLog("⚔️ TRẢM LONG! Oggy gây sát thương " + Math.round(strength) + " điểm cho Rồng!", "hero");
        play("slash");
        rung("dragonCard");

        var heroCritical = Math.random() * 100 < intel;

        if (heroCritical) {
            dragonHealth = dragonHealth - strength * 2; // Double damage
            ghiLog("💥 CHÍ MẠNG! Oggy gây sát thương chí mạng thêm " + Math.round(strength * 2) + " điểm cho Rồng!", "hero");
            play("crit");
        }
    } else {
        ghiLog("💨 XÍ HỤT! Rồng đã lượn tránh được đòn tấn công của Oggy!", "miss");
        play("miss");
    }
    capNhat();

    if (dragonHealth <= 0) {
        // Rồng lên bàn thờ
        ketThuc(true);
        return;
    }

    // Rồng tấn công! (chậm một nhịp cho dễ theo dõi)
    setTimeout(function () {
        var dragonHit = Math.random() * 100 > agility;

        if (dragonHit) {
            health = health - dragonStr;
            ghiLog("🔥 HỎA CẦU! Rồng gây sát thương " + dragonStr + " điểm cho Oggy!", "dragon");
            play("fire");
            rung("heroCard");

            var dragonCritical = Math.random() * 100 < dragonInt;

            if (dragonCritical) {
                health = health - dragonStr * 2; // Double damage
                ghiLog("☄️ CHÍ MẠNG! Rồng gây sát thương chí mạng thêm " + dragonStr * 2 + " điểm cho Oggy!", "dragon");
            }
        } else {
            ghiLog("✨ XÍ HỤT! Oggy đã tránh được đòn tấn công từ Rồng!", "miss");
            play("miss");
        }
        capNhat();

        // Kiểm tra xem Anh Hùng có bị nướng?!
        if (health <= 0) {
            ketThuc(false);
            return;
        }

        dangDanh = false;
        $("btnAttack").disabled = false;
    }, 700);
});

function ketThuc(thang) {
    gameContinue = false;
    $("btnAttack").disabled = true;
    setTimeout(function () {
        if (thang) {
            ghiLog("🏆 CHIẾN THẮNG!!! Chúc mừng Mèo Oggy đã chiến thắng Rồng Già!!!", "win");
            play("win");
        } else {
            ghiLog("💀 THẤT BẠI!!! Chúc mừng bạn đã có tên trên menu món tối của Rồng: Mèo Oggy nướng lá lốt", "lose");
            play("lose");
        }
    }, 450);
}

// ---------- CHƠI LẠI ----------
$("btnReplay").addEventListener("click", function (e) {
    e.preventDefault();
    play("click");
    gameContinue = false;
    $("arena").classList.add("hidden");
    $("setup").classList.remove("hidden");
});

$("btnSound").addEventListener("click", function () {
    amThanh = !amThanh;
    $("btnSound").textContent = amThanh ? "🔊" : "🔇";
    if (amThanh) play("click");
});

hienIntro();
