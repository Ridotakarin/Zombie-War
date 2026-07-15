<a name="readme-top"></a>

<p align="center">
  <img src="README_assets/Banner.png" width="100%" alt="Zombie War Banner"/>
</p>

<h1 align="center">Zombie War — Top-Down Zombie Survival</h1>

<p align="center">
  <img src="https://img.shields.io/badge/Engine-Unity%206.5-black?style=for-the-badge&logo=unity" />
  <img src="https://img.shields.io/badge/Genre-Top--Down%20Action-red?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Platform-Android-green?style=for-the-badge&logo=android" />
</p>

---

## 👋 Giới thiệu

**Zombie War** là một game top-down action sinh tồn, nơi người chơi điều khiển một người lính chiến đấu xuyên qua 3 khu vực đầy zombie để tới điểm đích. Dự án tập trung vào một combat loop dồn dập: né tránh — bắn — quăng bom, kết hợp hệ thống pooling và tối ưu hiệu năng để đảm bảo chạy mượt trên nhiều thiết bị Android.

Góc nhìn camera lấy cảm hứng từ phong cách top-down nghiêng của *Last Day on Earth*, theo sát nhân vật bằng Cinemachine.

---

## 🎮 Hướng dẫn Gameplay (Gameplay Tutorial)

Hệ thống hướng dẫn trong game được chia thành 4 trang trực quan giúp người chơi nhanh chóng làm quen với nhịp độ chiến đấu dồn dập của **Zombie War**.

---

### 1. Di chuyển bằng Virtual Joystick
Để sinh tồn trước bầy xác sống đông đảo, việc làm chủ hướng di chuyển là ưu tiên số một.
<p align="center">
  <img src="README_assets/Joystick.png" width="80%" alt="Joystick Tutorial"/>
  <img src="README_assets/Button.png" width="80%" alt="Joystick Tutorial"/>
</p>

* **Cơ chế:** Sử dụng nút xoay ảo (**Virtual Joystick**) ở góc trái màn hình cảm ứng để điều khiển hướng chạy của nhân vật.
* **Mẹo sinh tồn:** Luôn di chuyển liên tục theo hình vòng tròn (kite quái) để tránh bị dồn vào góc chết hoặc bị bao vây bởi các Zombie tốc độ cao.

---

### 2. Nhặt Vật phẩm Tiếp tế (Loot Items)
Dọc đường đi, các hòm tiếp tế là nguồn cứu trợ sinh mạng cực kỳ quan trọng.
<p align="center">
  <img src="README_assets/gameplay_2.png" width="80%" alt="Loot Items Tutorial"/>
</p>

* **Cơ chế:** Bước vào **Vòng tròn phát sáng (Glow Indicator)** xung quanh vật phẩm rơi trên đất để tự động thu thập.
* **Các loại tiếp tế:**
  * **Hộp cứu thương (Medkit):** Hồi phục một lượng máu ngay lập tức.
  * **Thùng đạn (Ammo):** Bổ sung đạn dự phòng cho vũ khí hiện tại.
  * **Mở khóa súng:** Nhặt vũ khí mới (ví dụ: Rifle) để gia tăng hỏa lực.

---

### 3. Kho Vũ khí Đa dạng (Multi-Weapon)
Linh hoạt thay đổi hỏa lực tùy theo tình huống đối đầu với từng loại Zombie.
<p align="center">
  <img src="README_assets/weapons.png" width="80%" alt="Weapons Tutorial"/>
</p>

* **Nút Switch (Đổi súng):** Nhấn để đổi qua lại giữa các vũ khí đã mở khóa.
* **Nút Bomb (Lựu đạn):** Ném một quả bom hẹn giờ gây sát thương diện rộng cực lớn (Splash Damage) dọn dẹp các cụm Zombie trong tích tắc.

---

### 4. Tiến tới Đích đến (Reach Goal)
Mục tiêu cuối cùng là sống sót xuyên qua cả 3 khu vực nguy hiểm để tới điểm sơ tán.
<p align="center">
  <img src="README_assets/Stage.png" width="80%" alt="Reach Goal Tutorial"/>
  <img src="README_assets/gameplay_3.png" width="80%" alt="Reach Goal Tutorial"/>
</p>

* **Cơ chế:** Map theo hướng ngang, hãy cố sống sót và tìm kiếm **Khu vực doanh trại quân đội**.
* **Điều kiện chiến thắng:** Chạm vào điểm đích để kích hoạt trực thăng/xe cứu hộ di tản và hoàn thành màn chơi an toàn.

---
## ⚔️ Vũ khí & Trang bị

| Vũ khí | Cơ chế | Đặc điểm |
| :--- | :--- | :--- |
| **Pistol** | Raycast | Tốc độ bắn vừa phải, chính xác |
| **Rifle** | Raycast | Tốc độ bắn nhanh, phù hợp giao tranh liên tục |
| **Grenade** | Splash damage (OverlapSphere) | Gây sát thương diện rộng, giới hạn số lượng mang theo |

- **Hệ thống pooling:** zombie và hiệu ứng particle (muzzle flash, hit impact) đều được quản lý qua object pool trung tâm, giảm tối đa chi phí Instantiate/Destroy khi giao tranh liên tục.
- **Multi-weapon:** đổi súng bằng 1 nút bấm, tự động bỏ qua vũ khí chưa mở khóa.

---

## ⚙️ Cơ chế cốt lõi

### 🧟 Zombie AI
- Trạng thái Idle / Chase / Attack dựa trên khoảng cách tới người chơi (NavMeshAgent).
- Hiệu ứng dissolve shader phát khi zombie chết, sau đó tự động release về pool.

### 🎯 Combat
- Bắn theo raycast, có tracer hiển thị đường đạn thời gian thực (LineRenderer).
  <p align="center">
  <img src="README_assets/gameplay_1.png" width="80%" alt="Weapons Tutorial"/>
</p>
- Hiệu ứng trúng đạn khác nhau theo bề mặt (thịt / vật thể).

### 🗺️ Level Design
- Map chia 3 khu vực, zombie spawn theo trigger khi người chơi tiến vào từng khu vực — tạo nhịp độ tăng dần thay vì dồn hết quái ngay từ đầu.
- Chạm điểm đích ở cuối khu vực 3 để chiến thắng.

### 🖥️ UI & Đa nền tảng
- HUD hiển thị máu, đạn, thời gian, có Pause menu với chỉnh âm lượng và chất lượng đồ họa.
- Hỗ trợ đa độ phân giải (Canvas Scaler) và Safe Area cho các thiết bị có notch/tai thỏ.

---

## 🚀 Cài đặt

### ⚙️ Quick Start
1. **Clone:** `git clone <đường-dẫn-repo-của-bạn>`
2. **Mở:** Unity 6.5 hoặc mới hơn.
3. **Chạy:** Load scene `Assets/Scenes/Menu.unity`.

### ⌨️ Điều khiển
- **Virtual Joystick:** Di chuyển nhân vật
- **Nút Fire:** Giữ để bắn liên tục
- **Nút Switch:** Đổi vũ khí
- **Nút Reload:** Nạp đạn
- **Nút Bomb:** Ném lựu đạn

---

## 👤 Liên hệ

**[Tên của bạn]** — Game Developer
* **LinkedIn:** [Link LinkedIn của bạn]
* **Email:** [Email của bạn]

---
<p align="center">
  Cảm ơn đã dành thời gian trải nghiệm Zombie War! 🧟‍♂️🔫
</p>
